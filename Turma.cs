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
    //tabela omes

    public partial class Turma : Form
    {
        Turmas turma = new Turmas();
        SudentDb student = new SudentDb();
        Curso curso = new Curso();
        public Turma()
        {
            InitializeComponent();

        }
        public void things()
        {
            guna2ComboBox3.DataSource = turma.GetTurma(new MySqlCommand("SELECT DISTINCT TurmaNome from turma"));
            guna2ComboBox3.DisplayMember = "TurmaNome";
            guna2ComboBox3.ValueMember = "TurmaNome";

            Combox_T.DataSource = student.getStudentlist(new MySqlCommand("SELECT student.StdFirstName from student;"));
            Combox_T.DisplayMember = "StdFirstName";
            Combox_T.ValueMember = "StdFirstName";
            guna2ComboBox4.DataSource = turma.Searchlastname(Combox_T.Text);
            guna2ComboBox4.DisplayMember = "StdLastName";
            guna2ComboBox4.ValueMember = "StdLastName";
            guna2ComboBox5.DataSource = turma.SearchId(Combox_T.Text, guna2ComboBox4.Text);
            guna2ComboBox5.DisplayMember = "StdId";
            guna2ComboBox5.ValueMember = "StdId";
        }


        private void Turma_Load(object sender, EventArgs e)
        {
            dataGridView_C.DataSource = student.getStudentlist(new MySqlCommand("SELECT  turma.TurmaId, turma.STId,student.StdFirstName,student.StdLastName,turma.TurmaNome,turma.Cursonome FROM student JOIN turma ON turma.STId=student.StdId;"));

            comboBox12.DataSource = turma.GetTurma(new MySqlCommand("SELECT DISTINCT TurmaNome from turma"));
            comboBox12.DisplayMember = "TurmaNome";
            comboBox12.ValueMember = "TurmaNome";

            guna2ComboBox42.DataSource = turma.Searchcurso(comboBox12.Text);
            guna2ComboBox42.DisplayMember = "CursoNome";
            guna2ComboBox42.ValueMember = "CursoNome";
            
            guna2ComboBox3.DataSource = turma.GetTurma(new MySqlCommand("SELECT DISTINCT TurmaNome from turma"));
            guna2ComboBox3.DisplayMember = "TurmaNome";
            guna2ComboBox3.ValueMember = "TurmaNome";

            things();

            dataGridView_C.Columns[0].HeaderText = "TurmaId";
            dataGridView_C.Columns[1].HeaderText = "Student id";
            dataGridView_C.Columns[2].HeaderText = "Primeiro Nome";
            dataGridView_C.Columns[3].HeaderText = "Ultimo Nome";
            dataGridView_C.Columns[4].HeaderText = "Turma";
            dataGridView_C.Columns[5].HeaderText = "Curso";


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txt_Cnome.Text == "")
            {
                MessageBox.Show("Campos obrigatoris", "eRRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string tname = txt_Cnome.Text;
                if (turma.InsertTurma(tname))
                {

                    MessageBox.Show("Nova Turma inserida", "Adicionar Turma ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBox12.DataSource = turma.GetTurma(new MySqlCommand("SELECT DISTINCT TurmaNome from turma"));
                    comboBox12.DisplayMember = "TurmaNome";
                    comboBox12.ValueMember = "TurmaNome";
                   

                }
                else
                {
                    MessageBox.Show("Turma nao inserida", "Adicionar Turma", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txt_Cnome.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox42.Text == "")
            {
                MessageBox.Show("Campos obrigatoris", "eRRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                int stdname = Convert.ToInt32(guna2ComboBox5.Text);
                string cname = guna2ComboBox42.Text;
                string tname = comboBox12.Text;
                if (!turma.checkTurma(stdname, tname))
                {
                    if (turma.InsertAlunos(stdname, cname, tname))
                    {
                        dataGridView_C.DataSource = student.getStudentlist(new MySqlCommand("SELECT  turma.TurmaId, turma.STId,student.StdFirstName,student.StdLastName,turma.TurmaNome,turma.Cursonome FROM student JOIN turma ON turma.STId=student.StdId;"));

                        MessageBox.Show("Novo Aluno inserido e turma inserido", "Adicionar Aluno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        comboBox12.DataSource = turma.GetTurma(new MySqlCommand("SELECT DISTINCT TurmaNome from turma"));
                        comboBox12.DisplayMember = "TurmaNome";
                        comboBox12.ValueMember = "TurmaNome";
                    }
                    else
                    {
                        MessageBox.Show(" Aluno nao inserido ", "Adicionar Aluno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(" Aluno ja inserido ", "Adicionar Aluno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_mostrarT_Click(object sender, EventArgs e)
        {
            button_search.Enabled = true;
            dataGridView_C.DataSource = student.getStudentlist(new MySqlCommand("SELECT  turma.TurmaId, turma.STId,student.StdFirstName,student.StdLastName,turma.TurmaNome,turma.Cursonome FROM student JOIN turma ON turma.STId=student.StdId;"));
        }
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            button_search.Enabled = false;
            dataGridView_C.DataSource = student.getStudentlist(new MySqlCommand("SELECT student.StdId,student.StdFirstName,student.StdLastName from student"));

        }

        private void button_search_Click(object sender, EventArgs e)
        {
            dataGridView_C.DataSource = turma.SearchTurma(guna2ComboBox3.Text);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox_id.Text);
                //Show a confirmation message before delete the student
                if (MessageBox.Show("Tem a certeza que quer remover a Turma ?", "Remover Turma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (turma.Deleteturma(id))
                    {

                        dataGridView_C.DataSource = student.getStudentlist(new MySqlCommand("SELECT  turma.TurmaId, turma.STId,student.StdFirstName,student.StdLastName,turma.TurmaNome,turma.Cursonome FROM student JOIN turma ON turma.STId=student.StdId;"));

                        comboBox12.DataSource = turma.GetTurma(new MySqlCommand("SELECT DISTINCT TurmaNome from turma"));
                        comboBox12.DisplayMember = "TurmaNome";
                        comboBox12.ValueMember = "TurmaNome";
                    }
                }
            }
            catch
            {
                MessageBox.Show("Selecione um id eliminar", "Remover Turma", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            dataGridView_C.DataSource = turma.GetTurma(new MySqlCommand("SELECT * FROM `turma`"));
        }

        private void Combox_T_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2ComboBox4.DataSource = turma.Searchlastname(Combox_T.Text);
            guna2ComboBox4.DisplayMember = "StdLastName";
            guna2ComboBox4.ValueMember = "StdLastName";
            guna2ComboBox5.DataSource = turma.SearchId(Combox_T.Text, guna2ComboBox4.Text);
            guna2ComboBox5.DisplayMember = "StdId";
            guna2ComboBox5.ValueMember = "StdId";
        }

        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2ComboBox5.DataSource = turma.SearchId(Combox_T.Text, guna2ComboBox4.Text);
            guna2ComboBox5.DisplayMember = "StdId";
            guna2ComboBox5.ValueMember = "StdId";
        }

        private void dataGridView_C_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox_id.Text = dataGridView_C.CurrentRow.Cells[0].Value.ToString();
                guna2ComboBox5.Text = dataGridView_C.CurrentRow.Cells[1].Value.ToString();
                Combox_T.Text = dataGridView_C.CurrentRow.Cells[2].Value.ToString();
                guna2ComboBox42.Text = dataGridView_C.CurrentRow.Cells[5].Value.ToString();
                comboBox12.Text = dataGridView_C.CurrentRow.Cells[4].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Selecione a tabele Turma", "Tabela errada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void guna2GradientButton2_Click_1(object sender, EventArgs e)
        {
            dataGridView_C.DataSource = student.getStudentlist(new MySqlCommand("SELECT  turma.TurmaId, turma.STId,student.StdFirstName,student.StdLastName,turma.TurmaNome,turma.Cursonome FROM student JOIN turma ON turma.STId=student.StdId;"));

        }

        

        private void guna2Button1_Click(object sender, EventArgs e)
        {


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2ComboBox42.Text)||guna2ComboBox42.SelectedValue == null) 
            {
                guna2ComboBox42.DataSource = curso.GetCourse(new MySqlCommand("SELECT CursoNome, GROUP_CONCAT(DisciplinasName) as Disciplinas From curso GROUP by CursoNome"));
                guna2ComboBox42.DisplayMember = "CursoNome";
                guna2ComboBox42.ValueMember = "CursoNome";
                things();
            }
            else
            {

                guna2ComboBox42.DataSource = turma.Searchcurso(comboBox12.Text);
                guna2ComboBox42.DisplayMember = "CursoNome";
                guna2ComboBox42.ValueMember = "CursoNome";
                things();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2ComboBox42.Text) || guna2ComboBox42.SelectedValue == null)
            {
                guna2ComboBox42.DataSource = curso.GetCourse(new MySqlCommand("SELECT CursoNome, GROUP_CONCAT(DisciplinasName) as Disciplinas From curso GROUP by CursoNome"));
                guna2ComboBox42.DisplayMember = "CursoNome";
                guna2ComboBox42.ValueMember = "CursoNome";
                things();

            }
            else
            {

                guna2ComboBox42.DataSource = turma.Searchcurso(comboBox12.Text);
                guna2ComboBox42.DisplayMember = "CursoNome";
                guna2ComboBox42.ValueMember = "CursoNome";
                MessageBox.Show("Esta  turma ja tem curso adciocionado","Turma",MessageBoxButtons.OK,MessageBoxIcon.Error);
                things();
            }
        }
    }
}

///e arranjar o sdearch