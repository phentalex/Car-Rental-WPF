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
        string CS;

        public MainWindow()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString.ToString();
        }

        MySqlConnection con = new MySqlConnection();

        
        private void BtnMenu1_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageCars();
        }

        private void BtnMenu2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnMenu3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnMenu4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnHide_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void BtnHide_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void BtnHide_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void BtnClose_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnClose_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void BtnClose_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
