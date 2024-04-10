﻿using Car_Rental.user;
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

namespace Car_Rental
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class PageSelectCar : Page
    {
        public PageSelectCar()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString.ToString();
        }

        MySqlConnection con = new MySqlConnection();

        DataTable dt = new DataTable();

        MySqlCommand cmd = new MySqlCommand();

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
                cmd.CommandText = "select id_car 'Номер', manufacturer 'Производитель', " +
                                  "model 'Модель', licensePlate 'НомернойЗнак', " +
                                  "gearbox 'КПП', price_a_day 'ЦенаЗаДень', " +
                                  "year 'ГодВыпуска' from rentalcar.cars;";
                cmd.ExecuteNonQuery();
                dt.Clear();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                SelectCarTable.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_select_Click(object sender, RoutedEventArgs e)
        {
            RequestWindow req = new RequestWindow();
            req.Show();
        }


    }
}