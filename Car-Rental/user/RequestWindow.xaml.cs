using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.UA;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

namespace Car_Rental.user
{
    /// <summary>
    /// Логика взаимодействия для RequestWindow.xaml
    /// </summary>
    public partial class RequestWindow : Window
    {
        string Login = UserAuthWindow.Login;
        public RequestWindow()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString.ToString();
            login123.Text = Login;
        }

        MySqlConnection con = new MySqlConnection();

        DataTable dt = new DataTable();

        MySqlCommand cmd = new MySqlCommand();

        MySqlDataAdapter adapter = new MySqlDataAdapter();



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();

        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {

        }

        public string MyLogin { get; set; }
        public void loadTable()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                string login = this.MyLogin;
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = $"select name, phone, passport, email_user, date_format(birthDate, '%d.%m.%Y') from rentalcar.clients where login_user = '{Login}'";
                cmd.ExecuteNonQuery();
                dt.Clear();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                ConfirmTable.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadTable();
        }
    }
}
