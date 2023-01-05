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
    public partial class score : Form
    {
        Turmas turm = new Turmas();
        Nota notas = new Nota();    
        SudentDb student=new SudentDb();
        Curso curso = new Curso();
        public score()
        {
            InitializeComponent();
        }
        private void LimpaCampos()
        {
           
            comboBox3.DataSource = null;
            comboBox4.DataSource = null;

        }
        private void things()
        {
            dataGridView_C.DataSource = notas.GetNotas(new MySqlCommand("SELECT score.ScoreId ,student.StdId,student.StdFirstName,student.StdLastName,score.DisciplinasName,score.Nota,score.TurmaNome from student JOIN  score on student.StdId=score.StudentId"));

            comboBox1.DataSource = turm.GetTurma(new MySqlCommand("SELECT DISTINCT TurmaNome from turma"));
            comboBox1.DisplayMember = "TurmaNome";
            comboBox1.ValueMember = "TurmaNome";
            guna2ComboBox3.DataSource = turm.GetTurma(new MySqlCommand("SELECT DISTINCT TurmaNome from turma"));
            guna2ComboBox3.DisplayMember = "TurmaNome";
            guna2ComboBox3.ValueMember = "TurmaNome";
        }

        private void score_Load(object sender, EventArgs e)
            {
                 things();
            dataGridView_C.Columns[0].HeaderText = "  Id Nota ";
            dataGridView_C.Columns[1].HeaderText = " Student Id";
            dataGridView_C.Columns[2].HeaderText = "Primeiro Nome";
            dataGridView_C.Columns[3].HeaderText = "Ultimo Nome";
            dataGridView_C.Columns[4].HeaderText = "Disciplina";
            dataGridView_C.Columns[5].HeaderText = "Nota";
            dataGridView_C.Columns[6].HeaderText = "Turma";



        }

 
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                int stdid = Convert.ToInt32(guna2ComboBox5.Text);
                string disame = comboBox3.Text;
                double not = Convert.ToDouble(guna2TextBox1.Text);
                string turma = comboBox1.Text;
                if (!notas.checkScore(stdid, disame))
                {
                    if (notas.InsertNota(stdid, disame, not, turma))
                    {

                        LimpaCampos();
                        things();
                        MessageBox.Show("Novo Aluno inserido e turma inserido", "Adicionar Aluno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Este aluno já tem nota", "Adicionar nota", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Campos obrigatoris", "eRRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            dataGridView_C.DataSource = student.getStudentlist(new MySqlCommand("SELECT student.StdId,student.StdFirstName,student.StdLastName from student"));

        }

        private void btn_mostrarT_Click(object sender, EventArgs e)
        {
            dataGridView_C.DataSource = notas.GetNotas(new MySqlCommand("SELECT score.ScoreId ,student.StdId,student.StdFirstName,student.StdLastName,score.DisciplinasName,score.Nota,score.TurmaNome from student JOIN  score on student.StdId=score.StudentId"));

        }

        private void button_search_Click(object sender, EventArgs e)
        {
            dataGridView_C.DataSource = turm.SearchTurma(guna2ComboBox3.Text);
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            dataGridView_C.DataSource = notas.GetNotas(new MySqlCommand("SELECT score.ScoreId ,student.StdId,student.StdFirstName,student.StdLastName,score.DisciplinasName,score.Nota,score.TurmaNome from student JOIN  score on student.StdId=score.StudentId"));

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(guna2TextBox2.Text);
                //Show a confirmation message before delete the student
                if (MessageBox.Show("Tem a certeza que quer remover a nota ?", "Remover Turma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (notas.DeleteNota(id))
                    {

                        LimpaCampos();
                        dataGridView_C.DataSource = notas.GetNotas(new MySqlCommand("SELECT score.ScoreId ,student.StdId,student.StdFirstName,student.StdLastName,score.DisciplinasName,score.Nota,score.TurmaNome from student JOIN  score on student.StdId=score.StudentId"));

                        MessageBox.Show("Nota Removida com sucesso", "Remover Nota", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Nota nao Removida ", "Remover Nota", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {

        }

        private void Combox_T_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                guna2ComboBox4.DataSource = turm.Searchlastname(Combox_T.Text);
                guna2ComboBox4.DisplayMember = "StdLastName";
                guna2ComboBox4.ValueMember = "StdLastName";
                guna2ComboBox5.DataSource = turm.SearchId(Combox_T.Text, guna2ComboBox4.Text);
                guna2ComboBox5.DisplayMember = "StdId";
                guna2ComboBox5.ValueMember = "StdId";
            
        }

    
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBox3.DataSource = notas.SearchDisciplina(comboBox4.Text);
            comboBox3.DisplayMember = "DisciplinasName";
            comboBox3.ValueMember = "DisciplinasName";


            comboBox4.DataSource = notas.getcurso(comboBox1.Text    ); ;
            comboBox4.DisplayMember = "Cursonome";
            comboBox4.ValueMember = "Cursonome";
            Combox_T.DataSource = notas.SearchFirstname(comboBox1.Text);
            Combox_T.DisplayMember = "StdFirstName";
            Combox_T.ValueMember = "StdFirstName";
            guna2ComboBox4.DataSource = turm.Searchlastname(Combox_T.Text);
            guna2ComboBox4.DisplayMember = "StdLastName";
            guna2ComboBox4.ValueMember = "StdLastName";
            guna2ComboBox5.DataSource = turm.SearchId(Combox_T.Text, guna2ComboBox4.Text);
            guna2ComboBox5.DisplayMember = "StdId";
            guna2ComboBox5.ValueMember = "StdId";
        }

        private void dataGridView_C_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { 
            guna2TextBox2.Text = dataGridView_C.CurrentRow.Cells[0].Value.ToString();
            guna2ComboBox5.Text = dataGridView_C.CurrentRow.Cells[1].Value.ToString();
            Combox_T.Text = dataGridView_C.CurrentRow.Cells[2].Value.ToString();
            guna2ComboBox4.Text = dataGridView_C.CurrentRow.Cells[3].Value.ToString();
            comboBox3.Text = dataGridView_C.CurrentRow.Cells[4].Value.ToString();
            guna2TextBox1.Text= dataGridView_C.CurrentRow.Cells[5].Value.ToString();
            comboBox1.Text= dataGridView_C.CurrentRow.Cells[6].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Click na tabela notas", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2ComboBox4.DataSource = turm.Searchlastname(Combox_T.Text);
            guna2ComboBox4.DisplayMember = "StdLastName";
            guna2ComboBox4.ValueMember = "StdLastName";
            guna2ComboBox5.DataSource = turm.SearchId(Combox_T.Text, guna2ComboBox4.Text);
            guna2ComboBox5.DisplayMember = "StdId";
            guna2ComboBox5.ValueMember = "StdId";
        }

        private void Combox_T_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            guna2ComboBox4.DataSource = turm.Searchlastname(Combox_T.Text);
            guna2ComboBox4.DisplayMember = "StdLastName";
            guna2ComboBox4.ValueMember = "StdLastName";
            guna2ComboBox5.DataSource = turm.SearchId(Combox_T.Text, guna2ComboBox4.Text);
            guna2ComboBox5.DisplayMember = "StdId";
            guna2ComboBox5.ValueMember = "StdId";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double scor = Convert.ToDouble(guna2TextBox1.Text);
                int stdid = Convert.ToInt32(guna2ComboBox5.Text);
                string dis = comboBox3.Text;
                if (notas.UpdateNota(scor, stdid, dis))
                {
                    things();
                    MessageBox.Show("Score Edited Complete", "Update Score", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch
            {
                MessageBox.Show("Score not edit", "Update Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_C_CellBorderStyleChanged(object sender, EventArgs e)
        {

        }
    }
}
///faxer ineer  join abelaa score com studente