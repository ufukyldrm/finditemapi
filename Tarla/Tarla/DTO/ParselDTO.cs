using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tarla.DTO
{
    public class ParselDTO
    {

        public int id { get; set; }
        public int parselno2 { get; set; }
        public int adano2 { get; set; }
        public string satınalmadurumu { get; set; }
        public Models.parselcoordinats maincoordinates { get; set; }

        public List<Models.parselcoordinats> parselcoordinats { get; set; }



    }
}
