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
    /// Логика взаимодействия для selectGood.xaml
    /// </summary>
    public partial class selectGood : Page
    {
        int realAmount = 0;
        string id = "";
        string id2 = "";
        SqlConnection sqlConnection;
        public selectGood()
        {
            InitializeComponent();
            connectDB();

            id = File.ReadAllText("Продукт.txt");
            id2 = File.ReadAllText("Пользователь.txt");
            amountBlock.Text = File.ReadAllText("Количество.txt");

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM Товары", sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                if(Convert.ToString(sqlReader["ID"]) == id)
                {
                    nameBlock.Text = Convert.ToString(sqlReader["Название"]);
                    addressBlock.Text = Convert.ToString(sqlReader["Адрес"]);
                    namePharm.Text = Convert.ToString(sqlReader["Аптека"]);
                }
            }

            if (sqlReader != null)
                sqlReader.Close();
        }
        private void connectDB()
        {
            string connectionStr = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetFullPath("../../shopDB.mdf")};";

            sqlConnection = new SqlConnection(connectionStr);

            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Content = new select();
        }

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                SqlCommand command = new SqlCommand("INSERT Заказы([ID пользователя],[ID товара],Количество,[Проверка на брак])" +
                    "VALUES(@idUser,@idGood,@amountGood,@status);", sqlConnection);

                command.Parameters.AddWithValue("idUser", Convert.ToInt32(id2));
                command.Parameters.AddWithValue("idGood", Convert.ToInt32(id));
                command.Parameters.AddWithValue("amountGood", Convert.ToInt32(amountBlock.Text));

                if(toggleStatus.IsChecked == true)
                {
                    command.Parameters.AddWithValue("status", "да");
                }
                else
                {
                    command.Parameters.AddWithValue("status", "нет");
                }
                
                command.ExecuteNonQuery();

                command = new SqlCommand($"UPDATE Товары SET Количество = @amount WHERE ID = @id", sqlConnection);
                command.Parameters.AddWithValue("id", Convert.ToInt32(id));

                findRealAmount();

                command.Parameters.AddWithValue("amount", realAmount - Convert.ToInt32(amountBlock.Text));
                command.ExecuteNonQuery();

                App.Current.MainWindow.Content = new final();
            }
        }

        private void findRealAmount()
        {
            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand($"SELECT * FROM Товары", sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {

                if (Convert.ToString(sqlReader["ID"]) == id)
                {
                    realAmount = Convert.ToInt32(sqlReader["Количество"]);
                    
                    if (sqlReader != null)
                        sqlReader.Close();
                    break;
                    
                }
            }

            if (sqlReader != null)
                sqlReader.Close();
        }

        
    }
}
