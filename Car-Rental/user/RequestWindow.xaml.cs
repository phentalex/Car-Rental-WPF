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
        string ID = PageSelectCar.ID;
        public RequestWindow()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString.ToString();
            login123.Text = Login;
            id123.Text = ID;
        }

        MySqlConnection con = new MySqlConnection();

        DataTable dt = new DataTable();

        MySqlCommand cmd = new MySqlCommand();

        MySqlDataAdapter adapter = new MySqlDataAdapter();

        MySqlDataReader dr;



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();

        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = $"insert into rentalcar.rents " +
                    $"(start_rent, end_rent, total_price, name, passport, email_user, " +
                    $"birthDate, manufacturer, model, price_a_day, licensePlate, gearbox) " +
                    $"values ('{start_rent.SelectedDate.Value.ToString("yyyy-MM-dd")}', " +
                    $"'{end_rent.SelectedDate.Value.ToString("yyyy-MM-dd")}', " +
                    $"'{total_price.Text}', '{name.Text}', '{Convert.ToInt64(passport.Text)}', " +
                    $"'{email_user.Text}', '{birthDate.SelectedDate.Value.ToString("yyyy-MM-dd")}', " +
                    $"'{manufacturer.Text}', '{model.Text}', '{price_a_day.Text.Replace(",", ".")}', " +
                    $"'{licensePlate.Text}', '{gearbox.Text}');";
                int a = cmd.ExecuteNonQuery();
                if (a == 1)
                {
                    MessageBox.Show("Заявка успешно отправлена!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void loadClient()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = $"select name, phone, passport, email_user, " +
                    $"date_format(birthDate, '%d.%m.%Y') from rentalcar.clients where login_user = '{Login}'";
                dr = cmd.ExecuteReader();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    login123.Text = dr.GetName(i).ToString();
                }
                while (dr.Read())
                {
                    name.Text = dr.GetValue(0).ToString();
                    phone.Text = dr.GetValue(1).ToString();
                    passport.Text = dr.GetValue(2).ToString();
                    email_user.Text = dr.GetValue(3).ToString();
                    birthDate.Text = dr.GetValue(4).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void loadCar()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = $"select manufacturer, model, price_a_day, " +
                    $"licensePlate, gearbox from rentalcar.cars where id_car = '{ID}'";
                dr = cmd.ExecuteReader();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    id123.Text = dr.GetName(i).ToString();
                }
                while (dr.Read())
                {
                    manufacturer.Text = dr.GetValue(0).ToString();
                    model.Text = dr.GetValue(1).ToString();
                    price_a_day.Text = dr.GetValue(2).ToString();
                    licensePlate.Text = dr.GetValue(3).ToString();
                    gearbox.Text = dr.GetValue(4).ToString();
                }
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
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadClient();
            loadCar();
        }

        private void end_rent_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void start_rent_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_checkPrice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (start_rent.Text.ToString() != null && end_rent.Text.ToString() != null)
                {
                    var days = (end_rent.SelectedDate.Value - start_rent.SelectedDate.Value).TotalDays;
                    total_price.Text = (days * Double.Parse(price_a_day.Text) + 0.09).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
