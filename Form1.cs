using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RedoEventAttendees
{
    public partial class Form1 : Form
    {
        // Connection string pointing to your local SQL Server database 'eventdb'
        string connectionString = "Data Source=localhost;Initial Catalog=eventdb;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        // Runs automatically when the Form loads
        [Obsolete]
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTableData();
        }

        // Helper method to load database records into your DataGridView
        [Obsolete]
        private void LoadTableData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM newtab";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception)
            {
                // Silence or log errors if database isn't reached immediately on boot
            }
        }

        // INSERT Button (or Save Button)
        [Obsolete]
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO newtab (AttendeeId, AttendeeName, RowNumber, Status) VALUES (@AttendeeId, @AttendeeName, @RowNumber, @Status)";

                    using (SqlCommand cnn = new SqlCommand(query, con))
                    {
                        cnn.Parameters.AddWithValue("@AttendeeId", int.Parse(textBox1.Text));
                        cnn.Parameters.AddWithValue("@AttendeeName", textBox2.Text);
                        cnn.Parameters.AddWithValue("@RowNumber", textBox3.Text);
                        cnn.Parameters.AddWithValue("@Status", textBox4.Text);

                        cnn.ExecuteNonQuery();
                        MessageBox.Show("Record Inserted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearInputs();
                        LoadTableData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper to clear textboxes after operations
        private void ClearInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        [Obsolete]
        private void button3_Click(object sender, EventArgs e)
        {
            // Safely check if textBox1 has a valid number before updating
            if (int.TryParse(textBox1.Text, out int attendeeId))
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string query = "UPDATE newtab SET AttendeeName = @AttendeeName, RowNumber = @RowNumber, Status = @Status WHERE AttendeeId = @AttendeeId";

                        using (SqlCommand cnn = new SqlCommand(query, con))
                        {
                            cnn.Parameters.AddWithValue("@AttendeeId", attendeeId);
                            cnn.Parameters.AddWithValue("@AttendeeName", textBox2.Text);
                            cnn.Parameters.AddWithValue("@RowNumber", textBox3.Text);
                            cnn.Parameters.AddWithValue("@Status", textBox4.Text);

                            int rowsAffected = cnn.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No record found with that Attendee ID in the database.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            ClearInputs();
                            LoadTableData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please click a row in the table or enter a valid Attendee ID before updating.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        [Obsolete]
        private void button4_Click(object sender, EventArgs e)
        {
            // A quick safety confirmation prompt so you don't delete by accident
            DialogResult dialogResult = MessageBox.Show("Do you really want to delete this attendee?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string query = "DELETE FROM newtab WHERE AttendeeId = @AttendeeId";

                        using (SqlCommand cnn = new SqlCommand(query, con))
                        {
                            cnn.Parameters.AddWithValue("@AttendeeId", int.Parse(textBox1.Text));

                            int rowsAffected = cnn.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No record found with that Attendee ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            ClearInputs();
                            LoadTableData(); // Refreshes your grid at the bottom
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            }
        }
    }
    


