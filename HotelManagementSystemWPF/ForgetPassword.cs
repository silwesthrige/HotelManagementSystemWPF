using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace HotelManagementSystemWPF
{
    public partial class ForgetPassword : Form
    {
        String connectionString = "Data Source=vishzz-laptop;Integrated Security=True;Encrypt=False";
        public ForgetPassword()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM employee WHERE LOWER(username) = @v1;", connection);
                    cmd.Parameters.AddWithValue("@v1", txtUserName.Text);
                    
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {

                        String userName = reader["username"].ToString().ToLower();
                        String email = reader["email"].ToString().ToLower();
                        reader.Close();

                        if (email == txtEmail.Text.ToLower())
                        {
                            System.Windows.MessageBox.Show("Email is correct");
                            SqlCommand cmd1 = new SqlCommand("UPDATE employee SET pass = @v1 WHERE LOWER(email) = @v2;", connection);
                            cmd1.Parameters.AddWithValue("@v2", txtEmail.Text.ToLower());
                            cmd1.Parameters.AddWithValue("@v1", txtNewPassword.Text.ToLower());
                            int indi = cmd1.ExecuteNonQuery();

                            if (indi > 0)
                            {
                                System.Windows.MessageBox.Show("Forget Password Successfull");
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("Forget Password Failed");

                            }
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            txtEmail.Clear();
            txtNewPassword.Clear();
            txtUserName.Clear();
        }

        
    }
}
