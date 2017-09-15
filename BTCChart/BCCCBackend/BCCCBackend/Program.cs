using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using static System.Console;

namespace BCCCBackend
{
    class Program
    {
        static void Main(string[] args)
        {
           
            int choice = 0;
           
            while (choice == 0 || choice ==1)
            {
                
                
                if (choice == 0)
                {
                    var timer = new System.Threading.Timer(e => DB.Update(DB.GetConnect(), BtcGetStock()), null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
                    choice = Convert.ToInt32(ReadLine());
                }


                else if (choice == 1)
                {
                    DB.Update(DB.GetConnect(), BtcGetStock());
                    choice = Convert.ToInt32(ReadLine());
                }

                else
                {
                    Environment.Exit(0);
                }
                
            }

            }

        public static BtcProp BtcGetStock()
        {
            var testList = new List<string>();
            var culture = CultureInfo.InvariantCulture;
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

            var prop = new BtcProp
            {
                Name = testList[0],
                Rate = Convert.ToDouble(testList[1], culture),
                Date = testList[2],
                Time = testList[3],
                Ask = Convert.ToDouble(testList[4], culture),
                Bid = Convert.ToDouble(testList[5], culture)
            };



            WriteLine($"{prop.Name} {prop.Rate} {prop.Date} {prop.Time} {prop.Ask} {prop.Bid} ADDED");




            return prop;
        }



    }


}
