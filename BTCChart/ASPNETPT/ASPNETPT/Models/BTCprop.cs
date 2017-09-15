using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPT.Models
{
    public class BtCprop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rate { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public double Ask { get; set; }
        public double Bid { get; set; }

        //public ICollection<BtCprop> Btc { get; set; }

    }
}
