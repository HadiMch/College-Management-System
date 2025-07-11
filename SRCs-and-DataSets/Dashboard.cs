using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CollegeManagementSystem
{
    public partial class Dashboard : BaseForm
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            LoadDashboardStats();
        }

        private void LoadDashboardStats()
        {
            try
            {
                con.Open();


                SqlCommand cmdStudents = new SqlCommand("SELECT COUNT(*) FROM tblStudent", con);
                int studentCount = (int)cmdStudents.ExecuteScalar();
                label3.Text = studentCount.ToString();


                SqlCommand cmdTeachers = new SqlCommand("SELECT COUNT(*) FROM tblTeacher", con);
                int teacherCount = (int)cmdTeachers.ExecuteScalar();
                label4.Text = teacherCount.ToString();


                SqlCommand cmdDepartments = new SqlCommand("SELECT COUNT(*) FROM tblDepartment", con);
                int deptCount = (int)cmdDepartments.ExecuteScalar();
                label8.Text = deptCount.ToString();

             
                SqlCommand cmdFees = new SqlCommand("SELECT ISNULL(SUM(Amount), 0) FROM tblFees", con);
                int totalFees = (int)cmdFees.ExecuteScalar();
                label6.Text = totalFees.ToString("C");

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dashboard stats: " + ex.Message);
                con.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NavigateToStudents();
        }

        private void btnFeesdeposite_Click(object sender, EventArgs e)
        {
            NavigateToFees();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NavigateToDepartments();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GoToDashboard();
        }





        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnTeachers_Click(object sender, EventArgs e)
        {
            GoToTeacher();
        }
    }
}
