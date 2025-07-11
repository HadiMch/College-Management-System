using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CollegeManagementSystem
{
    public partial class frmDepartments : BaseForm
    {
        private int DeptID = 0;

        public frmDepartments()
        {
            InitializeComponent();
        }

        private void frmDepartments_Load(object sender, EventArgs e)
        {
            DisplayDepartments();
        }

        private void btnDashBoard_Click(object sender, EventArgs e)
        {
            GoToDashboard();
        }

        private void btnTeachers_Click(object sender, EventArgs e)
        {
            GoToTeacher();
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            NavigateToStudents();
        }

        private void btnFeesDeposite_Click(object sender, EventArgs e)
        {
            NavigateToFees();
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            NavigateToDepartments();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text) ||
                string.IsNullOrWhiteSpace(txtDuration.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();

                string query = @"
                    INSERT INTO tblDepartment (DeptName, DeptDesc, DeptDuration)
                    VALUES (@Name, @Description, @Duration)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                cmd.Parameters.AddWithValue("@Duration", Convert.ToInt32(txtDuration.Text.Trim()));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Department saved successfully.");
                DisplayDepartments();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving department: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (DeptID == 0)
            {
                MessageBox.Show("Please select a department to update.");
                return;
            }

            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();

                string query = @"
                    UPDATE tblDepartment 
                    SET DeptName = @Name, DeptDesc = @Description, DeptDuration = @Duration
                    WHERE DepID = @ID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                cmd.Parameters.AddWithValue("@Duration", Convert.ToInt32(txtDuration.Text.Trim()));
                cmd.Parameters.AddWithValue("@ID", DeptID);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Department updated successfully.");
                DisplayDepartments();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating department: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void DGVDepartment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; 

            DataGridViewRow row = DGVDepartment.Rows[e.RowIndex];
            if (row.Cells["DepID"].Value == null) return;

            DeptID = Convert.ToInt32(row.Cells["DepID"].Value.ToString());
            txtName.Text = row.Cells["DeptName"].Value.ToString();
            txtDescription.Text = row.Cells["DeptDesc"].Value.ToString();
            txtDuration.Text = row.Cells["DeptDuration"].Value.ToString();
        }

        private void DGVDepartment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Reset()
        {
            txtName.Clear();
            txtDuration.Clear();
            txtDescription.Clear();
            DeptID = 0;
        }



        private void DisplayDepartments()
        {
            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();

                string query = @"
                    SELECT d.DepID, d.DeptName, d.DeptDesc, d.DeptDuration
                    FROM tblDepartment d";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DGVDepartment.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading departments: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }



        private void btnDenie_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DeptID == 0)
            {
                MessageBox.Show("Please select a student to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this student?",
                                          "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Open();
                    string query = "DELETE FROM tblDepartment WHERE DepID = @ID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", DeptID);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Department deleted successfully.");
                    DisplayDepartments();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting Department: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
