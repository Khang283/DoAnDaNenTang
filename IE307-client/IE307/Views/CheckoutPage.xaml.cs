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
    public partial class CheckoutPage : ContentPage
    {
        private string payment = "";
        public CheckoutPage()
        {
            InitializeComponent();
            GetCartList();
        }

        private async void GetCartList()
        {
            HttpClient client = new HttpClient();
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("userId", Application.Current.Properties["userID"].ToString());
            StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("http://" + Utility.API_Endpoint + ":5001/cart", content);
            if (result.StatusCode.ToString() == "OK")
            {
                var resultContent = await result.Content.ReadAsStringAsync();
                var cart = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<Cart>(resultContent);
                cv_CartList.ItemsSource = cart.items;
                lb_TotalPrice.Text = String.Format("{0:F3} đ", cart.totalPrice.ToString());
            }
            else
            {
                lb_TotalPrice.Text = "0 đ";
            }

        }

        private async void order_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Thông báo", "Bạn có muốn thanh toán giỏ hàng ?", "YES", "NO");
            if (answer)
            {
                var userID = Application.Current.Properties["userID"];
                var receiver = ent_Receiver.Text;
                var phone = ent_Phone.Text;
                var address = ent_Address.Text;
                if (receiver == null || phone == null || address == null || payment == "")
                {
                    await DisplayAlert("Thông báo", "Xin hãy điền đủ thông tin !", "OK");
                }
                else
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("userID", userID);
                    data.Add("receiver", receiver);
                    data.Add("phone", phone);
                    data.Add("address", address);
                    data.Add("pay", payment);
                    StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    HttpClient client = new HttpClient();
                    var result = await client.PostAsync("http://" + Utility.API_Endpoint + ":5001/cart/checkout", content);
                    var response = result.StatusCode.ToString();
                    if (response == "OK")
                    {
                        await DisplayAlert("Thông báo", "Giỏ hàng đã được thanh toán thành công", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Thông báo", "Đã xảy ra lỗi", "OK");
                    }
                }
            }
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var radiobtn=sender as RadioButton;
            payment = radiobtn.Content.ToString();
        }
    }
}