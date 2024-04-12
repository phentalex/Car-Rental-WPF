using Car_Rental.user;
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
    /// Логика взаимодействия для UserMainWindow.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        public UserMainWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnMenu1_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageSelectCar();
        }

        private void BtnMenu2_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PageHistory();
        }

        private void BtnMenu3_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow au = new AuthWindow();
            this.Close();
            au.Show();
        }
    }
}
