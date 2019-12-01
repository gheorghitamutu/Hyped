using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class VM
    {
        public Guid VMId { get; set; }
        public string RealID { get; set; }
        public string Name { get; set; }
        public string Configuration { get; set; }
        public string LastSave { get; set; }
    }
}
