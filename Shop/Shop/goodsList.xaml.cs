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

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для goodsList.xaml
    /// </summary>
    public partial class goodsList : Page
    {
        SqlConnection sqlConnection;
        public goodsList()
        {
            InitializeComponent();
            connectDB();

            List<Good> goods = new List<Good>();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM Товары", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    goods.Add(new Good() { ID = sqlReader.GetInt32(0), Name = sqlReader.GetString(1), Pharmacy = sqlReader.GetString(2), Amount = sqlReader.GetInt32(3), Address = sqlReader.GetString(4) });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (sqlReader != null)
                sqlReader.Close();

            if (goods.Count != 0)
                listGoods.ItemsSource = goods;
        }
        private void connectDB()
        {

            string connectionStr = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetFullPath("../../shopDB.mdf")};";

            sqlConnection = new SqlConnection(connectionStr);

            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Content = new autorization();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Content = new edit();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Content = new del();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Content = new select();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Content = new add();
        }
    }
}
