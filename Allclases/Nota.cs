using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studentmanagement.Allclases
{
    public class Nota
    {
        Dbconnect connect = new Dbconnect();
        public bool InsertNota(int stdid, string disame, double not, string turm)//para enserir grades
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `score`(`StudentId`, `DisciplinasName`, `Nota`, `TurmaNome`) VALUES (@stdid,@dis,@not,@turm)", connect.GetConnection);
            //@stid,@cn,@sco,@desc
            command.Parameters.Add("@stdid", MySqlDbType.Int32).Value = stdid;
            command.Parameters.Add("@dis", MySqlDbType.VarChar).Value = disame;
            command.Parameters.Add("@not", MySqlDbType.Double).Value = not;
            command.Parameters.Add("@turm", MySqlDbType.VarChar).Value =turm;
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
        public DataTable SearchTurma(string searchdata)//metodo praa fazer o seachr
        {
            MySqlCommand command = new MySqlCommand("SELECT `STId`  from turma where TurmaNome   LIKE '%" + searchdata + "%'", connect.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public DataTable GetNotas(MySqlCommand command)//to get notas 
        {
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public DataTable SearchDisciplina(string searchdis)//to get student table
        {
            MySqlCommand command = new MySqlCommand("SELECT  DISTINCT DisciplinasName from curso WHERE CursoNome LIKE '%" + searchdis + "%' and DisciplinasName IS NOT NULL", connect.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public DataTable SearchCurso(string searchcur)//to get student table
        {
            MySqlCommand command = new MySqlCommand("SELECT Distinct Cursonome from turma where TurmaNome  Like '%" + searchcur + "%'", connect.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public bool DeleteNota(int id)//metodo para deletar
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `score` WHERE `ScoreId`=@id", connect.GetConnection);
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
        public DataTable SearchFirstname(string searchdata)//to get firsrt name combobox
        {
            MySqlCommand command = new MySqlCommand("SELECT DISTINCT student.StdFirstName FROM student JOIN turma ON turma.STId=student.StdId WHERE turma.TurmaNome LIKE '%" + searchdata + "%'", connect.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public DataTable Searchlastname(string searchdata)//to get last name combobox
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
        public bool checkScore(int stdId, string cName)
        {
            DataTable table = GetNotas(new MySqlCommand("SELECT * FROM `score` WHERE `StudentId`= '" + stdId + "' AND `DisciplinasName`= '" + cName + "'"));
            if (table.Rows.Count > 0)
            { return true; }
            else
            { return false; }
        }
        public bool UpdateNota( double scor, int stdid, string dis)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `score` SET `Nota`= @scor WHERE `StudentId`=@stdid  AND `DisciplinasName`=@disc", connect.GetConnection);
            
          
            command.Parameters.Add("@scor", MySqlDbType.Double).Value = scor;
            command.Parameters.Add("@stdid", MySqlDbType.Int32).Value = stdid;
            command.Parameters.Add("@disc", MySqlDbType.VarChar).Value = dis;
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
        public  DataTable getcurso (string cName)
        {

            MySqlCommand command = new MySqlCommand("SELECT  turma.Cursonome  from turma  where turma.TurmaNome LIKE '%" + cName + "%' AND turma.Cursonome IS NOT null", connect.GetConnection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

    }
}
//Criar metodo para ir bucas os aluno da turma 