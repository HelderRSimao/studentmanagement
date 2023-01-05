using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studentmanagement.Allclases
{
    public class Disciplinadb:Dbconnect
    {
        Dbconnect connect = new Dbconnect();
        public bool InsertCourse(string cname, int dur, int idt )//fuction to insert course
                                                                   
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `disciplinas`( `DisciplinasName`, `CourseHour`, `TeacherId`) VALUES (@cn,@ch,@id)", connect.GetConnection);
            //@cn,@ch,@desc
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = cname;
            command.Parameters.Add("@ch", MySqlDbType.Int32).Value = dur;
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = idt;
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
        public DataTable GetCourse(MySqlCommand command)//to get disciplina table 
        {
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public bool DeletCourse(int id)// fUNÇAO PARA DELETAR
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `disciplinas` WHERE `CourseId`=@id", connect.GetConnection);
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
        public bool UpdateCourse(int id,string cname, int dur, int idt)//funçao para update
        {
            MySqlCommand command = new MySqlCommand("UPDATE `disciplinas` SET `CourseId`=@id,`DisciplinasName`=@cn,`CourseHour`=@hr,`TeacherId`= @it WHERE `CourseId`=@id", connect.GetConnection);
            //@id,@cn,@ch,@desc
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = cname;
            command.Parameters.Add("@hr", MySqlDbType.Int32).Value = dur;
            command.Parameters.Add("@it", MySqlDbType.Int32).Value = idt;
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
       
        public DataTable SearchId(string searchdata)//to get student table para  id
        {
            MySqlCommand command = new MySqlCommand("SELECT teacher.TeacherLastName from teacher where teacher.TeacherName = " + searchdata + " %'", connect.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
