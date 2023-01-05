using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace studentmanagement.Allclases
{
 public class SudentDb : Dbconnect
    {
        //criaçao da funçao pra  adicionar  novo estudantes na base de dados
        Dbconnect connect = new Dbconnect();
        public bool InsertStudent(string fname, string lname, DateTime bdate, string gender, string phone,  byte[] img)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `student`( `StdFirstName`, `StdLastName`, `Birthdate`, `Gender`, `Phone`,`Photo`) VALUES(@fn, @ln, @bd, @gd, @ph, @img)", connect.GetConnection);
            //@fn, @ln, @bd, @gd, @ph, @adr, @img neste comando iremos relacionar os nomes
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;
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
        public DataTable getStudentlist(MySqlCommand command)//to get student table//mudei isto
        {
            //MySqlCommand command = new MySqlCommand("SELECT * FROM `student`",connect.GetConnection);
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;





        }
        //Create a function to execute the count   query(total, male,female)
        public string exeCount(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connect.GetConnection);
            connect.OpenConnect();
            string count = command.ExecuteScalar().ToString();
            connect.CloseConnect();
            return count;
        }
        //to get the total student
     
        //Create a function  for student (first name ,last name, adress)
        public DataTable SearchStudent(string searchdata)//to get student table
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student` Where Concat(`StdId`,`StdFirstName`,`StdLastName`) LIKE '%" + searchdata + "%'", connect.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public bool UpdateStudent(int id, string fname, string lname, DateTime bdate, string gender, string phone, byte[] img) //Criaçao da funçao para update da stuent
        {
            MySqlCommand command = new MySqlCommand("UPDATE `student` SET `StdFirstName`=@fn,`StdLastName`=@ln,`Birthdate`=@bd,`Gender`=@gd,`Phone`=@ph,`Photo`=@img WHERE `StdId`=@id", connect.GetConnection);
            //@fn, @ln, @bd, @gd, @ph, @adr, @img neste comando iremos relacionar os nomes
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;
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
        public bool deleteStudent(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `student` WHERE `StdId`=@id", connect.GetConnection);

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
        // create a function for any command in studentDb
        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}

