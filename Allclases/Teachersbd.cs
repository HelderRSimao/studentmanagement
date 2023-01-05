using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studentmanagement.Allclases
{
    public class Teachersbd:Dbconnect
    {
        Dbconnect connect = new Dbconnect();
        //criaçao da funçao pra  adicionar  novo professores  na base de dados
        public bool InsertTeacher(string Tname, string Tlname,  string gender, DateTime bdate)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `teacher`( `TeacherName`, `TeacherLastName`, `Gender`, `Birthdate`) VALUES (@tn,@tl,@gd,@bd)",connect.GetConnection);

            command.Parameters.Add("@tn", MySqlDbType.VarChar).Value = Tname;
            command.Parameters.Add("@tl", MySqlDbType.VarChar).Value = Tlname;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = bdate;

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
        public DataTable getTeacherlist(MySqlCommand command)//to get student table//mudei isto
        {
            //MySqlCommand command = new MySqlCommand("SELECT * FROM `student`",connect.GetConnection);
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;



        }
        public DataTable SearchTeacher(string searchdata)//to get student table
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `teacher` WHERE Concat(`TeacherName`,`TeacherLastName`)LIKE '%" + searchdata + "%'", connect.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public bool UpdateTeacher(int id,string Tname, string Tlname, string gender, DateTime bdate)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `teacher` SET `TeacherName`= @tn,`TeacherLastName`= @tl,`Gender`= @gd,`Birthdate`= @bd WHERE `TeacherId`= @id", connect.GetConnection);
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@tn", MySqlDbType.VarChar).Value = Tname;
            command.Parameters.Add("@tl", MySqlDbType.VarChar).Value = Tlname;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = bdate;
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
        public bool DeleteTeacher(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `teacher` WHERE `TeacherId`= @id", connect.GetConnection);

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
        public DataTable Searchlastname(string searchdata)//to get student table
        {
            MySqlCommand command = new MySqlCommand("SELECT teacher.TeacherLastName from teacher where teacher.TeacherName LIKE '%" + searchdata + "%'", connect.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public DataTable SearchId(string first,string last )//to get student table para  id
        {
           
            MySqlCommand command = new MySqlCommand("SELECT teacher.TeacherId  from teacher where teacher.TeacherName LIKE '%" + first+ "%' and teacher.TeacherLastName LIKE '%" + last+ "%'",connect.GetConnection);

          MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
    
}
