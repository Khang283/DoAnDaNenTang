using IE307.Models;
using IE307.Share;
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
    public partial class WishlistPage : ContentPage
    {
        public WishlistPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadList();
        }

        private async void LoadList()
        {
            var userID = Application.Current.Properties["userID"].ToString();
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("userID", userID);
            StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpClient client=new HttpClient();
            var result = await client.PostAsync("http://" + Utility.API_Endpoint + ":5001/products/favorite", content);
            var response = result.StatusCode.ToString();
            if (response == "OK")
            {
                var json = await result.Content.ReadAsStringAsync();
                var favorite = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<List<Favorite>>(json);
                CV_Favorite.ItemsSource = favorite;
            }
            else
            {
                await DisplayAlert("Thông báo", "Đã xảy ra lỗi", "OK");
            }
        }

        private void CV_Favorite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Favorite favorite = CV_Favorite.SelectedItem as Favorite;
            Navigation.PushAsync(new ProductsDetailPage(favorite.item)); 
        }


        //int favouriteTapCount = 1;
        //private void ImgAddToWishlist_Tapped(object sender, EventArgs e)
        //{
        //    favouriteTapCount++;
        //    Image img = sender as Image;
        //    img.Source = favouriteTapCount % 2 == 0 ? "heart.png" : "redheart.png";
        //}
    }
}