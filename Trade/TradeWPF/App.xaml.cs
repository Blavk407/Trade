using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TradeWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User? authUser;

        public static Product? selectedProduct;

        public static int? selectedProductId;

        public static string actionProdutc;

        public static Frame mainFrame = new();

        public static readonly HttpClient httpClient = new HttpClient();

        public App() : base()
        {
            Exit += App_Exit;
        }


        private void App_Exit(object sender, ExitEventArgs e)
        {
            httpClient.Dispose();
        }
    }
}
