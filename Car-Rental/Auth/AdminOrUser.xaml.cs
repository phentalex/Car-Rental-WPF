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
using System.Windows.Shapes;

namespace Car_Rental
{
    /// <summary>
    /// Логика взаимодействия для AdminOrUser.xaml
    /// </summary>
    public partial class AdminOrUser : Window
    {
        public AdminOrUser()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            UserAuthWindow Ua = new UserAuthWindow();
            this.Close();
            Ua.Show();
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            AdminAuthWindow Aa = new AdminAuthWindow();
            this.Close();
            Aa.Show();
        }
    }
}
