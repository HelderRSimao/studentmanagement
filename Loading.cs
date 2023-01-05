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
    public partial class Loading : Form
    {

        public Loading()
        {
            InitializeComponent();

        }
        private void Loading_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        int startpoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 1;
            ProgressIndicator1.Start();
            if (startpoint > 40)
            {
                Registrarstudent registrarstudent = new Registrarstudent();
                ProgressIndicator1.Stop();
                timer1.Stop();
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }


    }
}
