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
using System.ComponentModel;

namespace IE307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
            //GetCartList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            cv_CartList.ItemsSource = null;
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
                var items = ((TappedEventArgs)e).Parameter;
                var productID = items;
                var userID = Application.Current.Properties["userID"].ToString();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("userID", userID);
                data.Add("productID", productID);
                StringContent content=new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data),Encoding.UTF8,"application/json");
                HttpClient client = new HttpClient();
                var result = await client.PutAsync("http://" + Utility.API_Endpoint + ":5001/cart/delete", content);
                var response= result.StatusCode.ToString();
                if (response == "OK")
                {
                    //cv_CartList.ItemsSource = null;
                    GetCartList();
                }
                else
                {
                    await DisplayAlert("Thông báo", "Đã xảy ra lỗi", "OK");
                }
            }

        }

        private async void btn_ReduceItem_Clicked(object sender, EventArgs e)
        {
            var sp = (Button)sender;
            var items = sp.CommandParameter;
            var productID = items;
            var userID = Application.Current.Properties["userID"].ToString();
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("userID", userID);
            data.Add("productID", productID);
            StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var result = await client.PutAsync("http://" + Utility.API_Endpoint + ":5001/cart/reduce", content);
            var response = result.StatusCode.ToString();
            if (response == "OK")
            {
                GetCartList();
            }
            else if (response == "NotAcceptable")
            {
                var anwser=await DisplayAlert("Thông báo", "Bạn có muốn xóa sản phẩm khỏi giỏ hàng", "YES", "NO");
                if (anwser)
                {
                    StringContent request = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    HttpClient http = new HttpClient();
                    var kq = await http.PutAsync("http://" + Utility.API_Endpoint + ":5001/cart/delete", request);
                    var res = kq.StatusCode.ToString();
                    if (res == "OK")
                    {
                        //cv_CartList.ItemsSource = null;
                        GetCartList();
                    }
                    else
                    {
                        await DisplayAlert("Thông báo", "Đã xảy ra lỗi", "OK");
                    }
                }
            }
            else
            {
                await DisplayAlert("Thông báo", "Đã xảy ra lỗi", "OK");
            }
        }

        private async void btn_IncreaseItem_Clicked(object sender, EventArgs e)
        {
            var sp = (Button)sender;
            var items = sp.CommandParameter;
            var productID = items;
            var userID = Application.Current.Properties["userID"].ToString();
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("userID", userID);
            data.Add("productID", productID);
            StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var result = await client.PutAsync("http://" + Utility.API_Endpoint + ":5001/cart/inc", content);
            var response = result.StatusCode.ToString();
            if (response == "OK")
            {
                GetCartList();
            }
            else
            {
                await DisplayAlert("Thông báo", "Đã xảy ra lỗi", "OK");
            }
        }

        private void checkout_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CheckoutPage());
        }
    }
}