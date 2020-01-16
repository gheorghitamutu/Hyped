using System;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data
{
    public class Network
    {
        [Key]
        public Guid NetId { get; private set; }
        public string Name { get; private set; }
        public string Notes { get; private set; }
        public string InstanceID { get; private set; }
        public Guid VMId { get; private set; }

        public static Network Create(string name,string notes,string instanceid,Guid vmid)
        {
            return new Network
            {
                NetId = Guid.NewGuid(),
                Name = name,
                Notes = notes,
                InstanceID=instanceid,
                VMId = vmid
            };
        }
        public void Update(string name,string notes,string instanceid,Guid vmid)
        {
            Name = name;
            Notes = notes;
            InstanceID = instanceid;
            VMId = vmid;
        }
    }
}
