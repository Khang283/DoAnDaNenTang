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
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        private async void btn_ChangePassword_Clicked(object sender, EventArgs e)
        {
            var userID = Application.Current.Properties["userID"].ToString();
            var oldPassword = ent_LastPassword.Text;
            var newPassword = ent_NewPassword.Text;
            var newPasswordRetype=ent_NewPasswordRetype.Text;
            if (newPassword != newPasswordRetype)
            {
                await DisplayAlert("Thông báo", "Mật khẩu nhập lại không chính xác!", "OK");
            }
            else if(oldPassword=="" || newPassword=="" || newPasswordRetype == "")
            {
                await DisplayAlert("Thông báo", "Xin hãy nhập đủ thông tin" , "OK");
            }
            else if (oldPassword == newPassword)
            {
                await DisplayAlert("Thông báo", "Mật khẩu mới trùng với mật khẩu cũ!", "OK");
            }
            else
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("userID", userID);
                data.Add("oldPassword", oldPassword);
                data.Add("newPassword", newPassword);
                StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data),Encoding.UTF8,"application/json");
                HttpClient client = new HttpClient();
                var result = await client.PutAsync("http://" + Utility.API_Endpoint + ":5001/account/password", content);
                var response = result.StatusCode.ToString();
                if (response == "OK")
                {
                    await DisplayAlert("Thông báo", "Đã đổi mật khẩu thành công!", "OK");
                    await Shell.Current.GoToAsync("//login");
                }
                else if(response == "BadRequest")
                {
                    await DisplayAlert("Thông báo", "Mật khẩu cũ không chính xác!", "OK");
                }
                else
                {
                    await DisplayAlert("Thông báo", "Đã xảy ra lỗi!", "OK");
                }
            }
        }
    }
}