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
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Car_Rental
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString.ToString();
        }

        MySqlConnection con = new MySqlConnection();

        MySqlCommand cmd = new MySqlCommand();

        MySqlDataReader dr;

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = $"select id_role from rentalcar.users where login_user = '{loginUser.Text}' and pass_user = '{passUser.Text}'";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (Convert.ToInt32(dr["id_role"]) == 1)
                    {
                        MessageBox.Show("Вы успешно вошли как сотрудник!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        AdminMainWindow main = new AdminMainWindow();
                        this.Close();
                        main.Show();
                    }
                    else if (Convert.ToInt32(dr["id_role"]) == 2)
                    {
                        MessageBox.Show("Вы успешно вошли как клиент!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        //AdminMainWindow main = new AdminMainWindow();
                        this.Close();
                        //main.Show();
                    } 
                    else
                    {
                        MessageBox.Show("Неверно указан логин или пароль!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
