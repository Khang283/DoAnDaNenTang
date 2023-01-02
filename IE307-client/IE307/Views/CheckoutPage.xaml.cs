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
    public partial class CheckoutPage : ContentPage
    {
        public CheckoutPage()
        {
            InitializeComponent();
        }

        private async void order_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Thông báo", "Đặt hàng thành công", "OK");

        }
    }
}