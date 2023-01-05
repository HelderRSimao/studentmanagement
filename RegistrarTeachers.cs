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
    public partial class RegistrarTeachers : Form
    {
        Teachersbd teacher = new Teachersbd();

        #region
        public void ShowTable() //Para mostar a a tabela de  prof
        {
            dataGridView_T.DataSource = teacher.getTeacherlist(new MySqlCommand("SELECT * FROM `teacher`"));
           
        }
        bool verify()//criaçao de uma funçaa para verifivar se esta preenchhido as textboxes
        {
            if ((textBox_Fname.Text == "") || (textBox_Lname.Text == "") )
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

            radioButton_male.Checked = true;
            dateTimePicker1.Value = DateTime.Now;

        }
        #endregion
        public RegistrarTeachers()
        {
            InitializeComponent();
            textBox_id.Enabled = false;

        }

        private void RegistrarTeachers_Load(object sender, EventArgs e)
        {
            ShowTable();
            dataGridView_T.Columns[0].HeaderText = "Id";
            dataGridView_T.Columns[1].HeaderText = "Primeiro Nome";
            dataGridView_T.Columns[2].HeaderText = "Ultimo Nome";
            dataGridView_T.Columns[3].HeaderText = "Sexo";
            dataGridView_T.Columns[4].HeaderText = "Data de Nascimento";
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            //add new proff«
            String Tname = textBox_Fname.Text;
            String Tlname = textBox_Lname.Text;
            DateTime bdate = dateTimePicker1.Value;
            string gender = radioButton_male.Checked ? "Male" : "Female";
            //ver se a idade de estudante esta entre 10 e 100
            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 10 || (this_year - born_year) > 100)
            {
                MessageBox.Show("A idade do professor tem que ser entre 10 and 100 ", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (verify())
            {
                try
                {
                
                  

                    if (teacher.InsertTeacher(Tname, Tlname,  gender, bdate))
                    {
                        ShowTable();
                        LimpaCampos();
                        MessageBox.Show("Professor adicionado com sucesso", "Adicionar Professor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Campo vazioo", "Adicionar Professor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox_Fname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);//so permite  letras
        }

        private void textBox_Lname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);//so permite  letras
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
        
            //ver se a idade de estudante esta entre 10 e 100
            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 10 || (this_year - born_year) > 100)
            {
                MessageBox.Show("A idade do professor tem que ser entre 10 and 100 ", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (verify())
            {
                try

                {
                    textBox_id.Enabled = false;

                    int id = Convert.ToInt32(textBox_id.Text);

                    String Tname = textBox_Fname.Text;
                    String Tlname = textBox_Lname.Text;
                    DateTime bdate = dateTimePicker1.Value;
                    string gender = radioButton_male.Checked ? "Male" : "Female";
                    if (teacher.UpdateTeacher(id,Tname, Tlname, gender, bdate))
                    {
                        ShowTable();
                        LimpaCampos();
                        MessageBox.Show("Professor updated com sucesso", "Update Professor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Campo vazioo", "Update  Professor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            
        }

        private void dataGridView_T_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_id.Text = dataGridView_T.CurrentRow.Cells[0].Value.ToString();
            textBox_Fname.Text = dataGridView_T.CurrentRow.Cells[1].Value.ToString();
            textBox_Lname.Text = dataGridView_T.CurrentRow.Cells[2].Value.ToString();
            
            if (dataGridView_T.CurrentRow.Cells[3].Value.ToString() == "Male")
            {
                radioButton_male.Checked = true;
            }
            dateTimePicker1.Value = (DateTime)dataGridView_T.CurrentRow.Cells[4].Value;
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                //remove the selected Student
                int id = Convert.ToInt32(textBox_id.Text);
                //Show a confirmation message before delete the student
                if (MessageBox.Show("Tem a certeza que quer remover o Professor ?", "Remover Professor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (teacher.DeleteTeacher(id))
                    {
                        ShowTable();
                        LimpaCampos();
                        MessageBox.Show("Professor Removido com sucesso", "Remover Professor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button_clear.PerformClick();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Campo vazio", "Remover Professor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            dataGridView_T.DataSource = teacher.SearchTeacher(textBox_search.Text);
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();

        }

        
    }
}