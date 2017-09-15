using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ASPNETPT.Models
{
    public class BtcRepo : IBtcRepo
    {
        private BtcContext _context;
        private ILogger<BtcRepo> _logger;


        public BtcRepo(BtcContext context, ILogger<BtcRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<BtCprop> GetBtCprops()
        {
            _logger.LogInformation("Getting BTC DATA from the database");
           
            return _context.Btcs.ToList();
        }
    }
}
