using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEndAPI.Data
{
    public class VM
    {
        [Key]
        public Guid VMId { get; private set; }
        public string RealID { get; private set; }
        public string Name { get; private set; }
        public string Configuration { get; private set; }
        public string LastSave { get; private set; }
        public virtual ICollection<Network> Networks { get; private set; }
        public int RAM { get; private set; }
        public int Cores { get; private set; }
        public virtual ICollection<SC> SCs { get; private set; }
        public Guid UserId { get; private set; }

        public static VM Create(string realid,string name,string configuration,string lastsave,int ram, int cores, Guid userId)
        {
            return new VM
            {
                VMId = Guid.NewGuid(),
                RealID = realid,
                Name = name,
                Configuration = configuration,
                LastSave = lastsave,
                RAM = ram,
                Cores=cores,
                UserId = userId
            };
        }

        public void Update(string realid, string name, string configuration, string lastsave,ICollection<Network> networks,int ram,int cores,ICollection<SC> scs)
        {
            RealID = realid;
            Name = name;
            Configuration = configuration;
            LastSave = lastsave;
            Networks = networks;
            RAM = ram;
            Cores = cores;
            SCs = scs;
        }
    }
}
