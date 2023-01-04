using IE307.Models;
using IE307.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        //int favouriteTapCount = 0;
        public HomePage()
        {
            InitializeComponent();
            ListBestSale();
            ListRecommend();
        }

        private async void ListRecommend()
        {
            HttpClient http = new HttpClient();
            var result = await http.GetAsync("http://" + Utility.API_Endpoint + ":5001/products");
            var content = await result.Content.ReadAsStringAsync();
            try
            {
                var products = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<List<Product>>(content);
                CV_Recommend.ItemsSource = products;
            }
            catch (Exception err)
            {
                await DisplayAlert("Thông báo", err.ToString(), "OK");
            }
        }

        private async void ListBestSale()
        {
            HttpClient http = new HttpClient();
            var result = await http.GetAsync("http://" + Utility.API_Endpoint + ":5001/products/bestsale");
            var content = await result.Content.ReadAsStringAsync();
            try
            {
                var products = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<List<Product>>(content);
                CV_BestSale.ItemsSource = products;
            }
            catch (Exception err)
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

        private void Categories_Tapped(object sender, EventArgs e)
        {
            string category = ((TappedEventArgs)e).Parameter as String;
            int page = 1;
            Navigation.PushAsync(new CategoriesPage(category,page));
        }



        //private void ImgAddToWishlist_Tapped(object sender, EventArgs e)
        //{
        //    favouriteTapCount++;
        //    Image img = sender as Image;
        //    img.Source = favouriteTapCount % 2 == 0 ? "heart.png" : "redheart.png";
        //}
    }
}