using Mapster;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkMapping.Tests
{
    public class EntityFrameworkMapsterTests
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

            var contract = entity.Adapt<CircularParent>();

            contract.Adapt(entity);

            Console.WriteLine($"State details:\n{_context.ChangeTracker.DebugView.LongView }");

            Assert.DoesNotThrow(() => _context.SaveChanges());
        }

        [Test]
        public void IdReferenceMapping()
        {
            var parent = new IdOnlyParentEntity
            {
                Id = 1,
            };
            _context.IdOnlyParentEntities.Add(parent);

            var childEntities = new List<IdOnlyChildEntity>()
            {
                new IdOnlyChildEntity
                {
                    Id = 1,
                    ParentId = parent.Id,
                },

                new IdOnlyChildEntity
                {
                    Id = 2,
                    ParentId = parent.Id,
                },
            };

            _context.IdOnlyChildEntities.AddRange(childEntities);

            _context.SaveChanges();

            var entity = _context.IdOnlyParentEntities
                .Include(e => e.Children)
                .ToList()
                .First();

            Console.WriteLine($"State details:\n{_context.ChangeTracker.DebugView.LongView }");

            var contract = entity.Adapt<IdOnlyParent>();

            contract.Adapt(entity);

            Console.WriteLine($"State details:\n{_context.ChangeTracker.DebugView.LongView }");

            Assert.DoesNotThrow(() => _context.SaveChanges());
        }

        [Test]
        public void MultiReferenceMapping()
        {
            var reference = new MultiRefReferenceEntity
            {
                Id = 1,
            };
            _context.MultiRefReferenceEntities.Add(reference);

            var parent = new MultiRefParentEntity
            {
                Id = 1,
                ReferenceId = reference.Id,
            };
            _context.MultiRefParentEntities.Add(parent);

            var childEntities = new List<MultiRefChildEntity>()
            {
                new MultiRefChildEntity
                {
                    Id = 1,
                    ParentId = parent.Id,
                    ReferenceId = reference.Id,
                },

                new MultiRefChildEntity
                {
                    Id = 2,
                    ParentId = parent.Id,
                    ReferenceId = reference.Id,
                },
            };

            _context.MultiRefChildEntities.AddRange(childEntities);

            _context.SaveChanges();

            var entity = _context.MultiRefParentEntities
                .Include(e => e.Children)
                .ThenInclude(e => e.Reference)
                .ToList()
                .First();

            Console.WriteLine($"State details:\n{_context.ChangeTracker.DebugView.LongView }");

            var contract = entity.Adapt<MultiRefParent>();

            contract.Adapt(entity);

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

            var contract = entity.Adapt<MultiRefParent>();

            contract.Adapt(entity);

            Console.WriteLine($"State details:\n{_context.ChangeTracker.DebugView.LongView }");

            Assert.DoesNotThrow(() => _context.SaveChanges());
        }
    }
}