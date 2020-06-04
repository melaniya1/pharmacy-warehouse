using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для select.xaml
    /// </summary>
    public partial class select : Page
    {
        int realAmount = 0;
        SqlConnection sqlConnection;
        public select()
        {
            InitializeComponent();
            connectDB();
        }
        private async void connectDB()
        {
            string connectionStr = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetFullPath("../../shopDB.mdf")};";

            sqlConnection = new SqlConnection(connectionStr);

            if (sqlConnection.State == ConnectionState.Closed)
                await sqlConnection.OpenAsync();
        }

        private void IdBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IdBox.Text != "" && amountBox.Text != "")
            {
                btnGet.IsEnabled = true;
            }
            else
            {
                btnGet.IsEnabled = false;
            }
        }

        private void amountBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IdBox.Text != "" && amountBox.Text != "")
            {
                btnGet.IsEnabled = true;
            }
            else
            {
                btnGet.IsEnabled = false;
            }
        }
        private bool checkId()
        {
            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand($"SELECT * FROM Товары", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    if (Convert.ToString(sqlReader["ID"]) == IdBox.Text)
                    {
                        if (sqlReader != null)
                            sqlReader.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "1", ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (sqlReader != null)
                sqlReader.Close();

            return false;
        }
        private bool checkAmount()
        {
            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand($"SELECT * FROM Товары", sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                
                if (Convert.ToInt32(amountBox.Text) > Convert.ToInt32(sqlReader["Количество"]) && Convert.ToString(sqlReader["ID"]) == IdBox.Text )
                {
                    
                    realAmount = Convert.ToInt32(sqlReader["Количество"]);
                    
                    //MessageBox.Show(Convert.ToString(Convert.ToInt32(amountBox.Text)) + " | " + Convert.ToString(Convert.ToInt32(sqlReader["Количество"])));
                    if (sqlReader != null)
                        sqlReader.Close();
                    return false;
                }
            }

            if (sqlReader != null)
                sqlReader.Close();

            return true;
        }
        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            errorAmount.Visibility = Visibility.Collapsed;
            errorId.Visibility = Visibility.Collapsed;
            errorAmount2.Visibility = Visibility.Collapsed;

            if (checkId())
            {
                Regex regex = new Regex(@"[^0-9]");

                if (regex.Matches(amountBox.Text).Count == 0)
                {
                    if (checkAmount())
                    {
                        File.WriteAllText("Продукт.txt", IdBox.Text);
                        File.WriteAllText("Количество.txt", amountBox.Text);

                        App.Current.MainWindow.Content = new selectGood();
                    }
                    else
                    {
                        errorAmount2.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    errorAmount.Visibility = Visibility.Visible;
                }
            }
            else
            {
                errorId.Visibility = Visibility.Visible;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Content = new goodsList();
        }
    }
}
