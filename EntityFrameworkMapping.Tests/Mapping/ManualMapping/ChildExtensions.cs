namespace EntityFrameworkMapping.Tests
{
    public static class ChildExtensions
    {
        public static CircularChildEntity ToEntity(this CircularChild contract, CircularChildEntity entity = null)
        {
            if (entity == null)
            {
                entity = new();
            }

            entity.Id = contract.Id;

            return entity;
        }

        public static CircularChild ToContract(this CircularChildEntity entity)
        {
            var contract = new CircularChild
            {
                Id = entity.Id
            };

            return contract;
        }

        public static NullableChildEntity ToEntity(this NullableChild contract, NullableChildEntity entity = null)
        {
            if (entity == null)
            {
                entity = new();
            }

            entity.Id = contract.Id;
            entity.ParentId = contract.ParentId;

            return entity;
        }

        public static NullableChild ToContract(this NullableChildEntity entity)
        {
            var contract = new NullableChild
            {
                Id = entity.Id,
                ParentId = entity.ParentId,
            };

            return contract;
        }
    }
}
