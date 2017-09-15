using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace BCCCBackend
{
    class DB
    {
        public static SqlConnection GetConnect()
        {
            string cs =
              @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=BTCBack;Data Source = localhost\SQLEXPRESS";
            SqlConnection connection = new SqlConnection(cs);
            connection.Open();
            Write("Connecting established : ");


            

            return connection;



        }

        public static void Show()

        {
           
        }

        public static void Update(SqlConnection connection, BtcProp data)
                
        {

            string sql = "INSERT INTO Btcs(Name,Rate,Date,Time,Ask,Bid) VALUES(@name,@rate,@date,@time,@ask,@bid)";
            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@name", data.Name);
            cmd.Parameters.AddWithValue("@rate", data.Rate);
            cmd.Parameters.AddWithValue("@date", data.Date);
            cmd.Parameters.AddWithValue("@time", data.Time);
            cmd.Parameters.AddWithValue("@ask", data.Ask);
            cmd.Parameters.AddWithValue("@bid", data.Bid);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();


            connection.Close();
           
        }
    }
}

