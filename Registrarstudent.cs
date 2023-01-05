using MySql.Data.MySqlClient;
using studentmanagement.Allclases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//por teachers na tabkla discipplinas
namespace studentmanagement
{
    public partial class Registrarstudent : Form
    {
        //Chamar  a class estudente
        SudentDb student = new SudentDb();
        #region
        public void ShowTable() //Para mostar a a tabela de alunos
        {
            dataGridView_Std.DataSource = student.getStudentlist(new MySqlCommand("SELECT * FROM `student`"));
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)dataGridView_Std.Columns[6];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }
        bool verify()//criaçao de uma funçaa para verifivar se esta preenchhido as textboxes
        {
            if ((textBox_Fname.Text == "") || (textBox_Lname.Text == "") ||
                   (textBox_phone.Text == "") ||
                   (pictureBox_student.Image == null))
            {
                return false;
            }
            else
                return true;
        }
        private void LimpaCampos()
        {
            textBox_id.Clear();
            textBox_Fname.Clear();
            textBox_Lname.Clear();
            textBox_phone.Clear();

            radioButton_male.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            pictureBox_student.Image = null;
        }
        #endregion//Metodos
        public Registrarstudent()
        {
            InitializeComponent();
            textBox_id.Enabled = false;
        }

        private void Registrarstudent_Load(object sender, EventArgs e)
        {
            ShowTable();
            dataGridView_Std.Columns[0].HeaderText = "Id";
            dataGridView_Std.Columns[1].HeaderText = "Primeiro Nome";
            dataGridView_Std.Columns[2].HeaderText = "Ultimo Nome";
            dataGridView_Std.Columns[3].HeaderText = "Data de Nascimento";
            dataGridView_Std.Columns[4].HeaderText = "Sexo";
            dataGridView_Std.Columns[5].HeaderText = "Telefone";
        }

        private void button_add_Click(object sender, EventArgs e)
        {

            //add new student
            String fname = textBox_Fname.Text;
            String lname = textBox_Lname.Text;
            DateTime bdate = dateTimePicker1.Value;

            string phone = textBox_phone.Text;

            string gender = radioButton_male.Checked ? "Male" : "Female";


            //ver se a idade de estudante esta entre 10 e 100
            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 10 || (this_year - born_year) > 100)
            {
                MessageBox.Show("A idade tem que ser entre  10 e 100 ", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (verify())
            {
                try
                {
                    //to get  foto from picture box
                    MemoryStream ms = new MemoryStream();
                    pictureBox_student.Image.Save(ms, pictureBox_student.Image.RawFormat);
                    byte[] img = ms.ToArray();

                    if (student.InsertStudent(fname, lname, bdate, gender, phone, img))
                    {
                        ShowTable();
                        LimpaCampos();
                        MessageBox.Show("Aluno adicionado com sucessso", "Adicionar aluno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Campo vazio", "Adicionar Aluno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_upload_Click(object sender, EventArgs e)
        {
            //browse photo from your computer
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)| *.jpg;*.png;*.gif";
            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox_student.Image = Image.FromFile(opf.FileName);
        }






        private void button_clear_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                //remove the selected Student
                int id = Convert.ToInt32(textBox_id.Text);
                //Show a confirmation message before delete the student
                if (MessageBox.Show("Tem a certeza que quer remover o aluno ?", "Remover aluno", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (student.deleteStudent(id))
                    {
                        ShowTable();
                        LimpaCampos();
                        MessageBox.Show("Aluno Removido com sucesso", "Remover aluno", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
            catch
            {
                MessageBox.Show("Campo vazio", "Remover Aluno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView_Std_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_id.Text = dataGridView_Std.CurrentRow.Cells[0].Value.ToString();
            textBox_Fname.Text = dataGridView_Std.CurrentRow.Cells[1].Value.ToString();
            textBox_Lname.Text = dataGridView_Std.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Value = (DateTime)dataGridView_Std.CurrentRow.Cells[3].Value;
            if (dataGridView_Std.CurrentRow.Cells[4].Value.ToString() == "Male")
            {
                radioButton_male.Checked = true;
            }
            textBox_phone.Text = dataGridView_Std.CurrentRow.Cells[5].Value.ToString();

            byte[] img = (byte[])dataGridView_Std.CurrentRow.Cells[6].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox_student.Image = Image.FromStream(ms);
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            //Procuara student
            dataGridView_Std.DataSource = student.SearchStudent(textBox_search.Text);
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)dataGridView_Std.Columns[6];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        private void button_update_Click(object sender, EventArgs e)
        {

            //ver se a idade de estudante esta entre 10 e 100
            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 10 || (this_year - born_year) > 100)
            {
                MessageBox.Show("A idade tem que ser entre  10 and 100 ", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (verify())
            {
                try
                {

                    textBox_id.Enabled = false;
                    //add new student
                    int id = Convert.ToInt32(textBox_id.Text);
                    String fname = textBox_Fname.Text;
                    String lname = textBox_Lname.Text;
                    DateTime bdate = dateTimePicker1.Value;

                    string phone = textBox_phone.Text;

                    string gender = radioButton_male.Checked ? "Male" : "Female";
                    //to get  phone from picture box
                    MemoryStream ms = new MemoryStream();
                    pictureBox_student.Image.Save(ms, pictureBox_student.Image.RawFormat);
                    byte[] img = ms.ToArray();

                    if (student.UpdateStudent(id, fname, lname, bdate, gender, phone, img))
                    {
                        ShowTable();
                        LimpaCampos();
                        MessageBox.Show("Alune Updated com sucesso", "Update aluno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Campo vazio ", "Update  aluno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox_Fname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);//so permite  letras
        }

        private void textBox_Lname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);//so permite  letras
        }
    }
     
}
