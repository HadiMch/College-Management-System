using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CollegeManagementSystem
{
    public partial class frmStudents : BaseForm
    {
        private int studentID = 0;
     
        public frmStudents()
        {
            InitializeComponent();
        }

        private void frmStudents_Load(object sender, EventArgs e)
        {
            FillDepartment();
            DisplayStudents();
        }

        private void FillDepartment()
        {
            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();
                string qr = "SELECT DepID, DeptName FROM tblDepartment";
                SqlCommand cmd = new SqlCommand(qr, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                cmbDepartment.DataSource = dt;
                cmbDepartment.DisplayMember = "DeptName";
                cmbDepartment.ValueMember = "DepID";
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

        private void DisplayStudents()
        {
            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();
                string query = @"
                    SELECT s.StdID, s.Stdname, s.StdGender, s.StdDOB, s.StdPhone,
                           s.StdDept, d.DeptName, s.StdFees
                    FROM tblStudent s
                    JOIN tblDepartment d ON s.StdDept = d.DepID";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DGVStudents.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void Reset()
        {
            txtStudentName.Clear();
            txtPhone.Clear();
            cmbGender.SelectedIndex = -1;
            cmbDepartment.SelectedIndex = -1;
            dtpDOB.Value = DateTime.Now;
            txtFees.Clear();
            studentID = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtFees.Text) ||
                cmbGender.SelectedIndex == -1 ||
                cmbDepartment.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();
                string query = @"
                    INSERT INTO tblStudent (Stdname, StdGender, StdDOB, StdPhone, StdDept, StdFees)
                    VALUES (@Name, @Gender, @DOB, @Phone, @Dept, @Fees)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtStudentName.Text.Trim());
                cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@DOB", dtpDOB.Value.Date);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@Dept", cmbDepartment.SelectedValue);
                cmd.Parameters.AddWithValue("@Fees", Convert.ToInt32(txtFees.Text.Trim()));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Student saved successfully.");
                DisplayStudents();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving student: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (studentID == 0)
            {
                MessageBox.Show("Please select a student to update.");
                return;
            }

            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();
                string query = @"
                    UPDATE tblStudent 
                    SET Stdname = @Name, StdGender = @Gender, StdDOB = @DOB,
                        StdPhone = @Phone, StdDept = @Dept, StdFees = @Fees
                    WHERE StdID = @ID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtStudentName.Text.Trim());
                cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@DOB", dtpDOB.Value.Date);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@Dept", cmbDepartment.SelectedValue);
                cmd.Parameters.AddWithValue("@Fees", Convert.ToInt32(txtFees.Text.Trim()));
                cmd.Parameters.AddWithValue("@ID", studentID);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Student updated successfully.");
                DisplayStudents();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating student: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (studentID == 0)
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
                    string query = "DELETE FROM tblStudent WHERE StdID = @ID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", studentID);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Student deleted successfully.");
                    DisplayStudents();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting student: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }



        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void DGVStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; 

            var row = DGVStudents.Rows[e.RowIndex];
            if (row.Cells["StdID"].Value == null) return; 

            studentID = int.Parse(row.Cells["StdID"].Value.ToString());
            txtStudentName.Text = row.Cells["Stdname"].Value.ToString();
            cmbGender.Text = row.Cells["StdGender"].Value.ToString();
            dtpDOB.Value = DateTime.Parse(row.Cells["StdDOB"].Value.ToString());
            txtPhone.Text = row.Cells["StdPhone"].Value.ToString();
            cmbDepartment.SelectedValue = row.Cells["StdDept"].Value;
            txtFees.Text = row.Cells["StdFees"].Value.ToString();

        }

        private void btnToTeachers_Click(object sender, EventArgs e)
        {
            GoToTeacher();
        }

        private void btnToFees_Click(object sender, EventArgs e)
        {
            NavigateToFees();
        }

        private void btnToDepartments_Click(object sender, EventArgs e)
        {
            NavigateToDepartments();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            GoToDashboard();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnToStudents_Click(object sender, EventArgs e)
        {
            NavigateToStudents();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void DGVStudents_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
