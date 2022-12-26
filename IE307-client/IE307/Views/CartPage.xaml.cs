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
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
        }


        private async void Delete_Tapped(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Cảnh báo", "Bạn chắc chắn muốn xóa sản phẩm?", "Có", "KHÔNG");
            if (answer)
            {

            }

        }
    }
}