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
    /// Логика взаимодействия для del.xaml
    /// </summary>
    public partial class del : Page
    {
        SqlConnection sqlConnection;
        public del()
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
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (sqlReader != null)
                sqlReader.Close();

            return false;
        }
        private void delGood()
        {
            SqlCommand command;

            command = new SqlCommand($"DELETE Товары WHERE ID = @id", sqlConnection);
            command.Parameters.AddWithValue("id", Convert.ToInt32(IdBox.Text));
            command.ExecuteNonQuery();
            App.Current.MainWindow.Content = new del();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IdBox.Text != "")
            {
                btnDel.IsEnabled = true;
            }
            else
            {
                btnDel.IsEnabled = false;
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            errorId.Visibility = Visibility.Collapsed;

            if (checkId())
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    delGood();
            }
            else
            {
                errorId.Visibility = Visibility.Visible;
            }
        }

        private void btnBack_Click_1(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Content = new goodsList();
        }
    }
}
