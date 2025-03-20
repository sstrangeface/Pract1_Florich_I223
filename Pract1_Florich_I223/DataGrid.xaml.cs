using System;
using System.Linq;
using System.Windows;
using Pract1_Florich_I223.DBmodel;

namespace Pract1_Florich_I223
{
    public partial class DataGrid : Window
    {
        // Контекст базы данных
        private ShopDBEntities3 _dbContext;

        public DataGrid()
        {
            InitializeComponent();

            // Инициализация контекста базы данных
            _dbContext = new ShopDBEntities3();

            // Загрузка данных из таблицы Products
            LoadProducts();
        }

        // Метод для загрузки данных из таблицы Products
        private void LoadProducts()
        {
            try
            {
                // Получаем данные из таблицы Products
                var products = _dbContext.Products.ToList();

                // Привязываем данные к DataGrid
                ProductsDataGrid.ItemsSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        // Обработчик кнопки ДОБАВИТЬ
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        // Обработчик кнопки УДАЛИТЬ
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            DelWindow delWindow = new DelWindow();
            delWindow.Show();
            this.Close();
        }
    }
}