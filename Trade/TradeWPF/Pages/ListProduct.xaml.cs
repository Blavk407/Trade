using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
    /// Логика взаимодействия для ListProduct.xaml
    /// </summary>
    public partial class ListProduct : Page
    {
        public ListProduct()
        {
            InitializeComponent();
        }

        public Product? CurrentProduct { get; private set; }

        private List<Product> _products = new();

        private async void ListProduct_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.authUser.UserRole != 1)
            {
                AddButton.Visibility = Visibility.Hidden;
                EditButton.Visibility = Visibility.Hidden;
                DeleteButton.Visibility = Visibility.Hidden;
            }
            var response = await App.httpClient.GetAsync($"https://localhost:7252/api/Product");
            if (response.IsSuccessStatusCode)
            {
                _products = await response.Content.ReadFromJsonAsync<List<Product>>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
                // картинка заглушка
                for (int i = 0; i < _products.Count; i++)
                    if (_products[i].ProductPhoto == "")
                        _products[i].ProductPhoto = @"/Resources/picture.png";
                // Имеется в наличии
                for (int i = 0; i < _products.Count; i++)
                    if (_products[i].ProductQuantityInStock > 0)
                        _products[i].AvailableInStock = true;
                ProductsListView.ItemsSource = _products;
            }
            CountStringsLabel.Content = _products.Count.ToString() + " товара(-ов)";
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Поиск продукта по наименованию
            var finalProducts = new List<Product>();
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
                finalProducts = _products;
            else
                finalProducts = _products.Where(x => x.ProductName.Contains(SearchTextBox.Text, StringComparison.InvariantCultureIgnoreCase)).ToList();
            ProductsListView.ItemsSource = finalProducts;
            // Выбор до поиска выбранного продукта
            if (CurrentProduct != null && finalProducts.Contains(CurrentProduct))
            {
                ProductsListView.SelectedItem = CurrentProduct;
            }
        }

        private void AscendingButton_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductsListView.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Ascending));
        }

        private void DescendingButton_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductsListView.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Descending));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            App.actionProdutc = "add";
            App.mainFrame.Navigate(new EditProduct());
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            App.actionProdutc = "edit";
            if (App.selectedProduct == null)
                MessageBox.Show("Вы не выбрали товар!");
            else
                App.mainFrame.Navigate(new EditProduct());
        }

        private void ProductsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.selectedProduct = ProductsListView.SelectedItem as Product;
            App.selectedProductId = _products.IndexOf(App.selectedProduct);
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var element = (Product)ProductsListView.SelectedItem;
            var client = App.httpClient;
            var deleteResponse = await client.DeleteAsync($"https://localhost:7252/api/Products/{_products.IndexOf(element) + 1}");
            if (deleteResponse.IsSuccessStatusCode)
            {
                _products.Remove(element);
                ProductsListView.Items.Remove(element);
            }
            else
                MessageBox.Show("ОШИБКА!");
        }
    }
}
