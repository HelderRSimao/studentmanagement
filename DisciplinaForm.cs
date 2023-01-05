using MySql.Data.MySqlClient;
using studentmanagement.Allclases;
using System;
using System.Windows.Forms;

namespace studentmanagement
{
    public partial class DisciplinaForm : Form
    {
        Teachersbd teacherbd = new Teachersbd();
        Disciplinadb Curso = new Disciplinadb();


        public DisciplinaForm()
        {
            InitializeComponent();
        }
        public void LimpaCampos()
        {
            textBox_id.Clear();
            txt_Cnome.Clear();
            txt_H.Clear();
        }


        private void Course_Load(object sender, EventArgs e)
        {

            guna2ComboBox2.Enabled = false;
            textBox_id.Enabled = false;
            dataGridView_C.DataSource = teacherbd.getTeacherlist(new MySqlCommand("SELECT  disciplinas.CourseId,disciplinas.TeacherId,teacher.TeacherName,teacher.TeacherLastName,disciplinas.DisciplinasName,disciplinas.CourseHour FROM disciplinas JOIN teacher ON disciplinas.Teacherid=teacher.Teacherid"));
            dataGridView_C.Columns[0].HeaderText = "Id Disciplina";
            dataGridView_C.Columns[1].HeaderText = "Id Prof";
            dataGridView_C.Columns[2].HeaderText = "Primiero Nome";
            dataGridView_C.Columns[3].HeaderText = "Ultimo Nome";
            dataGridView_C.Columns[4].HeaderText = "Disciplinas";
            dataGridView_C.Columns[5].HeaderText = "Duração";
            Combox_T.DataSource = teacherbd.getTeacherlist(new MySqlCommand("SELECT teacher.TeacherNAME from teacher; ; "));
            Combox_T.DisplayMember = "TeacherName";
            Combox_T.ValueMember = "TeacherName";

            // craiar cobo first nname adn last name o last name vem do searcha last name  e depois vem teacher id where last like sdfghj
            guna2ComboBox1.DataSource = teacherbd.Searchlastname(Combox_T.Text);
            guna2ComboBox1.DisplayMember = "TeacherLastName";
            guna2ComboBox1.ValueMember = "TeacherLastName";
            guna2ComboBox2.DataSource = teacherbd.SearchId(Combox_T.Text, guna2ComboBox1.Text);
            guna2ComboBox2.DisplayMember = "TeacherId";
            guna2ComboBox2.ValueMember = "TeacherId";


        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (txt_Cnome.Text == "" || txt_H.Text == "")
            {
                MessageBox.Show("Campos obrigatoris", "eRRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {


                string cname = txt_Cnome.Text;
                int chr = Convert.ToInt32(txt_H.Text);
                int idt = Convert.ToInt32(guna2ComboBox2.Text);

                if (Curso.InsertCourse(cname, chr, idt))
                {
                    dataGridView_C.DataSource = teacherbd.getTeacherlist(new MySqlCommand("SELECT  disciplinas.TeacherId,teacher.TeacherName,teacher.TeacherLastName,disciplinas.DisciplinasName,disciplinas.CourseHour FROM disciplinas JOIN teacher ON disciplinas.Teacherid=teacher.Teacherid"));
                    LimpaCampos();
                    MessageBox.Show("Novo curso inserido", "Adicionar Curso ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Curso inserido", "Adicionar Curso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            dataGridView_C.DataSource = teacherbd.getTeacherlist(new MySqlCommand("SELECT  disciplinas.TeacherId,teacher.TeacherName,teacher.TeacherLastName,disciplinas.DisciplinasName,disciplinas.CourseHour FROM disciplinas JOIN teacher ON disciplinas.TeacherId=teacher.TeacherId;"));
        }



        private void button_delete_Click(object sender, EventArgs e)
        {

        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        private void Combox_T_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2ComboBox1.DataSource = teacherbd.Searchlastname(Combox_T.Text);
            guna2ComboBox1.DisplayMember = "TeacherLastName";
            guna2ComboBox1.ValueMember = "TeacherLastName";
            guna2ComboBox2.DataSource = teacherbd.SearchId(Combox_T.Text, guna2ComboBox1.Text);
            guna2ComboBox2.DisplayMember = "TeacherId";
            guna2ComboBox2.ValueMember = "TeacherId";
        }

        private void Combox_T_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2ComboBox2.DataSource = teacherbd.SearchId(Combox_T.Text, guna2ComboBox1.Text);
            guna2ComboBox2.DisplayMember = "TeacherId";
            guna2ComboBox2.ValueMember = "TeacherId";
        }

        private void button_update_Click(object sender, EventArgs e)
        {

            if (txt_Cnome.Text == "" || txt_H.Text == "" || textBox_id.Text.Equals(""))
            {
                MessageBox.Show("Need Course data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int id = Convert.ToInt32(textBox_id.Text);
                string cname = txt_Cnome.Text;
                int dur = Convert.ToInt32(txt_H.Text);
                int idt = Convert.ToInt32(guna2ComboBox2.Text);


                if (Curso.UpdateCourse(id, cname, dur, idt))
                {
                    

                    LimpaCampos();
                    dataGridView_C.DataSource = teacherbd.getTeacherlist(new MySqlCommand("SELECT  disciplinas.CourseId,disciplinas.TeacherId,teacher.TeacherName,teacher.TeacherLastName,disciplinas.DisciplinasName,disciplinas.CourseHour FROM disciplinas JOIN teacher ON disciplinas.Teacherid=teacher.Teacherid"));
                    MessageBox.Show("Curso  atualizado com sucesso", "Atualizar Curso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Error-Curso nao editado", "Atualizar Curso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView_C_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox_id.Text = dataGridView_C.CurrentRow.Cells[0].Value.ToString();
                Combox_T.Text = dataGridView_C.CurrentRow.Cells[2].Value.ToString();
                txt_Cnome.Text = dataGridView_C.CurrentRow.Cells[4].Value.ToString();
                txt_H.Text = dataGridView_C.CurrentRow.Cells[5].Value.ToString();
            }
            catch 
            {
                MessageBox.Show("erro", "erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button_delete_Click_1(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox_id.Text);
                if (MessageBox.Show("Tem a certeza que quer remover a Disciplina ?", "Remover Disciplina", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    if (Curso.DeletCourse(id))
                    {

                        
                        LimpaCampos();
                        dataGridView_C.DataSource = teacherbd.getTeacherlist(new MySqlCommand("SELECT  disciplinas.CourseId,disciplinas.TeacherId,teacher.TeacherName,teacher.TeacherLastName,disciplinas.DisciplinasName,disciplinas.CourseHour FROM disciplinas JOIN teacher ON disciplinas.Teacherid=teacher.Teacherid"));
                        MessageBox.Show("Disciplina Removido com sucesso", "Remover Disciplina", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error-Curso nao editado", "Atualizar Curso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
