using System;
using System.Collections.Generic;
using System.Linq;
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

        private void btn_UpdateInfo_Clicked(object sender, EventArgs e)
        {

        }
    }
}