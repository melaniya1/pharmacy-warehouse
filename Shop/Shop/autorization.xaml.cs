using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для autorization.xaml
    /// </summary>
    public partial class autorization : Page
    {
        SqlConnection sqlConnection;
        public autorization()
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
        private void checkEmptyBox()
        {
            if (login.Text != "" && passBox.Password != "")
            {
                btnLog.IsEnabled = true;
            }
            else
            {
                btnLog.IsEnabled = false;
            }
        }
        private async void checkLoginAndPassword()
        {
            bool pass = false;
            int ID = 0;

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM Пользователи", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    if (Convert.ToString(sqlReader["Логин"]) == login.Text && Convert.ToString(sqlReader["Пароль"]) == passBox.Password)
                    {
                        pass = true;
                        ID = sqlReader.GetInt32(0);
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
                File.WriteAllText("Пользователь.txt",Convert.ToString(ID));
                App.Current.MainWindow.Content = new goodsList();
            }
            else
            {
                errorMessage.Visibility = Visibility.Visible;
            }

            if (sqlReader != null)
                sqlReader.Close();

        }
        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            errorMessage.Visibility = Visibility.Collapsed;
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                checkLoginAndPassword();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Content = new reg();
        }

        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkEmptyBox();
        }

        private void passBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            checkEmptyBox();
        }
    }
}
