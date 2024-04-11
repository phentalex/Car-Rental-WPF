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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;

namespace Car_Rental
{
    /// <summary>
    /// Логика взаимодействия для PageWorkers.xaml
    /// </summary>
    public partial class PageRent : Page
    {
        public PageRent()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString.ToString();
        }

        MySqlConnection con = new MySqlConnection();

        MySqlCommand cmd = new MySqlCommand();

        MySqlDataAdapter adapter = new MySqlDataAdapter();

        DataTable dt = new DataTable();

        private void Page_Loaded(object sender, RoutedEventArgs e)
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
                cmd.CommandText = "select id_rent 'НомерАренды', " +
                    "start_rent 'НачалоАренды', end_rent 'КонецАренды', " +
                    "total_price 'ПолнаяСтоимость', name 'ФИО', " +
                    "passport 'Паспорт', email_user 'Почта', " +
                    "date_format(birthDate, '%d.%m.%Y') 'ДатаРождения', " +
                    "manufacturer 'Производитель', model 'Модель', price_a_day 'ЦенаЗаДень', " +
                    "licensePlate 'НомернойЗнак', gearbox 'КПП' from rentalcar.rents;";
                cmd.ExecuteNonQuery();
                dt.Clear();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                RentsTable.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = $"update rentalcar.rents set ...";
                int a = cmd.ExecuteNonQuery();
                if (a == 1)
                {
                    MessageBox.Show("Данные успешно изменены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                loadTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = $"delete from rentalcar.rents where if_rent = '{id_rent.Text}'";
                int a = cmd.ExecuteNonQuery();
                if (a == 1)
                {
                    MessageBox.Show("Данные успешно удалены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                loadTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchText1_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dvManager2 = dt.DefaultView;
            dvManager2.RowFilter = $"Производитель LIKE '%{SearchText1.Text}%' OR " +
                $"Модель LIKE '%{SearchText1.Text}%' OR КПП LIKE '%{SearchText1.Text}%' OR " +
                $"НомерМашины LIKE '%{SearchText1.Text}%'OR ФИО LIKE '%{SearchText1.Text}%'";
        }

        private void SearchText2_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dvManager2 = dt.DefaultView;
            dvManager2.RowFilter = string.Format("convert(Паспорт, 'System.String') LIKE '%{0}%' OR " +
                "convert(ПолнаяСтоимость, 'System.String')", SearchText2.Text);
        }
    }
}
