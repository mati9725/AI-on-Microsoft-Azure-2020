using System.Collections.Generic;

namespace InMemoryDb.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public int DeviceTypeId { get; set; }
        public int LocationId { get; set; }
        public int PowerStateId { get; set; }

        public virtual LuisEntity DeviceType { get; set; }
        public virtual LuisEntity Location { get; set; }
        public virtual LuisEntity PowerState { get; set; }
    }
}