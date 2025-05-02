using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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

namespace HotelManagementSystemWPF.Views
{
    /// <summary>
    /// Interaction logic for customerDetails.xaml
    /// </summary>
      
    public partial class customerDetails : UserControl
    {
        String connectionString = "Data Source=vishzz-laptop;Integrated Security=True;Encrypt=False";
        public customerDetails()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String type = cmbSearch.Text.ToLower();
            if (type.ToLower() == "all customers")
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter("SELECT Customer.customerID,Customer.customerName,Customer.mobile,Customer.nationality,Customer.gender,Customer.dob,Customer.idProof,Customer.email,Customer.checkIn,rooms.roomNo,rooms.roomType,rooms.bed,rooms.price FROM Customer inner join rooms on Customer.roomID = rooms.roomID ;", connection);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        CustomerGrid.ItemsSource = dt.DefaultView;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (type.ToLower() == "in hotel customers")
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter("SELECT Customer.customerID,Customer.customerName,Customer.mobile,Customer.nationality,Customer.gender,Customer.dob,Customer.idProof,Customer.email,Customer.checkIn,rooms.roomNo,rooms.roomType,rooms.bed,rooms.price FROM Customer inner join rooms on Customer.roomID = rooms.roomID WHERE chekOut = 'NO' ;", connection);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        CustomerGrid.ItemsSource = dt.DefaultView;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (type.ToLower() == "checkout customers") {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter("SELECT Customer.customerID,Customer.customerName,Customer.mobile,Customer.nationality,Customer.gender,Customer.dob,Customer.idProof,Customer.email,Customer.checkIn,rooms.roomNo,rooms.roomType,rooms.bed,rooms.price FROM Customer inner join rooms on Customer.roomID = rooms.roomID WHERE chekOut = 'YES' ;", connection);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        CustomerGrid.ItemsSource = dt.DefaultView;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
