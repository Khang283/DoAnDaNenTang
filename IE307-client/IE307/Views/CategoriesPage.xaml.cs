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
    public partial class CategoriesPage : ContentPage
    {
        public CategoriesPage()
        {
            InitializeComponent();
            LoadList();
        }

        public async void LoadList()
        {
            HttpClient http = new HttpClient();
            var result = await http.GetAsync("http://192.168.0.102:5001/products");
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

        int favouriteTapCount = 0;
        private void ImgAddToWishlist_Tapped(object sender, EventArgs e)
        {
            favouriteTapCount++;
            Image img = sender as Image;
            img.Source = favouriteTapCount % 2 == 0 ? "heart.png" : "redheart.png";
        }
    }
}