using System;
using System.ComponentModel.DataAnnotations;

namespace BackEndAPI.Data
{
    public class CDDVD
    {
        [Key]
        public Guid CDDVDId { get; private set; }
        public string InstanceId { get; private set; }
        public string Name { get; private set; }
        public Guid SCId { get; private set; }

        public static CDDVD Create(string instanceid,string name,Guid scid)
        {
            return new CDDVD
            {
                CDDVDId = Guid.NewGuid(),
                InstanceId=instanceid,
                Name=name,
                SCId=scid
            };
        }


        public void Update(string instanceid,string name,Guid scid)
        {
            InstanceId = instanceid;
            Name = name;
            SCId = scid;
        }
    }
}
