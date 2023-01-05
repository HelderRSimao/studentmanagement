using MySql.Data.MySqlClient;
using System.Data;

namespace studentmanagement.Allclases
{
    public class Curso
    {
        Dbconnect connect = new Dbconnect();

        public bool Insertcurson(string cname)//fuction to insert course

        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `curso`(`CursoNome`) VALUES (@cn)", connect.GetConnection);
            //@cn,@ch,@desc
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = cname;

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
        public DataTable GetCourse(MySqlCommand command)
        {
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public bool Insertdisciplina(string cname,string cdis)//fuction to insert course

        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `curso`(`CursoNome`,`DisciplinasName`) VALUES (@cn,@cdis)", connect.GetConnection);
            //@cn,@ch,@desc
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = cname;
            command.Parameters.Add("@cdis", MySqlDbType.VarChar).Value = cdis;
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
        public bool DeletCurso(string cname)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `curso` WHERE `CursoNome`=@cn", connect.GetConnection);
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = cname;
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
        public bool checkdisciplina( string cName, string tName )
        {
            DataTable table = GetCourse(new MySqlCommand("SELECT * FROM `curso` WHERE `DisciplinasName`= '" + cName + "' and `CursoNome`='" + tName + "' "));
            if (table.Rows.Count > 0)
            { return true; }
            else
            { return false; }
        }
    }
}
