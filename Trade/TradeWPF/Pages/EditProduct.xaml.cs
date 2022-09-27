using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
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

namespace TradeWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Page
    {
        public EditProduct()
        {
            InitializeComponent();
        }

        private void EditProduct_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.actionProdutc == "add")
            {
                EditButton.Content = "Добавить";
                EditButton.Click += AddProducts;
            }
            if (App.actionProdutc == "edit")
            {
                ArticleTextBox.Text = App.selectedProduct.ProductArticleNumber;
                NameTextBox.Text = App.selectedProduct.ProductName;
                DescriptionTextBox.Text = App.selectedProduct.ProductDescription;
                CategoryTextBox.Text = App.selectedProduct.ProductCategory;
                ManufacturerTextBox.Text = App.selectedProduct.ProductManufacturer;
                CostTextBox.Text = App.selectedProduct.ProductCost.ToString();
                DiscountTextBox.Text = App.selectedProduct.ProductDiscountAmount.ToString();
                QuantityInStockTextBox.Text = App.selectedProduct.ProductQuantityInStock.ToString();
                ProviderTextBox.Text = App.selectedProduct.ProductProvider.ToString();
                MaxDiscountTextBox.Text = App.selectedProduct.ProductMaxDiscountAmount.ToString();
                UnitOfMeasurementTextBox.Text = App.selectedProduct.ProductUnitOfMeasurement;
                EditButton.Click += EditProducts;
            }
        }

        private async void AddProducts(object sender, RoutedEventArgs e)
        {
            try
            {
                Product newProduct = new Product
                {
                    ProductArticleNumber = ArticleTextBox.Text,
                    ProductCategory = CategoryTextBox.Text,
                    ProductPhoto = (string.IsNullOrEmpty(ProductImage.Source.ToString())) ? @"/Resources/picture.png" : ProductImage.Source.ToString(),
                    ProductCost = double.Parse(CostTextBox.Text),
                    ProductDiscountAmount = int.Parse(DiscountTextBox.Text),
                    ProductDescription = DescriptionTextBox.Text,
                    ProductManufacturer = ManufacturerTextBox.Text,
                    ProductMaxDiscountAmount = int.Parse(MaxDiscountTextBox.Text),
                    ProductName = NameTextBox.Text,
                    ProductProvider = ProviderTextBox.Text,
                    ProductQuantityInStock = int.Parse(QuantityInStockTextBox.Text),
                    ProductUnitOfMeasurement = UnitOfMeasurementTextBox.Text,
                    Orders = null
                };
                var content = JsonContent.Create(newProduct);
                var client = App.httpClient;
                var postResponse = await client.PostAsync("https://localhost:7252/api/Product", content);
                if (postResponse.IsSuccessStatusCode)
                {
                    App.mainFrame.RemoveBackEntry();
                    App.mainFrame.Navigate(new ListProduct());
                }
                else
                    MessageBox.Show("ОШИБКА!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async void EditProducts(object sender, RoutedEventArgs e)
        {
            var product = App.selectedProduct;
            var client = App.httpClient;
            var putResponse = await client.PutAsJsonAsync($"https://localhost:7252/api/Product/{App.selectedProductId + 1}", product);
            if (putResponse.IsSuccessStatusCode)
            {
                App.mainFrame.RemoveBackEntry();
                App.mainFrame.Navigate(new ListProduct());
            }
            else
                MessageBox.Show("Ошибка!");
        }

        private void ProductImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ProductImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
