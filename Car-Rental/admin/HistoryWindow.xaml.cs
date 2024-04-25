using MySql.Data.MySqlClient;
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
using System.Configuration;
using System.Data;

namespace Car_Rental.admin
{
    /// <summary>
    /// Логика взаимодействия для HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        string passport = PageClients.ID;
        public HistoryWindow()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString.ToString();
        }

        MySqlConnection con = new MySqlConnection();

        MySqlCommand cmd = new MySqlCommand();

        DataTable dt = new DataTable();

        MySqlDataAdapter adapter = new MySqlDataAdapter();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadTable();
        }

        public void loadTable()
        {
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
                    $"total_price 'ПолнаяСтоимость(руб.)', name 'ФИО', " +
                    $"passport 'Паспорт', email_user 'Почта', " +
                    $"date_format(birthDate, '%d.%m.%Y') 'ДатаРождения', " +
                    $"manufacturer 'Производитель', model 'Модель', price_a_day 'ЦенаЗаДень(руб.)', " +
                    $"licensePlate 'НомернойЗнак', gearbox 'КПП' from rentalcar.rents where passport = '{passport}';";
                adapter.SelectCommand = cmd;
                dt.Clear();
                adapter.Fill(dt);
                HistoryTable.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dvManager = dt.DefaultView;
            dvManager.RowFilter = $"Производитель LIKE '%{SearchText.Text}%' OR " +
                                  $"Модель LIKE '%{SearchText.Text}%' OR " +
                                  $"НомернойЗнак LIKE '%{SearchText.Text}%'";
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
