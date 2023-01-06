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
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
            ListCartHistory();
        }

        private async void ListCartHistory()
        {
            var userID = Application.Current.Properties["userID"].ToString();
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("userID", userID);
            StringContent content=new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data),Encoding.UTF8,"application/json");
            HttpClient client = new HttpClient();
            var result = await client.PostAsync("http://" + Utility.API_Endpoint + ":5001/cart/history", content);
            var response = result.StatusCode.ToString();
            if (response == "OK")
            {
                var cartList =await result.Content.ReadAsStringAsync();
                var list = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<List<Cart>>(cartList);
                CV_CartHistory.ItemsSource = list;
            }
            else
            {
                await DisplayAlert("Thông báo", "Đã xảy ra lỗi", "OK");
            }
        }

        private void btn_More_Clicked(object sender, EventArgs e)
        {
            var item = ((Button)sender).CommandParameter;
            var cart = item as Cart;
            Navigation.PushAsync(new HistoryDetailPage(cart));
        }

        private void CV_CartHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = CV_CartHistory.SelectedItem as Cart;
            Navigation.PushAsync(new HistoryDetailPage(item));
        }
    }
}