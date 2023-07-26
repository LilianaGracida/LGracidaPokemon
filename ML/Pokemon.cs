using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public  class Pokemon
    {
        public dynamic is_hidden { get; set; }
        public Pokemon pokemon { get; set; }
        public int slot { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string count { get; set; }
         public string tipo { get; set; }
        public dynamic img { get; set; }
        public List<object> Tipos { get; set; }
        public List<object> results { get; set; }
        public dynamic next { get; set; }
        public dynamic previous { get; set; }
         public int numero { get; set; }
    }
}
