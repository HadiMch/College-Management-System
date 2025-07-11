using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CollegeManagementSystem
{
    public class BaseForm : Form
    {
        
        protected SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1M896PO\SQLEXPRESS;Initial Catalog=CollegeDB;Integrated Security=True");

        

        protected void NavigateToStudents()
        {
            frmStudents obj = new frmStudents();
            obj.Show();
            this.Hide();
        }

        protected void NavigateToFees()
        {
            frmFeesDeposite obj = new frmFeesDeposite();
            obj.Show();
            this.Hide();
        }

        protected void NavigateToDepartments()
        {
            frmDepartments obj = new frmDepartments();
            obj.Show();
            this.Hide();
        }

        protected void Logout()
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        protected void GoToDashboard()
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            this.ClientSize = new System.Drawing.Size(278, 244);
            this.Name = "BaseForm";
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.ResumeLayout(false);

        }

        private void BaseForm_Load(object sender, EventArgs e)
        {

        }

        protected void GoToTeacher()
        {
            Teacher obj = new Teacher();
            obj.Show();
            this.Hide();
        }
    }
}
