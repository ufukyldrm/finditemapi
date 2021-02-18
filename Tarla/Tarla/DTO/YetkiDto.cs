using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tarla.DTO
{
    public class YetkiDto
    {

        public int id { get; set; }
        public string mail { get; set; }
        public bool duzenleyici { get; set; }
        public bool izleyici { get; set; }
        public bool admin { get; set; }

    }
}
