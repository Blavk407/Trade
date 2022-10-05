using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Point = System.Drawing.Point;

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
                if (captchaText == string.Empty)
                    await UserAuthorization(LoginTextBox.Text, PasswordPasswordBox.Password);
                else if (captchaText == CaptchaTextBox.Text)
                {
                    await UserAuthorization(LoginTextBox.Text, PasswordPasswordBox.Password);
                    captchaText = string.Empty;
                    CaptchaImage.Visibility = Visibility.Hidden;
                    CaptchaTextBox.Visibility = Visibility.Hidden;
                }
                else
                    MessageBox.Show("Вы не верно ввели капчу!");
            else
                MessageBox.Show("Вы не заполнили данные!");
            AuthButton.IsEnabled = true;
        }

        private string captchaText = string.Empty;

        public async Task UserAuthorization(string login, string password)
        {
            List<User> users = null;
            // получение списка пользователей
            var usersResponse = await App.httpClient.GetAsync($"https://localhost:7252/api/User");
            if (usersResponse.IsSuccessStatusCode)
                users = await usersResponse.Content.ReadFromJsonAsync<List<User>>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
            // получение пользователя
            if (users.Find(u => u.UserLogin == login) != null)
            {
                var response = await App.httpClient.GetAsync($"https://localhost:7252/api/User/{users.Find(u => u.UserLogin == login).UserId}");
                if (response.IsSuccessStatusCode)
                {
                    App.authUser = await response.Content.ReadFromJsonAsync<User>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
                    if (App.authUser != null && App.authUser.UserPassword != password)
                    {
                        MessageBox.Show("Не верный логин или пароль!");
                        await ActivateCaptcha();
                    }
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
            else
            {
                MessageBox.Show("Не верный логин или пароль!");
                await ActivateCaptcha();
            }
        }

        private async Task ActivateCaptcha()
        {
            captchaText = await CreateCaptcha(400, 150);
            AuthButton.Margin = new Thickness(0, 250, 0, 0);
            AuthButton.VerticalAlignment = VerticalAlignment.Bottom;
            CaptchaImage.Visibility = Visibility.Visible;
            CaptchaTextBox.Visibility = Visibility.Visible;
            AuthButton.IsEnabled = false;
            Thread.Sleep(10000);
            AuthButton.IsEnabled = true;
        }

        private const string chars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
            "zyxwvutsrqponmlkjihgfedcba" +
            "1234567890";

        private Brush[] colorsChar = {
            new SolidBrush(Color.FromArgb(235, 134, 134)),
            new SolidBrush(Color.FromArgb(235, 134, 164)),
            new SolidBrush(Color.FromArgb(235, 134, 201)),
            new SolidBrush(Color.FromArgb(226, 134, 235)),
            new SolidBrush(Color.FromArgb(191, 134, 235)),
            new SolidBrush(Color.FromArgb(154, 134, 235)),
            new SolidBrush(Color.FromArgb(134, 162, 235)),
            new SolidBrush(Color.FromArgb(134, 186, 235)),
            new SolidBrush(Color.FromArgb(134, 223, 235)),
            new SolidBrush(Color.FromArgb(134, 235, 186)),
            new SolidBrush(Color.FromArgb(134, 235, 137)),
            new SolidBrush(Color.FromArgb(172, 235, 134)),
            new SolidBrush(Color.FromArgb(214, 235, 134)),
            new SolidBrush(Color.FromArgb(235, 206, 134)),
            new SolidBrush(Color.FromArgb(235, 157, 134)),
        };
        private Brush[] colorsBackground = {
            new SolidBrush(Color.FromArgb(56, 6, 6)),
            new SolidBrush(Color.FromArgb(56, 6, 27)),
            new SolidBrush(Color.FromArgb(56, 6, 39)),
            new SolidBrush(Color.FromArgb(43, 6, 56)),
            new SolidBrush(Color.FromArgb(25, 6, 56)),
            new SolidBrush(Color.FromArgb(6, 6, 56)),
            new SolidBrush(Color.FromArgb(6, 26, 56)),
            new SolidBrush(Color.FromArgb(6, 44, 56)),
            new SolidBrush(Color.FromArgb(6, 56, 47)),
            new SolidBrush(Color.FromArgb(6, 56, 29)),
            new SolidBrush(Color.FromArgb(6, 56, 7)),
            new SolidBrush(Color.FromArgb(25, 56, 6)),
            new SolidBrush(Color.FromArgb(46, 56, 6)),
            new SolidBrush(Color.FromArgb(56, 42, 6)),
            new SolidBrush(Color.FromArgb(56, 27, 6)),
        };
        private readonly Random _random = new();

        public async Task<string> CreateCaptcha(int width, int height)
        {
            Bitmap result = new(width, height);
            int Xpos = _random.Next(0, 65);
            int Ypos = _random.Next(15, height / 2 - height / 3);
            Graphics g = Graphics.FromImage((System.Drawing.Image)result);
            Color backRandColor = ((SolidBrush)colorsBackground[_random.Next(colorsBackground.Length)]).Color;
            g.Clear(backRandColor);
            string textCaptcha = new string(Enumerable.Repeat(chars, 6).Select(s => s[_random.Next(s.Length)]).ToArray());
            foreach (char symbol in textCaptcha)
            {
                g.DrawString(
                    symbol.ToString(),
                    new Font("Comic Sans MS", _random.Next(30, 46)),
                    colorsChar[_random.Next(0, colorsChar.Length)],
                    new PointF(Xpos, Ypos));
                Xpos += 36;
                Ypos = _random.Next(10, height / 3);
            }

            g.DrawLine(
                new System.Drawing.Pen(Color.Black, 5),
                new Point(0, height / 2),
                new Point(width - 1, height / 2));

            string path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net6.0-windows", "");
            CaptchaImage.Source = null;
            File.Delete($"{path}Resources\\captcha.jpg");
            using (FileStream image = new($"{path}Resources\\captcha.jpg", FileMode.CreateNew))
            {
                ImageConverter imageConverter = new();
                var bytes = (byte[])imageConverter.ConvertTo(result, typeof(byte[]))!;
                await image.WriteAsync(bytes);
            }
            BitmapImage captcha = new BitmapImage();
            captcha.BeginInit();
            captcha.UriSource = new Uri($"{path}Resources\\captcha.jpg");
            captcha.EndInit();
            CaptchaImage.Source = captcha;
            return textCaptcha;
        }

        private void GoToMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Menu menu = new Menu();
            this.Visibility = Visibility.Hidden;
            menu.ShowDialog();
            this.Visibility = Visibility.Visible;
            App.authUser = null;
        }
    }
}
