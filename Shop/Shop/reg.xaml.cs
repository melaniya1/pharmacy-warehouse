using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.IO;
using System.Text.RegularExpressions;

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для reg.xaml
    /// </summary>
    public partial class reg : Page
    {
        SqlConnection sqlConnection;
        public reg()
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
            if (passBox.Password != "" && login.Text != "")
            {
                btnReg.IsEnabled = true;
            }
            else
            {
                btnReg.IsEnabled = false;
            }
        }
        private async void addUser()
        {
            bool pass = true;

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand($"SELECT * FROM Пользователи", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    if (Convert.ToString(sqlReader["Логин"]) == login.Text)
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
                command = new SqlCommand($"INSERT Пользователи(Имя,Фамилия,Логин,Пароль) VALUES(N'{first.Text}', N'{second.Text}',N'{login.Text}','{passBox.Password}')", sqlConnection);
                await command.ExecuteNonQueryAsync();
                App.Current.MainWindow.Content = new autorization();
            }
            else
            {
                errorSameLogin.Visibility = Visibility.Visible;
            }

            if (sqlReader != null)
                sqlReader.Close();
        }
        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            errorSameLogin.Visibility = Visibility.Collapsed;

            errorPasswords2.Visibility = Visibility.Collapsed;

            errorLogin.Visibility = Visibility.Collapsed;

            int countPass = 0;

            Regex regex = new Regex(@"[А-я]+");
            Regex regex2 = new Regex(@"[0-9]+");

            if (regex.Matches(passBox.Password).Count == 0 && passBox.Password.Length >= 8 && regex2.Matches(passBox.Password).Count >= 1)
            {
                countPass++;
            }
            else
            {
                errorPasswords2.Visibility = Visibility.Visible;
            }

            regex = new Regex(@"[А-я]+");

            if (regex.Matches(login.Text).Count == 0)
            {
                countPass++;
            }
            else
            {
                errorLogin.Visibility = Visibility.Visible;
            }

            if (countPass == 2)
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    addUser();
            }
        }

        private void second_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkEmptyBox();
        }

        private void first_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkEmptyBox();
        }

        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkEmptyBox();
        }

        private void pass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            checkEmptyBox();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Content = new autorization();
        }
    }
}
