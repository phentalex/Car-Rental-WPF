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
    /// Логика взаимодействия для PageClients.xaml
    /// </summary>
    public partial class PageClients : Page
    {
        public PageClients()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString.ToString();
        }

        MySqlConnection con = new MySqlConnection();

        MySqlCommand cmd = new MySqlCommand();

        DataTable dt = new DataTable();

        MySqlDataAdapter adapter = new MySqlDataAdapter();

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
                cmd.CommandText = "select id_client 'НомерКлиента', " +
                    "name 'ФИО', passport 'Паспорт', " +
                    "driversLicense 'НомерУдостоверения', " +
                    "city 'Город',  date_format(birthDate, '%d.%m.%Y') 'ДатаРождения', " +
                    "phone 'Телефон' from rentalcar.clients;";
                cmd.ExecuteNonQuery();
                dt.Clear();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                ClientsTable.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


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
                cmd.CommandText = $"insert into rentalcar.clients(name, passport, driversLicense, city, birthDate, phone) " +
                                  $"values ('{name.Text}', '{Convert.ToInt64(passport.Text)}', '{Convert.ToInt64(driversLicense.Text)}', " +
                                  $"'{city.Text}', '{birthDate.SelectedDate.Value.ToString("yyyy-MM-dd")}', '{Convert.ToInt64(phone.Text)}')";
                int a = cmd.ExecuteNonQuery();
                if (a == 1)
                {
                    MessageBox.Show("Данные успешно добавлены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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
                cmd.CommandText = $"update rentalcar.clients set name = '{name.Text}', " +
                                  $"passport = '{Convert.ToInt64(passport.Text)}', " +
                                  $"driversLicense = '{Convert.ToInt64(driversLicense.Text)}', " +
                                  $"city = '{city.Text}'," +
                                  $"birthDate = '{birthDate.SelectedDate.Value.ToString("yyyy-MM-dd")}', " +
                                  $"phone = '{Convert.ToInt64(phone.Text)}' where id_client = '{id_client.Text}'";
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
                cmd.CommandText = $"delete from rentalcar.clients where id_client = '{id_client.Text}'";
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

        //private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    DataView dvManager = dt.DefaultView;
        //    dvManager.RowFilter = $"НомерКлиента LIKE '%{Convert.ToInt32(SearchText1.Text)}%' " +
        //                          $"OR ФИО LIKE '%{SearchText1.Text}%' " +
        //                          $"OR НомерУдостоверения LIKE '%{Convert.ToInt32(SearchText1.Text)}%' " +
        //                          $"OR Город LIKE '%{SearchText1.Text}%' " +
        //                          $"OR Телефон LIKE '%{Convert.ToInt64(SearchText1.Text)}%'";
        //}

        private void SearchText1_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dvManager1 = dt.DefaultView;
            dvManager1.RowFilter = $"ФИО LIKE '%{SearchText1.Text}%'";
        }

        private void SearchText2_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dvManager2 = dt.DefaultView;
            dvManager2.RowFilter = string.Format("convert(Паспорт, 'System.String') LIKE '%{0}%' OR convert(НомерУдостоверения, 'System.String') LIKE '%{0}%' ", SearchText2.Text);
            //("convert(НомерУдостоверения, 'System.String') LIKE '%{0}%' ", SearchText2.Text);
        }
    }
}
