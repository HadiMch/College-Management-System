using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CollegeManagementSystem
{
    public partial class frmFeesDeposite : BaseForm
    {
        private int FID = 0;
        
        public frmFeesDeposite()
        {
            InitializeComponent();
        }

        private void frmFeesDeposite_Load(object sender, EventArgs e)
        {
            LoadFeesData(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GoToDashboard();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            GoToTeacher();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NavigateToStudents();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NavigateToFees();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NavigateToDepartments();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            int studentID;
            int paymentAmount;

            if (!int.TryParse(txtID.Text, out studentID) || !int.TryParse(txtAmount.Text, out paymentAmount))
            {
                MessageBox.Show("Please enter valid numbers for ID and Amount.");
                return;
            }

            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                
                SqlCommand getStudentCmd = new SqlCommand("SELECT StdFees FROM tblStudent WHERE StdID = @StdID", con);
                getStudentCmd.Parameters.AddWithValue("@StdID", studentID);
                object result = getStudentCmd.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("Student not found.");
                    return;
                }

                int currentBalance = Convert.ToInt32(result);

                if (currentBalance <= 0)
                {
                    MessageBox.Show("This student has already paid all fees.");
                    return;
                }

                if (paymentAmount > currentBalance)
                {
                    MessageBox.Show("Payment amount exceeds the outstanding balance.");
                    return;
                }

                SqlCommand insertPaymentCmd = new SqlCommand("INSERT INTO tblFees (StdID, stdname, Fdate, Amount) VALUES (@StdID, @StdName, @Fdate, @Amount)", con);
                insertPaymentCmd.Parameters.AddWithValue("@StdID", studentID);
                insertPaymentCmd.Parameters.AddWithValue("@StdName", txtName.Text);
                insertPaymentCmd.Parameters.AddWithValue("@Fdate", dtpDate.Value);
                insertPaymentCmd.Parameters.AddWithValue("@Amount", paymentAmount);
                insertPaymentCmd.ExecuteNonQuery();

                
                int newBalance = currentBalance - paymentAmount;
                SqlCommand updateStudentCmd = new SqlCommand("UPDATE tblStudent SET StdFees = @NewBalance WHERE StdID = @StdID", con);
                updateStudentCmd.Parameters.AddWithValue("@NewBalance", newBalance);
                updateStudentCmd.Parameters.AddWithValue("@StdID", studentID);
                updateStudentCmd.ExecuteNonQuery();

                MessageBox.Show("Payment recorded successfully. Remaining balance: " + newBalance);

                LoadFeesData(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DGVFeesDeposite_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DGVFeesDeposite_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void LoadFeesData()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM tblFees", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DGVFeesDeposite.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }
    }
}
