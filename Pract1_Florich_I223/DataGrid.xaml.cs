using System;
using System.IO;
using System.Linq;
using System.Windows;
using Pract1_Florich_I223.DBmodel;

namespace Pract1_Florich_I223
{
    public partial class DataGrid : Window
    {
        // Контекст базы данных
        private ShopDBEntities5 _dbContext;

        public DataGrid()
        {
            InitializeComponent();

            // Инициализация контекста базы данных
            _dbContext = new ShopDBEntities5();

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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ProductsDataGrid.SelectedItem as Products;
            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите товар для редактирования.");
                return;
            }

            // Создаем копию товара для редактирования
            var productToEdit = new Products
            {
                ProductID = selectedProduct.ProductID,
                ProductName = selectedProduct.ProductName,
                Price = selectedProduct.Price,
                Description = selectedProduct.Description
            };

            var editWindow = new EditWindow(productToEdit, _dbContext);
            if (editWindow.ShowDialog() == true)
            {
                // Обновляем оригинальный товар, если редактирование успешно
                selectedProduct.ProductName = productToEdit.ProductName;
                selectedProduct.Price = productToEdit.Price;
                selectedProduct.Description = productToEdit.Description;

                _dbContext.SaveChanges();
                ProductsDataGrid.Items.Refresh(); // Обновляем отображение
            }
        }

        // Обработчик кнопки УДАЛИТЬ
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ProductsDataGrid.SelectedItem as Products;
            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите товар для удаления.");
                return;
            }

            // Удаление без подтверждения (минимальная версия)
            _dbContext.Products.Remove(selectedProduct);
            _dbContext.SaveChanges();
            LoadProducts(); // Обновляем список товаров
        }

        // Обработчик кнопки ЭКСПОРТИРОВАТЬ
        private void ExpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем текущую дату для имени файла
                var time = DateTime.Today.ToShortDateString().Replace('.', '-');
                string filePath = $"{time}_report.csv";

                // Получаем данные из DataGrid
                var data = ProductsDataGrid.ItemsSource.Cast<Products>().ToList();

                // Создаем CSV-контент
                var csvContent = new System.Text.StringBuilder();

                // Добавляем заголовки столбцов с разделителем ";" (Excel лучше его понимает)
                csvContent.AppendLine("Название;Цена;Описание");

                // Добавляем данные по каждому товару
                foreach (var item in data)
                {
                    // Экранируем специальные символы и используем точку с запятой как разделитель
                    csvContent.AppendLine(
                        $"\"{item.ProductName?.Replace("\"", "\"\"")}\";" +
                        $"{item.Price.ToString().Replace(",", ".")};" +
                        $"\"{item.Description?.Replace("\"", "\"\"")}\"");
                }

                // Сохраняем файл с кодировкой UTF-8 с BOM (для корректного отображения в Excel)
                File.WriteAllText(filePath, csvContent.ToString(), System.Text.Encoding.UTF8);

                // Показываем сообщение об успехе
                MessageBox.Show($"Данные успешно экспортированы в файл: {Path.GetFullPath(filePath)}",
                              "Экспорт завершен",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте: {ex.Message}",
                              "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        // Обработчик кнопки ВЫХОД
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрываем текущее окно
            this.Close();

            // Открываем окно авторизации
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
        }
    }
}