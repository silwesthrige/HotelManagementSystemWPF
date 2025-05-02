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
using System.Windows.Shapes;

namespace HotelManagementSystemWPF
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {


            String connectionString = "Data Source=vishzz-laptop;Initial Catalog=HotelManagementDB;Integrated Security=True;Encrypt=False";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Customer;", connection);

                DataTable dataTable = new DataTable();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Customer;", connection);
                adapter.Fill(dataTable);
                dataGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
