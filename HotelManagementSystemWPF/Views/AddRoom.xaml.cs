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
    /// Interaction logic for AddRoom.xaml
    /// </summary>
    public partial class AddRoom : UserControl
    {
        String connectionString = "Data Source=vishzz-laptop;Integrated Security=True;Encrypt=False";
        public AddRoom()
        {
            InitializeComponent();
            Load_Data();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void cleanAll() { 
            
            txtRoomNumber.Text = "";
            cmbRoomType.SelectedIndex = -1;
            cmbBed.SelectedIndex = -1;
            txtPrice.Text = "";
        }

        private void Load_Data() { 
            
           
            String query = "SELECT * FROM rooms;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridRoom.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Alert",MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO rooms(roomNo,roomType,bed,price) VALUES (@v1,@v2,@v3,@v4);", connection);

                command.Parameters.AddWithValue("@v1", txtRoomNumber.Text);
                command.Parameters.AddWithValue("@v2", cmbRoomType.Text);
                command.Parameters.AddWithValue("@v3",cmbBed.Text);
                command.Parameters.AddWithValue("@v4", Convert.ToInt32(txtPrice.Text));

                int indicator = command.ExecuteNonQuery();
                if (indicator > 0)
                {
                    MessageBox.Show("Room Added!!", "Succesfull!!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Room Not Added", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                connection.Close();
                cleanAll();
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message, "Add Room", MessageBoxButton.OK, MessageBoxImage.Information);

            }


            Load_Data();
        }

        private void btnDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("DELETE FROM rooms WHERE roomNo = @v1;", connection);

                command.Parameters.AddWithValue("@v1", txtRoomNumber.Text);
                
                int indicator = command.ExecuteNonQuery();
                if (indicator > 0)
                {
                    MessageBox.Show("Room Deleted!!", "Succesfull!!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Room Not Deleted", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                connection.Close();
                cleanAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Room", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            Load_Data();
        }
    }
}
