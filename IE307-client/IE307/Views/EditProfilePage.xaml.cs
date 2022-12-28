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
    public partial class EditProfilePage : ContentPage
    {
        public EditProfilePage()
        {
            InitializeComponent();
            SetEntry();
        }

        public void SetEntry()
        {
            ent_Username.Text = Application.Current.Properties["username"].ToString();
            ent_Email.Text = Application.Current.Properties["email"].ToString();
            try
            {
                ent_Phone.Text = Application.Current.Properties["phone"].ToString();
            }
            catch
            {
                ent_Phone.Text = "";
            }

            try
            {
                ent_Address.Text = Application.Current.Properties["address"].ToString();
            }
            catch
            {
                ent_Address.Text = "";
            }
            
        }

        private async void btn_UpdateInfo_Clicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Thông báo", "Bạn có chắc muốn thay đổi thông tin ?", "YES", "NO");
            if (confirm)
            {
                var userID = Application.Current.Properties["userID"].ToString();
                var username = ent_Username.Text;
                var email = ent_Email.Text;
                var phone = ent_Phone.Text;
                var address = ent_Address.Text;
                HttpClient client = new HttpClient();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("userID", userID);
                data.Add("username", username);
                data.Add("email", email);
                data.Add("phone", phone);
                data.Add("address", address);
                StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var result = await client.PutAsync("http://" + Utility.API_Endpoint + ":5001/account/update", content);
                if (result.StatusCode.ToString() == "OK")
                {
                    await DisplayAlert("Thông báo", "Cập nhật thành công!", "OK");
                    Application.Current.Properties["username"] = username;
                    Application.Current.Properties["email"] = email;
                    Application.Current.Properties["phone"] = phone;
                    Application.Current.Properties["address"] = address;
                }
                else if (result.StatusCode.ToString() == "BADREQUEST")
                {
                    await DisplayAlert("Thông báo", "Tên tài khoản đã tồn tại", "OK");
                }
                else
                {
                    await DisplayAlert("Thông báo", "Không tìm thấy tài khoản", "OK");
                }
            }
        }
    }
}