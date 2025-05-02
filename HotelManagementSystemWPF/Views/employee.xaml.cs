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
    /// Interaction logic for employee.xaml
    /// </summary>
    public partial class employee : UserControl
    {
        String connectionString = "Data Source=vishzz-laptop;Integrated Security=True;Encrypt=False";
        public employee()
        {
            InitializeComponent();
            LOAD_DATA();
        }

        private void btnDeleteEmp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO employee (employeeName,mobile,gender,email,username,pass) VALUES (@v1,@v2,@v3,@v4,@v5,@v6);", connection);
                    command.Parameters.AddWithValue("@v1", txtName.Text);
                    command.Parameters.AddWithValue("@v2", Convert.ToInt32(txtMobile.Text));
                    command.Parameters.AddWithValue("@v3", cmbGender.Text);
                    command.Parameters.AddWithValue("@v4", txtEmail.Text);
                    command.Parameters.AddWithValue("@v5", txtUserName.Text);
                    command.Parameters.AddWithValue("@v6", txtPassword.Password);

                    int indicator = command.ExecuteNonQuery();
                    if (indicator > 0)
                    {

                        MessageBox.Show("Registration Successfull", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Registration Unsuccessfull", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            LOAD_DATA();
            txtEmail.Clear();
            txtUserName.Clear();
            txtPassword.Clear();
            txtMobile.Clear();
            cmbGender.SelectedIndex = -1;
            
        }

        private void EmpDetailDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmpDetailDataGrid.SelectedItem is DataRowView row)
            {
                // Set your TextBoxes here
                EmployeeSearchTextBox.Text = row["employeeName"].ToString();
                
            }
        }
        public void LOAD_DATA() {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT employeeID,employeeName,mobile,gender,email,username FROM employee;", connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    EmpDetailDataGrid.ItemsSource = dt.DefaultView;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EmployeeSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EmployeeSearchTextBox.Text != "")
            {
                SearchPlaceholder.Text = "";
            }
            else {
                SearchPlaceholder.Text = "Search by Employee Name";
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM employee WHERE LOWER(employeeName) LIKE '%"+EmployeeSearchTextBox.Text+"%';", connection))
                    {
                        // cmd.Parameters.AddWithValue("@search", txtCustomerName.Text);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            EmpDetailDataGrid.ItemsSource = dt.DefaultView; // Correct binding
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM employee WHERE LOWER(employeeName) =@v1;", connection))
                    {

                        cmd.Parameters.AddWithValue("@v1",EmployeeSearchTextBox.Text.ToLower());
                        int indicator = cmd.ExecuteNonQuery();
                        if (indicator > 0)
                        {
                            MessageBox.Show("Deleted!", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else {
                            MessageBox.Show("Delete Unsuccessful!", "Done", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            LOAD_DATA();



        }
    }
}
