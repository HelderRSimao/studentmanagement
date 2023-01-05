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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            openChildForm(new Registrarstudent());
        }
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            main_panel.Controls.Add(childForm);
            main_panel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

     
        private void panel_slide_Paint(object sender, PaintEventArgs e)
        {

        }

 
        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            openChildForm(new RegistrarTeachers());
        }

        private void guna2TileButton3_Click(object sender, EventArgs e)
        {
            openChildForm(new DisciplinaForm());
        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void guna2TileButton4_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageCurso());
        }

        private void guna2TileButton5_Click(object sender, EventArgs e)
        {
            openChildForm(new Turma());
        }

        private void guna2TileButton7_Click(object sender, EventArgs e)
        {
            openChildForm(new score());
        }

        private void label_totalStd_Click(object sender, EventArgs e)
        {

        }

        private void guna2TileButton10_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
           
        }

        private void guna2TileButton8_Click(object sender, EventArgs e)
        {
            openChildForm(new User());
        }

        private void guna2TileButton9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
