using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studentmanagement.Allclases
{
    public class Users : Dbconnect
    {
        Dbconnect connect = new Dbconnect();
        public bool InsertUser( string username, string userpassword) //fuction to   create a new user
                                                                                 //
        {// use  char para passe patrapores numeros e letras
            MySqlCommand command = new MySqlCommand("INSERT INTO `user`( `username`, `password`) VALUES (@name,@pass)", connect.GetConnection);
            //@cn,@ch,@desc
           
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = userpassword;
            connect.OpenConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseConnect();
                return true;
            }
            else
            {
                connect.CloseConnect();
                return false;
            }
        }
        public DataTable GetList(MySqlCommand command)
        {
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public bool UpdateUser(string username, string userpassword,int id)
        {// use  char para passe patrapores numeros e letras

            MySqlCommand command = new MySqlCommand("Update`user` Set`username`=@name, `password`=@pass WHERE `userId`=@id", connect.GetConnection);
            //@cn,@ch,@desc
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = userpassword;
            connect.OpenConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseConnect();
                return true;
            }
            else
            {
                connect.CloseConnect();
                return false;
            }



        }
        public bool deleteuser(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `user` WHERE `userId`=@id", connect.GetConnection);

            //@id
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            connect.OpenConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseConnect();
                return true;
            }
            else
            {
                connect.CloseConnect();
                return false;
            }

        }
    }
}