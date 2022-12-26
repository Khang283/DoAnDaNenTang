using IE307.Models;
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

namespace IE307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            Title = "Đăng ký";
        }

        private void btn_Login_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private async void btn_Register_Clicked(object sender, EventArgs e)
        {
            var username=ent_Username.Text; 
            var password=ent_Password.Text; 
            var password_retype=ent_PasswordRetype.Text;
            var email=ent_Email.Text;   
            if(username==null || password == null || email==null || password_retype==null)
            {
                await DisplayAlert("Thông báo", "Bạn chưa điền đầy đủ thông tin", "OK");
            }
            else if (password != password_retype)
            {
                await DisplayAlert("Thông báo", "Nhập lại mật khẩu !", "OK");
            }
            else
            {
                HttpClient http = new HttpClient();
                var account = new Account
                {
                    username = username,
                    password = password,
                    email = email,
                };
                StringContent request_data = new StringContent(JsonConvert.SerializeObject(account), System.Text.Encoding.UTF8, "application/json");
                var result =await http.PostAsync("http://"+Utility.API_Endpoint+":5001/account/register", request_data);
                if (result.StatusCode.ToString() == "OK")
                {
                    await DisplayAlert("Thông báo", "Tạo tài khoản thành công", "OK");
                    await Navigation.PopAsync();
                }
                else if (result.StatusCode.ToString() == "BadRequest")
                {
                    await DisplayAlert("Thông báo", "Tài khoản đã tồn tại !", "OK");
                }
                else
                {
                    await DisplayAlert("Thông báo", "Lỗi Server !", "OK");
                }
            }
        }
    }
}