using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Tipo
    {
        public string tipo { get; set; }
        public dynamic pokemon { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public bool is_hidden { get; set; }
         public int slot { get; set; }
        public List<Object> results { get; set; }
        public string img { get; set; }
    }
}
