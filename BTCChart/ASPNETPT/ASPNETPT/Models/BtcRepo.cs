using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.ApplicationInsights.AspNetCore.TelemetryInitializers;
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

        public void GetBtCdata()
        {
            _logger.LogInformation("Getting BTC DATA from the database");


            var testList = new List<string>();
         
            String URLString =
                "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20(%22BTCUSD%22)&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
            XmlTextReader reader = new XmlTextReader(URLString);

            while (reader.Read())
            {

                switch (reader.NodeType)
                {


                    case XmlNodeType.Text: //Display the text in each element.
                        testList.Add(reader.Value);
                        break;


                    default:
                        break;

                }
            }


            var data = new BtCprop()
            {
                Name = testList[0],
                Rate = Convert.ToDecimal(testList[1], CultureInfo.InvariantCulture),
                Date = testList[2],
                Time = testList[3],
                Ask =  testList[4], 
                Bid =  testList[5],

            };

            _context.Btcs.Add(data);
            _context.SaveChanges();
            
        }
    }
}
