using System;
using System.Windows;
using Pract1_Florich_I223.DBmodel;

namespace Pract1_Florich_I223
{
    public partial class EditWindow : Window
    {
        private ShopDBEntities5 _dbContext;
        private Products _product;

        public EditWindow(Products product, ShopDBEntities5 dbContext)
        {
            InitializeComponent();

            _dbContext = dbContext;
            _product = product ?? new Products();

            // Заполняем поля данными
            if (_product.ProductID > 0) // Редактирование существующего товара
            {
                Title = "Редактирование товара";
            }
            else // Добавление нового товара
            {
                Title = "Добавление нового товара";
            }

            // Инициализация привязки данных
            DataContext = this;
        }

        public Products CurrentProduct => _product;

        public string WindowTitle => _product.ProductID > 0 ? "Редактирование товара" : "Добавление товара";

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Валидация данных
                if (string.IsNullOrWhiteSpace(_product.ProductName))
                {
                    MessageBox.Show("Введите название товара", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_product.Price <= 0)
                {
                    MessageBox.Show("Цена должна быть больше нуля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Если товар новый - добавляем в контекст
                if (_product.ProductID == 0)
                {
                    _dbContext.Products.Add(_product);
                }

                _dbContext.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}