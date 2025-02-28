using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Pract1_Florich_I223.DBmodel;

namespace Pract1_Florich_I223
{
    public partial class MainWindow : Window
    {
        private ShopDBEntities2 dbContext;

        public MainWindow()
        {
            InitializeComponent();
            dbContext = new ShopDBEntities2();
            LoadProducts();
        }

        private void LoadProducts()
        {
            ProductsDataGrid.ItemsSource = dbContext.Products.ToList();
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string productName = ProductNameTextBox.Text;
                if (string.IsNullOrEmpty(productName))
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

                LoadProducts(); // Обновляем ListBox
                ProductNameTextBox.Clear();
                ProductPriceTextBox.Clear();
                ProductDescriptionTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении товара: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Логика для кнопки "Авторизация"
            MessageBox.Show("Авторизация");
        }
    }
}