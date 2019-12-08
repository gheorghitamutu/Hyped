using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.Data
{
    public class VM
    {
        public Guid VMId { get; set; }
        public string RealID { get; set; }
        public string Name { get; set; }
        public string Configuration { get; set; }
        public string LastSave { get; set; }

        public static VM Create(string realid,string name,string configuration,string lastsave)
        {
            return new VM
            {
                VMId = Guid.NewGuid(),
                RealID = realid,
                Name = name,
                Configuration = configuration,
                LastSave = lastsave
            };
        }

        public void Update(string realid, string name, string configuration, string lastsave)
        {
            RealID = realid;
            Name = name;
            Configuration = configuration;
            LastSave = lastsave;
        }
    }
}
