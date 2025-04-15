using System;
using System.Windows;
using Yagnov_I212.DBmodel;

namespace Yagnov_I212
{
    public partial class MainWindow : Window
    {
        private readonly ShopDBEntities5 dbContext;
        private readonly Users _currentUser;

        public MainWindow(Users user)
        {
            InitializeComponent();
            dbContext = new ShopDBEntities5();
            _currentUser = user;
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string productName = ProductNameTextBox.Text;
                if (string.IsNullOrWhiteSpace(productName))
                {
                    MessageBox.Show("Введите название товара.");
                    return;
                }

                if (!decimal.TryParse(ProductPriceTextBox.Text, out decimal productPrice))
                {
                    MessageBox.Show("Введите корректную цену товара.");
                    return;
                }

                string productDescription = ProductDescriptionTextBox.Text;

                var newProduct = new Products
                {
                    ProductName = productName,
                    Price = productPrice,
                    Description = productDescription
                };

                dbContext.Products.Add(newProduct);
                dbContext.SaveChanges();

                MessageBox.Show("Товар успешно добавлен.");

                // Очистка полей после добавления
                ProductNameTextBox.Clear();
                ProductPriceTextBox.Clear();
                ProductDescriptionTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении товара: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Возврат к списку товаров, передаём текущего пользователя обратно
            DataGrid dataGridWindow = new DataGrid(_currentUser);
            dataGridWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Авторизация");
        }
    }
}
