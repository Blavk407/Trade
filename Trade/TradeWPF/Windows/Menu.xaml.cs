using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
            App.mainFrame = MainFrame;
            App.mainFrame.Navigate(new Pages.ListProduct());
        }

        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            FIOTextBlock.Text = App.authUser.UserSurname + " " + App.authUser.UserName + " " + App.authUser.UserPatronymic;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
