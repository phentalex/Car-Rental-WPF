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

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString.ToString();
        }

        MySqlConnection con = new MySqlConnection();

        private void BtnExit_MouseEnter(object sender, MouseEventArgs e)
        {
            BtnExit.Foreground = new SolidColorBrush(Colors.DarkRed);
        }

        private void BtnExit_MouseLeave(object sender, MouseEventArgs e)
        {
            BtnExit.Foreground = new SolidColorBrush(Colors.White);
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
