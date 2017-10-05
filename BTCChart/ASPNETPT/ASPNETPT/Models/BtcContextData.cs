using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SQLitePCL;

namespace ASPNETPT.Models
{
    public class BtcContextData
    {
        private static BtcContext _context;

        public BtcContextData(BtcContext context)
        {
            _context = context;
        }



      //  INCASE FOR UPDATEING TO DATABASE BY WEB //no need with backend service.

        //    public async Task seedData()
        //{

        //    var testList = new List<string>();
        //    var culture = CultureInfo.InvariantCulture;
        //    String URLString =
        //        "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20(%22BTCUSD%22)&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
        //    XmlTextReader reader = new XmlTextReader(URLString);

        //    while (reader.Read())
        //    {

        //        switch (reader.NodeType)
        //        {


        //            case XmlNodeType.Text: //Display the text in each element.
        //                testList.Add(reader.Value);
        //                break;


        //            default:
        //                break;

        //        }
        //    }


        //    if (!_context.Btcs.Any())
        //    {
        //        var data
        //             = new BtCprop()
        //             {
        //                 Name = testList[0],
        //                 Rate = Convert.ToDouble(testList[1], culture),
        //                 Date = testList[2],
        //                 Time = testList[3],
        //                 Ask = Convert.ToDouble(testList[4], culture),
        //                 Bid = Convert.ToDouble(testList[5], culture)

        //             };

        //        _context.Btcs.Add(data);



        //        await _context.SaveChangesAsync();

        //    }
        //}


        //    public static void GetBtcsFromYahoo()
        //    {

        //        var testList = new List<string>();
        //        var culture = CultureInfo.InvariantCulture;
        //        String URLString =
        //            "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20(%22BTCUSD%22)&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
        //        XmlTextReader reader = new XmlTextReader(URLString);

        //        while (reader.Read())
        //        {

        //            switch (reader.NodeType)
        //            {


        //                case XmlNodeType.Text: //Display the text in each element.
        //                    testList.Add(reader.Value);
        //                    break;


        //                default:
        //                    break;

        //            }
        //        }


        //                    var data= new BtCprop()
        //                 {
        //                     Name = testList[0],
        //                     Rate = Convert.ToDouble(testList[1], culture),
        //                     Date = testList[2],
        //                     Time = testList[3],
        //                     Ask = Convert.ToDouble(testList[4], culture),
        //                     Bid = Convert.ToDouble(testList[5], culture)

        //                 };

        //            _context.Btcs.Add(data);
        //            _context.SaveChanges();





        //}



        public IEnumerable<BtCprop> GetBtcs()
        {
            return _context.Set<BtCprop>();
        }

    }
}
    
