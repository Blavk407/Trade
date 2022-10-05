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

        private List<Product> _products = new();
        private int countStrings = 0;

        private async void ListProduct_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.authUser == null || App.authUser.UserRole != 1)
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
            var responseManufacturer = await App.httpClient.GetAsync($"https://localhost:7252/api/Product/Manufacturers");
            if (responseManufacturer.IsSuccessStatusCode)
            {
                var manufacturers = await responseManufacturer.Content.ReadFromJsonAsync<List<string>>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
                manufacturers.Add("Все производители");
                FiltrationComboBox.ItemsSource = manufacturers;
            }
            countStrings = _products.Count;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Поиск продукта по наименованию
            var finalProducts = new List<Product>();
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
                finalProducts = _products;
            else if (string.IsNullOrEmpty(filterManufacturer))
                finalProducts = _products.Where(x => x.ProductName.Contains(SearchTextBox.Text, StringComparison.InvariantCultureIgnoreCase)).ToList();
            else
                finalProducts = _products.Where(x => x.ProductName.Contains(SearchTextBox.Text, StringComparison.InvariantCultureIgnoreCase) && x.ProductManufacturer == filterManufacturer).ToList();
            ProductsListView.ItemsSource = finalProducts;
            CountStringsLabel.Content = finalProducts.Count.ToString() + " из " + countStrings + " товаров";
            // Выбор до поиска выбранного продукта
            if (App.selectedProduct != null && finalProducts.Contains(App.selectedProduct))
            {
                ProductsListView.SelectedItem = App.selectedProduct;
            }
            SortingCost();
        }

        private void AscendingButton_Click(object sender, RoutedEventArgs e)
        {
            Ascending();
        }

        private void Ascending()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductsListView.Items);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("ProductCost", ListSortDirection.Ascending));
            ascending = true;
            descending = false;
            ProductsListView.Items.Refresh();
        }

        private void DescendingButton_Click(object sender, RoutedEventArgs e)
        {
            Descending();
        }

        private void Descending()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductsListView.Items);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("ProductCost", ListSortDirection.Descending));
            descending = true;
            ascending = false;
            ProductsListView.Items.Refresh();
        }

        private bool descending = false;
        private bool ascending = false;

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
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var element = (Product)ProductsListView.SelectedItem;
            var client = App.httpClient;
            var deleteResponse = await client.DeleteAsync($"https://localhost:7252/api/Products?article={App.selectedProduct.ProductArticleNumber}");
            if (deleteResponse.IsSuccessStatusCode)
            {
                _products.Remove(element);
                ProductsListView.Items.Remove(element);
            }
            else
                MessageBox.Show("ОШИБКА!");
        }

        private string filterManufacturer;

        private void FiltrationComboBox_Selected(object sender, RoutedEventArgs e)
        {
            var finalProducts = new List<Product>();
            if (FiltrationComboBox.SelectedValue.ToString() != "Все производители")
            {
                finalProducts = _products.Where(x => x.ProductManufacturer.Contains(FiltrationComboBox.SelectedValue.ToString(), StringComparison.InvariantCultureIgnoreCase)).ToList();
                filterManufacturer = FiltrationComboBox.SelectedValue.ToString();
            }
            else
                finalProducts = _products;
            if (!string.IsNullOrWhiteSpace(SearchTextBox.Text))
                finalProducts = finalProducts.Where(x => x.ProductName.Contains(SearchTextBox.Text, StringComparison.InvariantCultureIgnoreCase)).ToList();
            ProductsListView.ItemsSource = finalProducts;
            CountStringsLabel.Content = finalProducts.Count.ToString() + " из " + countStrings + " товаров";
            // Выбор до поиска выбранного продукта
            if (App.selectedProduct != null && finalProducts.Contains(App.selectedProduct))
            {
                ProductsListView.SelectedItem = App.selectedProduct;
            }
            SortingCost();
        }

        private void SortingCost()
        {
            if (descending)
                Descending();
            if (ascending)
                Ascending();
        }
    }
}
