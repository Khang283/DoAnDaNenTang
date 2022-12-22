
using IE307.Models;
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
            var result = await http.PostAsync("http://172.30.203.165:5001/account/login", request_data);
            if (result.StatusCode.ToString()=="OK")
            {
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