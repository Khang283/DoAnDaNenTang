using IE307.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IE307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            LoadList();
        }

        public async void LoadList()
        {
            HttpClient http = new HttpClient();
            var result = await http.GetAsync("http://172.30.203.165:5001/products");
            var content= await result.Content.ReadAsStringAsync();
            try
            {
                var products=MongoDB.Bson.Serialization.BsonSerializer.Deserialize<List<Product>>(content);
                CV_BestSale.ItemsSource = products;
                CV_Recommend.ItemsSource = products;
            }
            catch(Exception err)
            {
                await DisplayAlert("Thông báo", err.ToString(), "OK");
            }
        }

        private void CV_BestSale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product product = CV_BestSale.SelectedItem as Product;
            Navigation.PushAsync(new ProductsDetailPage(product));
        }

        private void CV_Recommend_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product product = CV_Recommend.SelectedItem as Product;
            Navigation.PushAsync(new ProductsDetailPage(product));
        }
    }
}