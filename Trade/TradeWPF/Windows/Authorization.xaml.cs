using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace TradeWPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private async void AuthButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            AuthButton.IsEnabled = false;
            if (!string.IsNullOrEmpty(LoginTextBox.Text) && !string.IsNullOrEmpty(PasswordPasswordBox.Password))
                await UserAuthorization(LoginTextBox.Text, PasswordPasswordBox.Password);
            else
                MessageBox.Show("Вы не заполнили данные!");
            AuthButton.IsEnabled = true;
        }

        public async Task UserAuthorization(string login, string password)
        {
            List<User> users = null;
            // получение списка пользователей
            var usersResponse = await App.httpClient.GetAsync($"https://localhost:7252/api/User");
            if (usersResponse.IsSuccessStatusCode)
                users = await usersResponse.Content.ReadFromJsonAsync<List<User>>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
            // получение пользователя
            var response = await App.httpClient.GetAsync($"https://localhost:7252/api/User/{users.Find(u => u.UserLogin == login).UserId}");
            if (response.IsSuccessStatusCode)
            {
                App.authUser = await response.Content.ReadFromJsonAsync<User>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
                if (App.authUser != null && App.authUser.UserPassword != password)
                    MessageBox.Show("Не верный логин или пароль!");
                else 
                { 
                    MessageBox.Show("Вы успешно вошли в систему"); 
                    Menu menu = new Menu();
                    this.Visibility = Visibility.Hidden;
                    menu.ShowDialog();
                    this.Visibility = Visibility.Visible;
                }
            }
            else
                MessageBox.Show("Пользователь не найден!");
        }
    }
}
