using System.Collections.Generic;

namespace InMemoryDb.Models
{
    public class LuisEntity
    {
        public int LuisEntityId { get; set; }
        public string Name { get; set; }
        public int? ParentEntityId { get; set; }

        public virtual LuisEntity ParentEntity { get; set; }

    }
}