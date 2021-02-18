using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tarla.Models
{
    public class kullanicilar
    {

        public int id { get; set; }
        public string ad { get; set; }
        public string sifre { get; set; }
        public string passwordhash { get; set; }
        public string passwordsalt { get; set; }

        public int yetki { get; set; }

 

    }
}
