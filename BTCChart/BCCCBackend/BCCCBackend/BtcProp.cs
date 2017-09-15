using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCCCBackend
{

    [Table(Name = "BTC")]
    class BtcProp
    {
        public string Name { get; set; }
        public double Rate { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public double Ask { get; set; }
        public double Bid { get; set; }
        
        
    }
}
