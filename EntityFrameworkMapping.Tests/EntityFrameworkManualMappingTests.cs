using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkMapping.Tests
{
    public class EntityFrameworkManualMappingTests
    {
        private TestDbContext _context { get; set; }

        [SetUp]
        public void Setup() => _context = new TestDbContext();

        [Test]
        public void CircularReferenceMapping()
        {
            var parent = new CircularParentEntity
            {
                Id = 1,
            };
            _context.CircularParentEntities.Add(parent);

            var childEntities = new List<CircularChildEntity>()
            {
                new CircularChildEntity
                {
                    Id = 1,
                    Parent = parent,
                },

                new CircularChildEntity
                {
                    Id = 2,
                    Parent = parent,
                },
            };

            _context.CircularChildEntities.AddRange(childEntities);

            _context.SaveChanges();

            var entity = _context.CircularParentEntities
                .Include(e => e.Children)
                .ToList()
                .First();

            Console.WriteLine($"State details:\n{_context.ChangeTracker.DebugView.LongView }");

            var contract = entity.ToContract();

            contract.ToEntity(entity);

            Console.WriteLine($"State details:\n{_context.ChangeTracker.DebugView.LongView }");

            Assert.DoesNotThrow(() => _context.SaveChanges());
        }

        [Test]
        public void NullableParentMapping()
        {
            var parent = new NullableParentEntity
            {
                Id = 1,
            };
            _context.NullableParentEntities.Add(parent);

            var childEntities = new List<NullableChildEntity>()
            {
                new NullableChildEntity
                {
                    Id = 1,
                    ParentId = parent.Id,
                },

                new NullableChildEntity
                {
                    Id = 2,
                    ParentId = parent.Id,
                },
            };

            _context.NullableChildEntities.AddRange(childEntities);

            _context.SaveChanges();

            var entity = _context.NullableParentEntities
                .Include(e => e.Children)
                .ToList()
                .First();

            Console.WriteLine($"State details:\n{_context.ChangeTracker.DebugView.LongView }");

            var contract = entity.ToContract();

            contract.ToEntity(entity);

            Console.WriteLine($"State details:\n{_context.ChangeTracker.DebugView.LongView }");

            Assert.DoesNotThrow(() => _context.SaveChanges());
        }
    }
}
