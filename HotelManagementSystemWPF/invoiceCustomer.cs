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
using DGVPrinterHelper;
using HotelManagementSystemWPF.Views;

namespace HotelManagementSystemWPF
{
    public partial class invoiceCustomer : Form
    {
        DGVPrinter printer = new DGVPrinter();
        String connectionString = "Data Source=vishzz-laptop;Integrated Security=True;Encrypt=False";
        public invoiceCustomer()
        {
            InitializeComponent();
            LoadData();
            
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // To ignore header clicks
            {
                DataGridViewRow row = dataGridViewShan.Rows[e.RowIndex];

                // Example: set TextBox from column "customerName"
                txtSearchName.Text = row.Cells["customerName"].Value?.ToString();
            }
        }
        


        private void LoadData()
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    try
                    {

                        using (SqlCommand cmd = new SqlCommand("SELECT Customer.customerName, Customer.mobile, Customer.nationality, Customer.idProof, Customer.email, Customer.checkIn, Customer.checkOut,rooms.roomNo, rooms.roomType, rooms.bed, rooms.price, Customer.chekOut FROM Customer INNER JOIN rooms ON Customer.roomID = rooms.roomID;", connection))
                        {
                            // cmd.Parameters.AddWithValue("@search", txtCustomerName.Text);

                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                dataGridViewShan.DataSource = dt; // Correct binding
                            }
                        }

                    }
                    catch (Exception ex) {
                        System.Windows.MessageBox.Show(ex.Message);

                    }






                        
                    
                }
            }
            catch (Exception ex) {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public double calculation() {

           
            return 0;
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT Customer.customerName, Customer.mobile, Customer.nationality, Customer.idProof, Customer.email, Customer.checkIn, Customer.checkOut,rooms.roomNo, rooms.roomType, rooms.bed, rooms.price, Customer.chekOut FROM Customer INNER JOIN rooms ON Customer.roomID = rooms.roomID WHERE LOWER(Customer.customerName) LIKE '%"+txtSearchName.Text+"%' ", connection))
                    {
                        // cmd.Parameters.AddWithValue("@search", txtCustomerName.Text);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridViewShan.DataSource = dt; // Correct binding
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int days = 0;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT Customer.checkIn, Customer.checkOut, rooms.price FROM Customer INNER JOIN rooms ON Customer.roomID = rooms.roomID WHERE LOWER(customerName) = @v1;", connection))
                    {

                        cmd.Parameters.AddWithValue("@v1",txtSearchName.Text.ToLower());
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            

                                

                                DateTime checkIn = Convert.ToDateTime(reader["checkIn"]);
                            double roomPrice = Convert.ToDouble(reader["price"]);


                                DateTime checkOut = Convert.ToDateTime(reader["checkOut"]);
                                TimeSpan duration = checkOut - checkIn;
                                 days = (int)duration.TotalDays;
                                

                                printer.Title = "SCR HOTEL - GUEST INVOICE";

                                // 📅 SubTitle with full details
                                printer.SubTitle =
                                    $"Date: {DateTime.Now:MMMM dd, yyyy}\n" +
                                    $"Guest Stay: {checkIn.ToString():dd MMM yyyy} to {checkOut.ToString():dd MMM yyyy} ({days} night(s))\n" +
                                    $"Room Rate: Rs. {roomPrice.ToString():N2} per night";

                                // 🧾 SubTitle formatting
                                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;

                                // 📋 Data table format
                                printer.PageNumbers = true;
                                printer.PageNumberInHeader = false;
                                printer.PorportionalColumns = true;
                                printer.HeaderCellAlignment = StringAlignment.Center;
                                printer.CellAlignment = StringAlignment.Center;

                                // Landscape layout
                                printer.printDocument.DefaultPageSettings.Landscape = true;

                                // 💰 Footer with final price
                                printer.Footer = $"TOTAL AMOUNT: Rs. {days * roomPrice :N2}   |   Thank you for staying with us!";
                                printer.FooterSpacing = 25;

                                // 🧾 Print
                                printer.PrintDataGridView(dataGridViewShan);

                                if (days < 0)
                                {
                                    System.Windows.MessageBox.Show("Check Out Date is before Check In Date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                                }
                            }




                        }



                    }
                





            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.ToString());
            }
        }
    }
}
