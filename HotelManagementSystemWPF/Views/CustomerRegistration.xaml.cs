using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace HotelManagementSystemWPF.Views
{
    /// <summary>
    /// Interaction logic for CustomerRegistration.xaml
    /// </summary>
    public partial class CustomerRegistration : UserControl
    {


        String connectionString = "Data Source=vishzz-laptop;Integrated Security=True;Encrypt=False";
        public CustomerRegistration()
        {
            InitializeComponent();


        }
        public void setComboBox(String query, ComboBox combo)
        {

        }





        private void alloteRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String customerName = txtFullName.Text;
                String mobile = txtNobileNo.Text;
                String nationality = txtNationality.Text;
                String gender = cmbGender.Text;
                String dob = dpDateOfBirth.Text;
                DateTime checkIn = dpCheckIn.SelectedDate.Value;
                String idProof = txtIdProof.Text;
                String address = txtAddress.Text;
                
                String roomNo = cmbRoomNo.Text;
                String roomID = ""; // Initialize to prevent null errors

                if (!string.IsNullOrWhiteSpace(customerName) &&
                    !string.IsNullOrWhiteSpace(mobile) &&
                    !string.IsNullOrWhiteSpace(nationality) &&
                    !string.IsNullOrWhiteSpace(gender) &&
                    !string.IsNullOrWhiteSpace(dob) &&
                    !string.IsNullOrWhiteSpace(idProof) &&
                    !string.IsNullOrWhiteSpace(address)  &&
                    !string.IsNullOrWhiteSpace(roomNo)&&(dpCheckIn.SelectedDate.HasValue))
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();  // Open connection before executing queries

                        // Retrieve Room ID from Database
                        using (SqlCommand cmd1 = new SqlCommand("SELECT roomID FROM rooms WHERE roomNo = @v1;", conn))
                        {
                            cmd1.Parameters.AddWithValue("@v1", roomNo);
                            using (SqlDataReader reader = cmd1.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    roomID = reader["roomID"].ToString();
                                }
                            }
                        }

                        // Debug: Confirm Room ID is retrieved before inserting
                        if (string.IsNullOrEmpty(roomID))
                        {
                            MessageBox.Show("Room ID could not be found!");
                            return; // Exit function if no room ID found
                        }

                        // Insert customer details into the database
                        using (SqlCommand command = new SqlCommand("INSERT INTO Customer (customerName, mobile, nationality, gender, dob, idProof, email, checkIn, roomID) VALUES (@v1, @v2, @v3, @v4, @v5, @v6, @v7, @v8, @v9);", conn))
                        {
                            command.Parameters.AddWithValue("@v1", customerName);
                            command.Parameters.AddWithValue("@v2", mobile);
                            command.Parameters.AddWithValue("@v3", nationality);
                            command.Parameters.AddWithValue("@v4", gender);
                            command.Parameters.AddWithValue("@v5", dob);
                            command.Parameters.AddWithValue("@v6", idProof);
                            command.Parameters.AddWithValue("@v7", address);
                            command.Parameters.AddWithValue("@v8", checkIn);
                            command.Parameters.AddWithValue("@v9", roomID); // Ensure roomID is populated

                            int indicator = command.ExecuteNonQuery();

                            if (indicator > 0)
                            {
                                MessageBox.Show("Room Allotted Successfully");
                                MailMessage mail = new MailMessage();
                                mail.From = new MailAddress("shanmax18913@gmail.com");
                                mail.To.Add(address);
                                mail.Subject = "Booking Successfully";
                                mail.Body = $"Dear {customerName},\n\n" +
                                            $"Welcome to our hotel! You have successfully checked in.\n\n" +
                                            $"Room Number: {roomNo}\n" +
                                            $"Check-in Date: {checkIn.ToShortDateString()}\n\n" +
                                            $"We hope you enjoy your stay!\n\nBest regards,\nSCR Hotel"; ;

                                // SMTP server settings
                                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); // e.g., smtp.gmail.com
                                smtp.Credentials = new NetworkCredential("shanmax18913@gmail.com", "qsiu qaig ufdf safe");
                                smtp.EnableSsl = true; // Use SSL if required

                                try
                                {
                                    smtp.Send(mail);
                                    MessageBox.Show("Email sent successfully!","Done",MessageBoxButton.OK,MessageBoxImage.Information);
                                    
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error sending email: " + ex.Message);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Room Allotment Failed");
                            }

                            SqlCommand cmd2 = new SqlCommand("UPDATE rooms SET booked = 'YES' WHERE roomID = @v1;",conn);
                            cmd2.Parameters.AddWithValue("@v1",roomID);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please fill in all required fields.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }



            checkOut co = new checkOut();
            co.Load_Data();


            Clear();




        }
    
        



        private void cmbRoomNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
        public void Clear() {

           txtAddress.Text = "";
            txtFullName.Text = "";
            txtIdProof.Text = "";
            txtNobileNo.Text = "";
            txtNationality.Text = "";
            txtPrice.Text = "";
            txtNobileNo.Text = "";
            cmbRoomNo.SelectedIndex = -1;
            cmbGender.SelectedIndex = -1;
            cmbBed.SelectedIndex = -1;
            RoomType.SelectedIndex = -1;
            cmbBed.SelectedIndex = -1;
            dpCheckIn.SelectedDate = null;
            dpDateOfBirth.SelectedDate = null;
            

        }

        private void txtFullName_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void RoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           


        }
        private SqlCommand SqlCommand(string v, SqlConnection conn)
        {
            throw new NotImplementedException();
        }

        private void btnSearchRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cmbRoomNo.Items.Clear();
                String roomNo, bed, roomType, price, booked;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM rooms;", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            
                            while (reader.Read())
                            {
                                roomNo = reader["roomNo"].ToString();
                                roomType = reader["roomType"].ToString().ToLower();
                                bed = reader["bed"].ToString().ToLower();
                                price = reader["price"].ToString();
                                booked = reader["booked"].ToString().ToLower();

                                
                                

                                string selectedRoomType = ((ComboBoxItem)RoomType.SelectedItem).Content.ToString().ToLower();
                                
                                string selectedBedType = ((ComboBoxItem)cmbBed.SelectedItem).Content.ToString().ToLower();

                                // Ensure combo box selections are not null
                                if (selectedRoomType != "" && selectedBedType != "")
                                {
                                    
                                    if (booked == "no" && selectedRoomType == roomType )
                                    {
                                        
                                        if (selectedBedType == bed) {

                                            
                                            cmbRoomNo.Items.Add(roomNo);
                                            
                                        }
                                        
                                            
                                        
                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSet_Price(object sender, RoutedEventArgs e)
        {

            try
            {
                int roomNo =Convert.ToInt32(cmbRoomNo.Text);
                

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open(); // Ensure the connection is opened before executing commands
                    String price;
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM rooms WHERE roomNo = @v1;", conn)) // Correctly create SqlCommand
                    {
                        cmd.Parameters.AddWithValue("@v1", roomNo.ToString()); // Use parameters to prevent SQL injection
                        using (SqlDataReader reader = cmd.ExecuteReader()) // ExecuteReader() is used to get a SqlDataReader
                        {
                            // Process the data from the reader if needed
                            if (reader.Read())
                            {
                               
                                price = reader["price"].ToString();
                                txtPrice.Text = price;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
