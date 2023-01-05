using MySql.Data.MySqlClient;
using studentmanagement.Allclases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace studentmanagement
{
    public partial class ManageCurso : Form
    {
        Disciplinadb disciplina = new Disciplinadb();
        Curso curso = new Curso();
        public ManageCurso()
        {
            InitializeComponent();
        }

        private void ManageCurso_Load(object sender, EventArgs e)
        {
            dataGridView_C.DataSource= curso.GetCourse(new MySqlCommand("SELECT CursoNome, GROUP_CONCAT(DisciplinasName SEPARATOR ';') as Disciplinas From curso GROUP by CursoNome"));
            Combox_T.DataSource = disciplina.GetCourse(new MySqlCommand("SELECT * FROM `disciplinas`"));
            Combox_T.DisplayMember = "DisciplinasName";
            Combox_T.ValueMember = "DisciplinasName";
            guna2ComboBox1.DataSource = curso.GetCourse(new MySqlCommand("SELECT CursoNome, GROUP_CONCAT(DisciplinasName) as Disciplinas From curso GROUP by CursoNome"));
            guna2ComboBox1.DisplayMember = "CursoNome";
            guna2ComboBox1.ValueMember= "CursoNome";
        }

      

        private void button_add_Click(object sender, EventArgs e)
        {
            string cname = guna2ComboBox1.Text;
            string cdis = Combox_T.Text;

            if (guna2ComboBox1.Text == "" || Combox_T.Text == "")
            {
                MessageBox.Show("Preencha o campo", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                if (!curso.checkdisciplina(cdis ,cname))
                {
                    if (curso.Insertdisciplina(cname, cdis))
                    {
                        guna2ComboBox1.DataSource = curso.GetCourse(new MySqlCommand("SELECT CursoNome, GROUP_CONCAT(DisciplinasName) as Disciplinas From curso GROUP by CursoNome"));
                        dataGridView_C.DataSource = curso.GetCourse(new MySqlCommand("SELECT CursoNome, GROUP_CONCAT(  DisciplinasName SEPARATOR ';') as DiscipliNAS From curso GROUP by CursoNome"));
                        MessageBox.Show("Novo curso inserido", "Adicionar Curso ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Curso inserido", "Adicionar Curso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Disciplina ja enseridA", "Adicionar Disciplina", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                try
                {
                    string cname = guna2ComboBox1.Text;
                if (guna2ComboBox1.Text == "")
                {
                    MessageBox.Show("Preencha o campo", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (curso.DeletCurso(cname))
                    {
                        MessageBox.Show("Elimando sem sucesso", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                    else
                    {
                        MessageBox.Show("Elimando co sucesso", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView_C.DataSource = curso.GetCourse(new MySqlCommand("SELECT CursoNome, GROUP_CONCAT(DisciplinasName SEPARATOR ';') as Disciplinas From curso GROUP by CursoNome"));
                        guna2ComboBox1.DataSource = curso.GetCourse(new MySqlCommand("SELECT CursoNome, GROUP_CONCAT(DisciplinasName) as Disciplinas From curso GROUP by CursoNome"));

                    }
                }
            }
                catch 
                {
                    MessageBox.Show("Erro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                           
            
        }
            
           
        

        private void button_clear_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txt_Cnome.Text == "")
            {
                MessageBox.Show("Preencha o campo", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                string cname = txt_Cnome.Text;
                if (curso.Insertcurson(cname))
                {
                    //ShowData();

                    MessageBox.Show("Novo curso inserido", "Adicionar Curso ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView_C.DataSource = curso.GetCourse(new MySqlCommand("SELECT CursoNome, GROUP_CONCAT(DisciplinasName SEPARATOR ';') as Disciplinas From curso GROUP by CursoNome"));

                    guna2ComboBox1.DataSource = curso.GetCourse(new MySqlCommand("SELECT CursoNome, GROUP_CONCAT(DisciplinasName) as Disciplinas From curso GROUP by CursoNome"));
                    txt_Cnome.Clear();

                }
                else
                {
                    MessageBox.Show("Curso inserido", "Adicionar Curso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

 
       

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView_C.DataSource = curso.GetCourse(new MySqlCommand("SELECT CursoNome, GROUP_CONCAT(DisciplinasName SEPARATOR ';') as Disciplinas From curso GROUP by CursoNome"));

        }

        private void dataGridView_C_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2ComboBox1.Text = dataGridView_C.CurrentRow.Cells[0].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txt_Cnome.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView_C.DataSource = curso.GetCourse(new MySqlCommand("SELECT CursoNome, GROUP_CONCAT(DisciplinasName SEPARATOR ';') as Disciplinas From curso GROUP by CursoNome"));

            guna2ComboBox1.DataSource = curso.GetCourse(new MySqlCommand("SELECT CursoNome, GROUP_CONCAT(DisciplinasName) as Disciplinas From curso GROUP by CursoNome"));
        }
    }
}
