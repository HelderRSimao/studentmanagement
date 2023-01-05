using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace studentmanagement.Allclases
{
    public class Dbconnect
    {
        // Nesta classe  irie criar conneccao com entre a aplicçao e bd
        MySqlConnection connect = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=studentdb");

        public MySqlConnection GetConnection
        {
            get { return connect; }
        }
        // create  a funlao paraa  abrir connection
        public void OpenConnect()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
            {
                connect.Open();
            }
        }
        // create  a funlao paraa  fechar connection
        public void CloseConnect()
        {
            if (connect.State == System.Data.ConnectionState.Open)
            {
                connect.Close();
            }
        }
    }
}
