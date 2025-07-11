using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CollegeManagementSystem
{
    public partial class Teacher : BaseForm
    {
        
        private int TeID = 0;

        public Teacher()
        {
            InitializeComponent();
        }

        private void Teacher_Load(object sender, EventArgs e)
        {
            DisplayTeachers();
            LoadDepartments();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            GoToDashboard();
        }

        private void btnTeacher_Click(object sender, EventArgs e)
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                cmbGender.SelectedIndex == -1 ||
                cmbDepart.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();

                string query = @"
                    INSERT INTO tblTeacher (Tname, TGender, TDOB, TPhone, TDept)
                    VALUES (@Name, @Gender, @DOB, @Phone, @Dept)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@DOB", dtpDOB.Value.Date);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@Dept", cmbDepart.SelectedValue);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Teacher saved successfully.");
                DisplayTeachers();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving Teacher: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void DisplayTeachers()
        {
            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();

                string query = @"
                    SELECT t.TID, t.Tname, t.TGender, t.TDOB, t.TPhone, d.DeptName, t.TDept
                    FROM tblTeacher t
                    JOIN tblDepartment d ON t.TDept = d.DepID";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DGVTeacher.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading teachers: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void Reset()
        {
            txtName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            cmbGender.SelectedIndex = -1;
            cmbDepart.SelectedIndex = -1;
            dtpDOB.Value = DateTime.Now;
            TeID = 0;
        }

        private void LoadDepartments()
        {
            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();

                string query = "SELECT DepID, DeptName FROM tblDepartment";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbDepart.DataSource = dt;
                cmbDepart.DisplayMember = "DeptName";
                cmbDepart.ValueMember = "DepID";
                cmbDepart.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load departments: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (TeID == 0)
            {
                MessageBox.Show("Please select a Teacher to update.");
                return;
            }

            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();
                string query = @"
                    UPDATE tblTeacher 
                    SET Tname = @Name, TGender = @Gender, TDOB = @DOB,
                        TPhone = @Phone, TDept = @Dept
                    WHERE TID = @ID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@DOB", dtpDOB.Value.Date);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@Dept", cmbDepart.SelectedValue);
                cmd.Parameters.AddWithValue("@ID", TeID);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Teacher updated successfully.");
                DisplayTeachers();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Teacher: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void DGVTeacher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = DGVTeacher.Rows[e.RowIndex];
            if (row.Cells["TID"].Value == null) return;

            TeID = int.Parse(row.Cells["TID"].Value.ToString());
            txtName.Text = row.Cells["Tname"].Value.ToString();
            cmbGender.Text = row.Cells["TGender"].Value.ToString();
            dtpDOB.Value = DateTime.Parse(row.Cells["TDOB"].Value.ToString());
            txtPhone.Text = row.Cells["TPhone"].Value.ToString();
            cmbDepart.SelectedValue = row.Cells["TDept"].Value;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (TeID == 0)
            {
                MessageBox.Show("Please select a Teacher to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this Teacher?",
                                          "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Open();
                    string query = "DELETE FROM tblTeacher WHERE TID = @ID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", TeID);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Teacher deleted successfully.");
                    DisplayTeachers();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting Teacher: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
