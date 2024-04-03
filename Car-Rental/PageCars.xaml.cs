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
    /// Interaction logic for PageCars.xaml
    /// </summary>
    public partial class PageCars : Page
    {
        public PageCars()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString.ToString();
        }

        DataTable dt = new DataTable();

        MySqlConnection con = new MySqlConnection();

        MySqlCommand cmd = new MySqlCommand();

        MySqlDataReader dr;

        MySqlDataAdapter adapter = new MySqlDataAdapter();

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "insert into rentalcar.cars(licensePlate, manufacturer, model, gearbox, price_a_day, year) " +
                    $"values ('{licensePlate.Text}', '{manufacturer.Text}', '{model.Text}', '{gearbox.Text}', '{price_a_day.Text}', '{year.Text}');";
                int a = cmd.ExecuteNonQuery();
                if(a == 1)
                {
                    MessageBox.Show("Данные успешно добавлены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                loadTable();
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
                cmd.CommandText = $"update rentalcar.cars set licensePlate = '{licensePlate.Text}', " +
                    $"manufacturer = '{manufacturer.Text}', model = '{model.Text}', " +
                    $"gearbox = '{gearbox.Text}', price_a_day = '{price_a_day.Text}', " +
                    $"year = '{year.Text}' where id_car = '{id_car.Text}'";
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
                cmd.CommandText = $"delete from rentalcar.cars where id_car = '{id_car.Text}'";
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
                cmd.CommandText = "select id_car 'НомерМашины'," +
                    "licensePlate 'НомернойЗнак'," +
                    "manufacturer 'Производитель'," +
                    "model 'Модель'," +
                    "gearbox 'КоробкаПередач'," +
                    "price_a_day 'ЦенаЗаДень'," +
                    "year 'ГодВыпуска' from rentalcar.cars;";
                cmd.ExecuteNonQuery();
                dt.Clear();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                CarsTable.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            loadTable();
        }
    }
}
