using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для add.xaml
    /// </summary>
    public partial class add : Page
    {
        SqlConnection sqlConnection;
        public add()
        {
            InitializeComponent();
            connectDB();
            foreach (UIElement c in stackBox.Children)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).TextChanged += nameBox_TextChanged;
                }
            }
        }
        private async void connectDB()
        {

            string connectionStr = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetFullPath("../../shopDB.mdf")};";

            sqlConnection = new SqlConnection(connectionStr);

            if (sqlConnection.State == ConnectionState.Closed)
                await sqlConnection.OpenAsync();
        }
        private void checkEmptyBox()
        {
            if (nameBox.Text != "" && namePharm.Text != "" && amountBox.Text != "" && addressBox.Text != "")
            {
                btnAdd.IsEnabled = true;
            }
            else
            {
                btnAdd.IsEnabled = false;
            }
        }
        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkEmptyBox();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Content = new goodsList();
        }
        private void addGood()
        {
            bool pass = true;

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand($"SELECT * FROM Товары", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    if (Convert.ToString(sqlReader["Название"]) == nameBox.Text && Convert.ToString(sqlReader["Аптека"]) == namePharm.Text)
                    {
                        pass = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (sqlReader != null)
                sqlReader.Close();

            if (pass)
            {
                command = new SqlCommand($"INSERT Товары(Название,Аптека,Адрес,Количество) VALUES(N'{nameBox.Text}', N'{namePharm.Text}',N'{addressBox.Text}',@Кол)", sqlConnection);
                command.Parameters.AddWithValue("Кол", Convert.ToInt32(amountBox.Text));
                command.ExecuteNonQuery();
                App.Current.MainWindow.Content = new add();
            }
            else
            {
                errorGood.Visibility = Visibility.Visible;
            }

            if (sqlReader != null)
                sqlReader.Close();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            errorAmount.Visibility = Visibility.Collapsed;
            errorGood.Visibility = Visibility.Collapsed;

            Regex regex = new Regex(@"[^0-9]");

            if (regex.Matches(amountBox.Text).Count == 0)
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    addGood();
            }
            else
            {
                errorAmount.Visibility = Visibility.Visible;
            }
        }
    }
}
