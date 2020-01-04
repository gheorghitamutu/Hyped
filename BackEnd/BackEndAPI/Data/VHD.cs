using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.Data
{
    public class VHD
    {
        [Key]
        public Guid VHDId { get; private set; }
        public Guid SCId { get; private set; }
        public string Name { get; private set; }
        public string InstanceId { get; private set; }
        public string Path { get; private set; }
        public int Size { get; private set; }

        public static VHD Create(Guid scid,string name,string instanceid,string path,int size)
        {
            return new VHD
            {
                VHDId = Guid.NewGuid(),
                SCId=scid,
                InstanceId=instanceid,
                Name=name,
                Path=path,
                Size=size
            };
        }


        public void Update(Guid scid,string name,string instanceid,string path,int size)
        {
            SCId = scid;
            Name = name;
            InstanceId = instanceid;
            Path = path;
            Size = size;
        }
    }
}
