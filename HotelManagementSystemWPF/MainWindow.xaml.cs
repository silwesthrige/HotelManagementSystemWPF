using System;
using System.Collections.Generic;
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

namespace HotelManagementSystemWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String connectionString = "Data Source=vishzz-laptop;Integrated Security=True;Encrypt=False";
        public MainWindow()
        {
            InitializeComponent();
        }

        


        private void Button_Click(object sender, RoutedEventArgs e)
        {
                  
           
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void btnTogglePassword_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            window2.Show();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text != "" && txtPassword.Password != "")
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String passw;
                        SqlCommand command = new SqlCommand("SELECT * FROM employee WHERE username = @v1;", connection);
                        command.Parameters.AddWithValue("@v1", txtUsername.Text);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {

                            passw = reader["pass"].ToString().ToLower();

                            if (passw == txtPassword.Password.ToLower())
                            {
                                Window1 window1 = new Window1();
                                window1.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Password Incorrect!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                                clear_data();
                            }
                        }
                        else {
                            MessageBox.Show("Please Try Again");
                            clear_data();
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else {
                MessageBox.Show("Please Fill Sections","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            

                

              

            //

        }
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ForgetPassword forget = new ForgetPassword();
            forget.Show();
        }

        public void clear_data() {
            txtPassword.Clear();
            txtUsername.Clear();
        }
    }
}
