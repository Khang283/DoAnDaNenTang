using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IE307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        public ICommand HelpCommand => new Command<string>(async (url) => await Browser.OpenAsync(url));
        public ICommand TermsCommand => new Command<string>(async (url) => await Browser.OpenAsync(url));
        public UserPage()
        {
            InitializeComponent();
            /*session
                circleci*/
            //BindingContext = this;
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lb_Username.Text = Application.Current.Properties["username"].ToString();
            lb_Email.Text = Application.Current.Properties["email"].ToString();
        }

        private void editProfile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditProfilePage());
        }

        private void btnLogout_clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//login");
        }
    }
}