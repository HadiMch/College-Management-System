using System;
using System.Windows.Forms;

namespace CollegeManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Tick += new EventHandler(Timer1_Tick); 
            timer1.Interval = 100; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(5);

            if (progressBar1.Value == 100)
            {
                this.Hide();
                Login log = new Login();
                log.Show();
                timer1.Enabled = false;
            }
        }
    }
}
