using Microsoft.EntityFrameworkCore;
using System;

namespace EntityFrameworkMapping.Tests
{
    public class TestDbContext : DbContext
    {
        public DbSet<CircularParentEntity> CircularParentEntities { get; set; }
        public DbSet<CircularChildEntity> CircularChildEntities { get; set; }

        public DbSet<IdOnlyParentEntity> IdOnlyParentEntities { get; set; }
        public DbSet<IdOnlyChildEntity> IdOnlyChildEntities { get; set; }

        public DbSet<MultiRefParentEntity> MultiRefParentEntities { get; set; }
        public DbSet<MultiRefReferenceEntity> MultiRefReferenceEntities { get; set; }
        public DbSet<MultiRefChildEntity> MultiRefChildEntities { get; set; }

        public DbSet<NullableParentEntity> NullableParentEntities { get; set; }
        public DbSet<NullableChildEntity> NullableChildEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString()).EnableSensitiveDataLogging();
    }
}
