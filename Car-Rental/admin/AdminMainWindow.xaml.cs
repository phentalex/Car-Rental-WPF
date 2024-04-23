using System;
using System.Collections.Generic;
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
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;


namespace Car_Rental
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString.ToString();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        MySqlConnection con = new MySqlConnection();
        

        
        private void BtnMenu1_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageCars();
        }

        private void BtnMenu2_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageRent();
        }

        private void BtnMenu3_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageClients();
        }

        private void BtnMenu4_Click(object sender, RoutedEventArgs e)
        {
            AdminAuthWindow auth = new AdminAuthWindow();
            this.Close();
            auth.Show();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
