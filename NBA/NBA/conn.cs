using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace NBA
{
    static class conn
    {
        public static string conn_string = "Server=localhost;Database=prs_projekat;Uid=root;Password=;Pooling=true;";

       public static MySqlConnection getConnection()
    {
            MySqlConnection connection = new MySqlConnection(conn_string);
            

            return connection;
    } 
    }     
}
