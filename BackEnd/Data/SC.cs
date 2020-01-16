using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data
{
    public class SC
    {
        [Key]
        public Guid SCId { get; private set; }
        public string Name { get; private set; }
        public string InstanceId { get; private set; }
        public virtual ICollection<VHD> VHDs { get; private set; }
        public virtual ICollection<CDDVD> CDDVDs { get; private set; }
        public Guid VMId { get; private set; }

        public static SC Create(string name,string instanceid,Guid vmid)
        {
            return new SC
            {
                SCId = Guid.NewGuid(),
                Name=name,
                InstanceId=instanceid,
                VMId=vmid      
            };
        }


        public void Update(string name,string instanceid,ICollection<VHD> vhds,ICollection<CDDVD> cddvds,Guid vmid)
        {
            Name = name;
            VHDs = vhds;
            InstanceId = instanceid;
            CDDVDs = cddvds;
            VMId = vmid;
        }
    }
}
