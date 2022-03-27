# Entity Framework Core Mapping Tests

Simple use cases of Entity Framework Core, saving changes after different types of mappers are used to update related entities. The goal of this solution is to demonstrate how different mapping techniques and libraries behave in common mapping scenarious. Use cases are isolated with the bare minimum of fields and are oversimplified in order to demonstrate the behaviour.

## Used Mappers
For the testing we used 3 mapping techniques:
* Automapper 3rd party library - heavily uses reflections, expression parsing and some code generation.
* Mapster 3rd party library - based entirely on code generation.
* Manual mapping - classical assignment of field values one-by-one with code.

## Test Cases
In order to demonstrate how the above mapping approaches work with Entity Framework Core, we've put together 4 test cases:
* Mapping circular references - A parent-child data structure where the parent has all the child object and each child has a reference to the parent object.
* ID as reference mapping - A parent-child data structure where we have the navigational properties in the entities, but we are mapping IDs for singular regerences. The child has a ParentId and the contract exposes only a ParentId, removing the circular reference between the two.
* Multi-reference mapping - A parent-child data structure, where both the parent and the child are pointing to a third reference object. Navigational properties are kept in the entities, but only IDs are exposed in the contracts. This test case was added to explain why StackOverflow is not a good source of information.
* Nullable parent mapping - Probably the most important test in this PoC. A parent-child data structure where the ParentId is a nullable value. That is an important detail as nullable value type in C# is a struct that wraps the base value type.

## Test Results
![image](https://user-images.githubusercontent.com/11503830/160257497-0d15e55c-6e72-405a-8d07-da036a91f1e0.png)

From the test cases couple of things become immediately obvious:
* Automated mapping does not handle well circullar references. Both Automapper and Mapster failed with this task with default behaviour.
* Automated mapping does not handle well nullable values too. Again both Automapper and Mapster failed with this task with default behaviour.
* Automated mapping is slow. The delays introduced in handling those cases are several times slower than the manual mapping. Automapper scores the worst because of the heavy use of reflections and expression parsing. While Mapster is a lot better due to the code generation approach it still is a lot slower than the classical manual mapping.

## Possible Mitigations

### Automapper
Automapper provides extension mechanisms with couple of interfaces. `IMemberValueResolver` can be used to do custom mapping logic for specific members. However, those extensions always require the creation of a new object at the end, which kills their purpose in this case. While it is a good limitation in automapper to avoid locking instances in hanging references and prevent memory leaks, it is not something that we can use in this case. The following implementation was tried during the tests:

```csharp
/// <summary>
/// Identifiable list member resolver. Used to map lists of database entities by reusing existing items if they are present and adding non existing ones.
/// Suitable for mapping lists for entity framework to make sure that the change tracker only registers actual modifications.
/// This resolver would not delete any entities that are missing in the source, but present in the destination.
/// Entity deletion should be handled manually with the proper repository methods.
/// This resolver also would not attach IDs and scope to the newly added entities.
/// Adding of entities should be handled manually with the proper repository methods.
/// </summary>
/// <typeparam name="TSourceItem">Type of the source item in the list.</typeparam>
/// <typeparam name="TDestItem">Type of the destination item in the list.</typeparam>
public class IdentifyableListResolver<TSourceItem, TDestItem> : IMemberValueResolver<object, object, List<TSourceItem>, List<TDestItem>>
    where TSourceItem : IIdentifiable
    where TDestItem : IIdentifiable
{
    /// <inheritdoc/>
    public List<TDestItem> Resolve(object source, object destination, List<TSourceItem> sourceMember, List<TDestItem> destMember, ResolutionContext context)
    {
        if (sourceMember == null)
        {
            return null;
        }

        if (destMember == null)
        {
            destMember = new List<TDestItem>();
        }

        foreach (var item in sourceMember)
        {
            var existingItem = destMember.FirstOrDefault(e => e.Id == item.Id);
            if (existingItem == null)
            {
                destMember.Add(context.Mapper.Map<TDestItem>(item));
            }
            else
            {
                context.Mapper.Map(item, existingItem);
            }
        }

        return new(destMember);
    }
}

```

From the Automapper source code we can see that [collections are always cleared before the mapping](https://github.com/AutoMapper/AutoMapper/blob/master/src/AutoMapper/Mappers/CollectionMapper.cs) and that [nullable types are mapped as reference types with new structure creation](https://github.com/AutoMapper/AutoMapper/blob/master/src/AutoMapper/Mappers/NullableDestinationMapper.cs). The standard extension mechanisms don't work for the problematic cases, so the only possible solution with Automapper is to define new `IObjectMapperInfo` classes for collections and nullables. From the source code it is clear that this is not straight-forward task as this will require to swap core Automapper behaviour.

### Mapster
The situation with Mapster is quite similar. Mapster's default behaviour will [always recreate lists](https://github.com/MapsterMapper/Mapster/blob/master/src/Mapster/Adapters/CollectionAdapter.cs) and [nullables](https://github.com/MapsterMapper/Mapster/blob/master/src/Mapster/Adapters/PrimitiveAdapter.cs). Extension is possible by defining custom adapters to solve those issues. The lack of comments and documentation are definitely not making this easy.

## Conclusions
Pros of automated mapping:
* It is automated!
* Less boilerplate.
* Easy initial setup with out of the box configuration.

Cons of automated mapping:
* Hard to extend. If we go out of the standard cases things degrade fast. We need to replace core chunks of code in the libraries to account for certain cases.
* Slow. In the case of Automapper - terribly slow. Mapster is significantly faster, but still sensibly slower than manual mapping.
* Potentially dangerous. We introduce DTO and DAO separation for a reason. If it was straight forward 1-to-1 things we wouldn't even need DTOs, we'll just have one set that we use in database and for exposing contracts. With automation it's easy for properties that are not supposed to be there to slip into the contract, exposing sensitive information. In my opinion this should be a controlled process where the developer makes those decisions explicitly.
* Can introduce complex problems at a late stage of the project, because of the assumptions made in those libraries.

Automated mapping is great if you are able to use default behaviour and if you can suffer the performance penalty (mostly in the case of Automapper). However, combining a complex state-based ORM framework like EF with the assumptions made by Automapper and Mapster is probably not the best idea. Both libraries provide EF extensions which at the time of writing this article are outdated and they do not account for complex business cases and are unaware of the Primary Key concept in entity framework, which makes their usage a ticking timebomb that will do some serious damege at some point in more complex projects. The extension of both libraries requires solid knowledge of how they work and it is not a trivial task. When we combine this with the terrible performance, we find ourselves in the situation that in order to save some boilerplate at the time of project setup we introduce greater complexity, less control and potentially hard to track and fix problems. And why? To solve a trivial problem. It is simply not worth it.
