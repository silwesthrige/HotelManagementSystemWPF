using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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
using EvoPdf;
using PdfSharp.Drawing;
using DGVPrinterHelper;
using System.Drawing;
using System.Reflection;
using System.Net.Mail;
using System.Net;

// Correct Usage



namespace HotelManagementSystemWPF.Views
{
    /// <summary>
    /// Interaction logic for checkOut.xaml
    /// </summary>
    public partial class checkOut : UserControl
    {
        String connectionString = "Data Source=vishzz-laptop;Integrated Security=True;Encrypt=False";
        DGVPrinter printer = new DGVPrinter();
        public checkOut()
        {
            InitializeComponent();
            Load_Data();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(20); // Set interval
            timer.Tick += Timer_Tick; // Assign event handler
            timer.Start(); // Start timer





        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            Load_Data(); // Refresh DataGrid
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT Customer.customerID, Customer.customerName, Customer.mobile, Customer.nationality, Customer.gender, Customer.dob, Customer.idProof, Customer.email, Customer.checkIn, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price FROM Customer INNER JOIN rooms ON Customer.roomID = rooms.roomID WHERE Customer.chekOut = 'NO' AND LOWER(rooms.roomNo) LIKE '%" + txtRoomNo.Text + "%';", connection))
                    {
                        // cmd.Parameters.AddWithValue("@search", txtCustomerName.Text);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgCustomers.ItemsSource = dt.DefaultView; // Correct binding
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCustomers.SelectedItem is DataRowView row)
            {
                // Set your TextBoxes here
                txtRoomNo.Text = row["roomNo"].ToString();
                txtCustomerName.Text = row["customerName"].ToString();
            }
        }
        public void Load_Data() {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT Customer.customerID,Customer.customerName,Customer.mobile,Customer.nationality,Customer.gender,Customer.dob,Customer.idProof,Customer.email,Customer.checkIn,rooms.roomNo,rooms.roomType,rooms.bed,rooms.price FROM Customer inner join rooms on Customer.roomID = rooms.roomID WHERE chekOut = 'NO';", connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgCustomers.ItemsSource = dt.DefaultView;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void sendMail(String address,String customerName,String roomNo,DateTime checkOut,double totalCharges) {
            MessageBox.Show("Room Allotted Successfully");
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("shanmax18913@gmail.com");
            mail.To.Add(address);
            mail.Subject = "Thank You for Choosing Us!!";
            mail.Body = $"Dear {customerName},\n\n" +
                        $"Thank you for staying with us! We hope you had a pleasant and comfortable experience.\n\n" +
                       $"Room Number: {roomNo}\n" +
                       $"Check-out Date: {checkOut}\n" +
                       $"Total Charges: LKR {totalCharges.ToString()}\n\n" +
                       $"We truly appreciate your visit and look forward to welcoming you again in the future.\n\n" +
                       $"Best regards,\nSCR Hotel";

            // SMTP server settings
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); // e.g., smtp.gmail.com
            smtp.Credentials = new NetworkCredential("shanmax18913@gmail.com", "qsiu qaig ufdf safe");
            smtp.EnableSsl = true; // Use SSL if required
            try
            {
                smtp.Send(mail);
                MessageBox.Show("Email sent successfully!", "Done", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.Message);
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Load_Data();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT Customer.customerID, Customer.customerName, Customer.mobile, Customer.nationality, Customer.gender, Customer.dob, Customer.idProof, Customer.email, Customer.checkIn, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price FROM Customer INNER JOIN rooms ON Customer.roomID = rooms.roomID WHERE Customer.chekOut = 'NO' AND LOWER(Customer.customerName) LIKE '%" + txtCustomerName.Text + "%';", connection))
                    {
                        // cmd.Parameters.AddWithValue("@search", txtCustomerName.Text);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgCustomers.ItemsSource = dt.DefaultView; // Correct binding
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT checkIn FROM Customer WHERE customerName = @v1;", connection))
                    {
                        cmd.Parameters.AddWithValue("@v1", txtCustomerName.Text);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read()) {

                            DateTime checkIn = Convert.ToDateTime(reader["checkIn"]);


                            DateTime checkOut = dtCheckOut.SelectedDate.Value;
                            TimeSpan duration = checkOut - checkIn;
                            int days = (int)duration.TotalDays;

                            if (days < 0)
                            {
                                MessageBox.Show("Check Out Date is before Check In Date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                        }


                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


            try
            {

                MessageBoxResult result = MessageBox.Show("Are You Sure!!", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("UPDATE Customer SET chekOut='YES' , checkOut ='" + dtCheckOut.SelectedDate.Value.Date + "' WHERE LOWER(customerName) = '" + txtCustomerName.Text + "' UPDATE rooms SET booked = 'NO' WHERE roomNo = '" + txtRoomNo.Text + "';", connection);
                        int indicator = command.ExecuteNonQuery();

                        if (indicator == 1)
                        {

                            MessageBox.Show("Check Out Unsuccessfull", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("Check Out Successfull", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                            SqlCommand cmd = new SqlCommand("SELECT Customer.customerID, Customer.customerName, Customer.mobile, Customer.nationality, Customer.gender, Customer.dob, Customer.idProof, Customer.email, Customer.checkIn, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price FROM Customer INNER JOIN rooms ON Customer.roomID = rooms.roomID WHERE LOWER(Customer.customerName) = @v1;", connection);
                            cmd.Parameters.AddWithValue("@v1", txtCustomerName.Text);
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read()) {
                                String CustomerName = reader["customerName"].ToString();
                                String roomNo = reader["roomNo"].ToString();
                                String email = reader["email"].ToString();
                                DateTime checkIn = Convert.ToDateTime(reader["checkIn"]);
                                DateTime checkOut = dtCheckOut.SelectedDate.Value.Date;
                                double roomPrice = Convert.ToDouble(reader["price"]);
                                TimeSpan duration = checkOut - checkIn;
                                double totalCharges = roomPrice * duration.TotalDays;
                                sendMail(email, CustomerName, roomNo, checkOut, totalCharges);
                               
                            }

                        }

                    }
                    Load_Data();
                    txtCustomerName.Text = "";
                    txtRoomNo.Text = "";
                    dtCheckOut.Text = "";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }



        }
        
        public string GetTextBox1Value()
        {
            String name = txtCustomerName.Text;
            return name;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            invoiceCustomer invoice = new invoiceCustomer();
            invoice.Show();
        }
        
    }
}
