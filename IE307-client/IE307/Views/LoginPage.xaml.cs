
using IE307.Models;
using IE307.Share;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btn_register_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private async void btn_login_Clicked(object sender, EventArgs e)
        {
            string username = ent_Username.Text;
            string password = ent_Password.Text;
            Account account = new Account
            {
                username = username,
                password = password,
            };
            StringContent request_data = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(account),System.Text.Encoding.UTF8,"application/json");
            HttpClient http = new HttpClient();
            var result = await http.PostAsync("http://" + Utility.API_Endpoint + ":5001/account/login", request_data);
            if (result.StatusCode.ToString()=="OK")
            {
                var content = await result.Content.ReadAsStringAsync();
                var accountInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<Account>(content);
                Application.Current.Properties["username"] = accountInfo.username;
                Application.Current.Properties["email"] = accountInfo.email ;
                Application.Current.Properties["phone"] = accountInfo.phone;
                Application.Current.Properties["address"] = accountInfo.address;
                await Shell.Current.GoToAsync("//home");
            }
            else if (result.StatusCode.ToString()=="NotFound")
            {
                await DisplayAlert("Thông báo", "Không tìm thấy tài khoản !", "OK");
            }
            else
            {
                await DisplayAlert("Thông báo", "Lỗi Server !", "OK");
            }
        }
    }
}