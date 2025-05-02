using HotelManagementSystemWPF.Views;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;



namespace HotelManagementSystemWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        String connectionString = "Data Source=vishzz-laptop;Integrated Security=True;Encrypt=False";

        public Window1()
        {
            InitializeComponent();
            

        }
        private DispatcherTimer autoRefreshTimer;

        private void StartAutoRefresh()
        {
            autoRefreshTimer = new DispatcherTimer();
            autoRefreshTimer.Interval = TimeSpan.FromSeconds(3); // Refresh every 5 seconds
            autoRefreshTimer.Tick += AutoRefreshTimer_Tick;
            autoRefreshTimer.Start();
        }

        private void AutoRefreshTimer_Tick(object sender, EventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            bill bill = new bill();
            bill.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            UCCustomerDetails.Visibility = Visibility.Hidden;

            UCCustomerRegistration.Visibility = Visibility.Hidden;
            
            UCEmployee.Visibility = Visibility.Hidden;



            UCCheckout.Visibility = Visibility.Hidden;
            AddRoomControl.Visibility = Visibility.Visible;
                       

        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            UCCustomerDetails.Visibility = Visibility.Hidden;
            UCCheckout.Visibility = Visibility.Hidden;
            AddRoomControl.Visibility = Visibility.Hidden;
            UCEmployee.Visibility = Visibility.Hidden;
            AddRoomControl.Visibility = Visibility.Hidden;

            UCCustomerRegistration.Visibility = Visibility.Visible;

             
           
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            DialogResult result = (DialogResult)System.Windows.MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            UCCustomerDetails.Visibility = Visibility.Hidden;
            AddRoomControl.Visibility = Visibility.Hidden;
            UCCustomerRegistration.Visibility = Visibility.Hidden;
            UCCheckout.Visibility = Visibility.Hidden;
            UCEmployee.Visibility = Visibility.Hidden;



            UCCheckout.Visibility = Visibility.Visible;
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            UCCustomerDetails.Visibility = Visibility.Hidden;
            AddRoomControl.Visibility = Visibility.Hidden;
            UCCustomerRegistration.Visibility = Visibility.Hidden;
            UCCheckout.Visibility = Visibility.Hidden;
            UCEmployee.Visibility = Visibility.Visible;

        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            AddRoomControl.Visibility = Visibility.Hidden;
            UCCustomerRegistration.Visibility = Visibility.Hidden;
            UCCheckout.Visibility = Visibility.Hidden;
            UCEmployee.Visibility = Visibility.Hidden;
            UCCustomerDetails.Visibility = Visibility.Visible;
        }
    }
}
