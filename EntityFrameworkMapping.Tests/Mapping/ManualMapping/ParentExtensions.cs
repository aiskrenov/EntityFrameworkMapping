using System.Linq;

namespace EntityFrameworkMapping.Tests
{
    /// <summary>
    /// This is implemented as static methods for convenience, but in a real solutions 
    /// this should be implemented as an instance of an interface and proper unit tests should be provided.
    /// A generic IMapper interface can be provided that maps from one type to another and can be injected into scope
    /// to enforce our data sagregation strategy.
    /// </summary>
    public static class ParentExtensions
    {
        public static CircularParentEntity ToEntity(this CircularParent contract, CircularParentEntity entity = null)
        {
            if (entity == null)
            {
                entity = new();
            }

            entity.Id = contract.Id;

            if (entity.Children == null)
            {
                entity.Children = new();
            }

            // This can be a generic collection mapper based on IIdentifiable
            // The point is to keep the old list and old element, remove any that are not there,
            // update the ones that exist and add the ones that are missing.
            // Note that such logic is only required when we need to map from Contract to Entity
            // and preserve the instances with as little changes as possible.
            // When mapping from entity to Contract this is, most likely, not required.
            if (contract.Children != null)
            {
                var missingIds = entity.Children
                    .Select(e => e.Id)
                    .Where(id => !contract.Children.Any(c => c.Id == id));

                foreach (var childId in missingIds)
                {
                    var toRemove = entity.Children.Where(c => c.Id == childId).First();
                    entity.Children.Remove(toRemove);
                }

                foreach (var child in contract.Children)
                {
                    var existingChild = entity.Children.FirstOrDefault(e => e.Id == child.Id);
                    if (existingChild == null)
                    {
                        // Here we can also set scope and generate IDs
                        entity.Children.Add(child.ToEntity());
                    }
                    else
                    {
                        child.ToEntity(existingChild);
                    }
                }
            }

            return entity;
        }

        public static CircularParent ToContract(this CircularParentEntity entity)
        {
            var result = new CircularParent
            {
                Id = entity.Id
            };

            if (entity.Children != null)
            {
                foreach (var child in entity.Children)
                {
                    result.Children.Add(child.ToContract());
                }
            }

            return result;
        }

        public static NullableParentEntity ToEntity(this NullableParent contract, NullableParentEntity entity = null)
        {
            if (entity == null)
            {
                entity = new();
            }

            entity.Id = contract.Id;

            if (entity.Children == null)
            {
                entity.Children = new();
            }

            // This can be a generic collection mapper based on IIdentifiable
            // The point is to keep the old list and old element, remove any that are not there,
            // update the ones that exist and add the ones that are missing.
            // Note that such logic is only required when we need to map from Contract to Entity
            // and preserve the instances with as little changes as possible.
            // When mapping from entity to Contract this is, most likely, not required.
            if (contract.Children != null)
            {
                var missingIds = entity.Children
                    .Select(e => e.Id)
                    .Where(id => !contract.Children.Any(c => c.Id == id));

                foreach (var childId in missingIds)
                {
                    var toRemove = entity.Children.Where(c => c.Id == childId).First();
                    entity.Children.Remove(toRemove);
                }

                foreach (var child in contract.Children)
                {
                    var existingChild = entity.Children.FirstOrDefault(e => e.Id == child.Id);
                    if (existingChild == null)
                    {
                        // Here we can also set scope and generate IDs
                        entity.Children.Add(child.ToEntity());
                    }
                    else
                    {
                        child.ToEntity(existingChild);
                    }
                }
            }

            return entity;
        }

        public static NullableParent ToContract(this NullableParentEntity entity)
        {
            var result = new NullableParent
            {
                Id = entity.Id
            };

            if (entity.Children != null)
            {
                foreach (var child in entity.Children)
                {
                    result.Children.Add(child.ToContract());
                }
            }

            return result;
        }
    }
}
