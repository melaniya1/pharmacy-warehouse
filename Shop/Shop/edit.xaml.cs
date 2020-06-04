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
    /// Логика взаимодействия для edit.xaml
    /// </summary>
    public partial class edit : Page
    {
        bool isPass = false;
        SqlConnection sqlConnection;
        public edit()
        {
            InitializeComponent();
            connectDB();
            foreach (UIElement c in boxes.Children)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).TextChanged += IdBox_TextChanged;
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
            if(isPass)
            {
                if (nameBox.Text != "" && namePharm.Text != "" && amountBox.Text != "" && addressBox.Text != "")
                {
                    btnComplite.IsEnabled = true;
                }
                else
                {
                    btnComplite.IsEnabled = false;
                }
            }
            else
            {
                if (IdBox.Text != "")
                {
                    btnComplite.IsEnabled = true;
                }
                else
                {
                    btnComplite.IsEnabled = false;
                }
            }
            
        }

        private void IdBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkEmptyBox();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Content = new goodsList();
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
        private void editGood()
        {
            bool pass = true;

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand($"SELECT * FROM Товары", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    if (Convert.ToString(sqlReader["Название"]) == nameBox.Text && Convert.ToString(sqlReader["Аптека"]) == namePharm.Text && Convert.ToString(sqlReader["ID"]) != IdBox.Text)
                    {
                        pass = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "2", ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (sqlReader != null)
                sqlReader.Close();

            if (pass)
            {
                command = new SqlCommand($"UPDATE Товары SET Название = @name, Аптека = @pharm, Количество = @amount, Адрес = @address WHERE ID = @id", sqlConnection);
                command.Parameters.AddWithValue("id", Convert.ToInt32(IdBox.Text));
                command.Parameters.AddWithValue("name", nameBox.Text);
                command.Parameters.AddWithValue("pharm", namePharm.Text);
                command.Parameters.AddWithValue("amount", Convert.ToInt32(amountBox.Text));
                command.Parameters.AddWithValue("address", addressBox.Text);
                command.ExecuteNonQuery();
                App.Current.MainWindow.Content = new edit();
            }
            else
            {
                errorGood.Visibility = Visibility.Visible;
            }

            if (sqlReader != null)
                sqlReader.Close();
        }
        private void btnComplite_Click(object sender, RoutedEventArgs e)
        {
            if(isPass)
            {
                    errorAmount.Visibility = Visibility.Collapsed;
                    errorGood.Visibility = Visibility.Collapsed;

                    Regex regex = new Regex(@"[^0-9]");

                    if (regex.Matches(amountBox.Text).Count == 0)
                    {
                        if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                            editGood();
                    }
                    else
                    {
                        errorAmount.Visibility = Visibility.Visible;
                    }
            }
            else
            {
                if (checkId())
                {
                    errorId.Visibility = Visibility.Collapsed;
                    IdBox.IsEnabled = false;
                    boxes.Visibility = Visibility.Visible;
                    isPass = true;

                    SqlDataReader sqlReader = null;

                    SqlCommand command = new SqlCommand($"SELECT * FROM Товары", sqlConnection);

                    try
                    {
                        sqlReader = command.ExecuteReader();

                        while (sqlReader.Read())
                        {
                            if (Convert.ToString(sqlReader["ID"]) == IdBox.Text)
                            {
                                nameBox.Text = Convert.ToString(sqlReader["Название"]);
                                namePharm.Text = Convert.ToString(sqlReader["Аптека"]);
                                amountBox.Text = Convert.ToString(sqlReader["Количество"]);
                                addressBox.Text = Convert.ToString(sqlReader["Адрес"]);
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "1", ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (sqlReader != null)
                        sqlReader.Close();
                }
                else
                {
                    errorId.Visibility = Visibility.Visible;
                }
            }
        }
    }

}
