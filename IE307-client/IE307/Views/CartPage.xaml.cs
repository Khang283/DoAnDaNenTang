using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IE307.Share;
using IE307.Models;
using System.Diagnostics;

namespace IE307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
            GetCartList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetCartList();
        }

        private async void GetCartList()
        {
            HttpClient client = new HttpClient();
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("userId", Application.Current.Properties["userID"].ToString());
            StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8,"application/json");
            var result= await client.PostAsync("http://" + Utility.API_Endpoint + ":5001/cart", content);
            if (result.StatusCode.ToString() == "OK")
            {
                var resultContent=await result.Content.ReadAsStringAsync(); 
                var cart = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<Cart>(resultContent);
                cv_CartList.ItemsSource = cart.items;
                lb_TotalPrice.Text=cart.totalPrice.ToString()+" đ";  
            }
            else
            {
                lb_TotalPrice.Text = "0 đ";
            }
            
        }
        private async void Delete_Tapped(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Cảnh báo", "Bạn chắc chắn muốn xóa sản phẩm?", "Có", "KHÔNG");
            if (answer)
            {
                
            }

        }

        private void checkout_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CheckoutPage());
        }
    }
}