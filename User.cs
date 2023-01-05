using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using studentmanagement.Allclases;

namespace studentmanagement
{
    public partial class User : Form
    {
        Users users=new Users();
        public User()
        {
            InitializeComponent();
        }
        private void ShowData()
        {
            dataGridView_user.DataSource = users.GetList(new MySqlCommand("SELECT * FROM `user`"));
            dataGridView_user.Columns[0].HeaderText = "Name";
            dataGridView_user .Columns[1].HeaderText = "Password";
            dataGridView_user.Columns[2].HeaderText = "Id";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void User_Load(object sender, EventArgs e)
        {
            ShowData();
            textBox_Fname.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = guna2TextBox1.Text;
                string password = guna2TextBox2.Text;
                int id = Convert.ToInt32(textBox_Fname.Text);
                if (users.UpdateUser(username, password,id))
                {
                    dataGridView_user.DataSource = users.GetList(new MySqlCommand("SELECT * FROM `user`"));
                    MessageBox.Show("User inserido com sucesso", "User", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    dataGridView_user.DataSource = users.GetList(new MySqlCommand("SELECT * FROM `user`"));
                    MessageBox.Show("User inserido sem sucesso", "User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
        }
            catch
            {
                MessageBox.Show("Erro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                string username = guna2TextBox1.Text;
                string password = guna2TextBox2.Text;
                if (users.InsertUser(username, password))
                {
                    dataGridView_user.DataSource = users.GetList(new MySqlCommand("SELECT * FROM `user`"));
                    MessageBox.Show("User inserido com sucesso", "User", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch
            {
                MessageBox.Show("Erro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox_Fname.Text);
                if (users.deleteuser(id))
                {
                    dataGridView_user.DataSource = users.GetList(new MySqlCommand("SELECT * FROM `user`"));
                    MessageBox.Show("User elimando com sucesso", "User", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch
            {
                MessageBox.Show("Erro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_user_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2TextBox1.Text = dataGridView_user.CurrentRow.Cells[0].Value.ToString();
            guna2TextBox2.Text = dataGridView_user.CurrentRow.Cells[1].Value.ToString();
            textBox_Fname.Text = dataGridView_user.CurrentRow.Cells[2].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Clear();
            guna2TextBox2.Clear();
            textBox_Fname.Clear();
        }
    }
}



