using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studentmanagement.Allclases
{
   public class Turmas
    {
        Dbconnect connect = new Dbconnect();

        public bool InsertTurma(string tname)//fuction to insert course

        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `turma`(TurmaNome ) VALUES (@tn)", connect.GetConnection);
            //@cn,@ch,@desc
            command.Parameters.Add("@tn", MySqlDbType.VarChar).Value = tname;
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
        public bool InsertAlunos(int stdname, string cname, string tname)//fuction to insert  alunos

        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `turma`( STId,Cursonome,TurmaNome) VALUES (@cn,@cname,@tn)", connect.GetConnection);
            //@cn,@ch,@desc
            command.Parameters.Add("@cn", MySqlDbType.Int32).Value = stdname;
            command.Parameters.Add("@cname", MySqlDbType.VarChar).Value = cname;
            command.Parameters.Add("@tn", MySqlDbType.VarChar).Value = tname;
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
        public DataTable GetTurma(MySqlCommand command)
        {
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public DataTable SearchTurma(string searchdata)//to get student table
        {
           MySqlCommand command = new MySqlCommand("SELECT turma.STId,student.StdFirstName,student.StdLastName,turma.TurmaNome,turma.Cursonome FROM student JOIN turma ON turma.STId=student.StdId Where turma.TurmaNome LIKE '%" + searchdata + "%'", connect.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public bool Deleteturma(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `turma` WHERE `TurmaId`=@id", connect.GetConnection);
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
            MySqlCommand command = new MySqlCommand("SELECT student.StdLastName from student where student.StdFirstName LIKE '%" + searchdata + "%'", connect.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public DataTable SearchId(string first, string last)//to get student table para  id
        {

            MySqlCommand command = new MySqlCommand("SELECT student.StdId from student where student.StdFirstName LIKE '%" + first + "%' and student.StdLastName LIKE '%" + last + "%'", connect.GetConnection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public bool checkTurma(int stdId, string tName)
        {
            DataTable table = GetTurma(new MySqlCommand("SELECT * FROM `turma` WHERE  `STId` = '" + stdId + "' AND `TurmaNome`= '" + tName + "'"));
            if (table.Rows.Count > 0)
            { return true; }
            else
            { return false; }
        }
        public DataTable Searchcurso(string searchdata)
        {
            MySqlCommand command = new MySqlCommand("SELECT turma.Cursonome from turma where turma.TurmaNome like '%" + searchdata + "%'  and turma.Cursonome IS not null", connect.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        

    }
}
