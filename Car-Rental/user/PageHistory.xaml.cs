using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Car_Rental.user
{
    /// <summary>
    /// Логика взаимодействия для PageHistory.xaml
    /// </summary>
    public partial class PageHistory : Page
    {
        string Login = UserAuthWindow.Login;

        public PageHistory()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString.ToString();
            
        }

        MySqlConnection con = new MySqlConnection();

        MySqlCommand cmd = new MySqlCommand();

        MySqlDataAdapter adapter = new MySqlDataAdapter();

        DataTable dt = new DataTable();

        MySqlDataReader dr;

        string passport123;
        string passportValue;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetPassport(passport123);
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = $"select id_rent 'НомерАренды', " +
                    $"date_format(start_rent, '%d.%m.%Y') 'НачалоАренды', " +
                    $"date_format(end_rent, '%d.%m.%Y') 'КонецАренды', " +
                    $"total_price 'ПолнаяСтоимость', name 'ФИО', " +
                    $"passport 'Паспорт', email_user 'Почта', " +
                    $"date_format(birthDate, '%d.%m.%Y') 'ДатаРождения', " +
                    $"manufacturer 'Производитель', model 'Модель', price_a_day 'ЦенаЗаДень', " +
                    $"licensePlate 'НомернойЗнак', gearbox 'КПП' from rentalcar.rents where passport = {passportValue};";
                cmd.ExecuteNonQuery();
                dt.Clear();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                HistoryTable.ItemsSource = dt.DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        public string GetPassport(string passport123)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = $"select passport from rentalcar.clients where login_user = '{Login}';";
                dr = cmd.ExecuteReader();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    passport123 = dr.GetName(i).ToString();
                }
                while (dr.Read())
                {
                    passportValue = dr.GetValue(0).ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return passportValue;
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dvManager = dt.DefaultView;
            dvManager.RowFilter = $"Производитель LIKE '%{SearchText.Text}%' OR " +
                $"Модель LIKE '%{SearchText.Text}%' OR КПП LIKE '%{SearchText.Text}%' OR " +
                $"НомернойЗнак LIKE '%{SearchText.Text}%'";
        }
    }
}
