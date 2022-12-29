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
    public partial class ProductsDetailPage : ContentPage
    {
        public Product sp;
        public ProductsDetailPage()
        {
            InitializeComponent();
        }

        public ProductsDetailPage(Product product)
        {
            InitializeComponent();
            sp = product;   
            Title = product.name;
            SV_ProductDetail.BindingContext = product;
        }

        private async void btn_AddToCart_Clicked(object sender, EventArgs e)
        {
            var productID = sp.ProductId;
            var userID = Application.Current.Properties["userID"].ToString();
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("userID",userID);
            data.Add("productID", productID);
            StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var result = await client.PostAsync("http://" + Utility.API_Endpoint + ":5001/cart/add", content);
            var response = result.StatusCode.ToString();
            if (response == "OK" || response=="Created")
            {
                await DisplayAlert("Thông báo", "Đã thêm vào giỏ hàng !", "OK");
            }
            else if (response == "NotFound")
            {
                await DisplayAlert("Thông báo", "Sản phẩm không tồn tại", "OK");
            }
            else
            {
                await DisplayAlert("Thông báo", "Đã xảy ra lỗi", "OK");
            }
        }

        //private void btnMinus_Clicked(object sender, EventArgs e)
        //{
        //    var quantity = Convert.ToInt32(lb_Quanity.Text);
        //    if(quantity > 0)
        //    {
        //        quantity--;
        //        lb_Quanity.Text = quantity.ToString();
        //    }
        //}

        //private void btnPlus_Clicked(object sender, EventArgs e)
        //{
        //    var quantity = Convert.ToInt32(lb_Quanity.Text);
        //    quantity++;
        //    lb_Quanity.Text = quantity.ToString();
        //}

        //int favouriteTapCount = 0;

        //private void ImgAddToWishlist_Tapped(object sender, EventArgs e)
        //{
        //    favouriteTapCount++;
        //    Image img = sender as Image;
        //    img.Source = favouriteTapCount % 2 == 0 ? "heart.png" : "redheart.png";
        //}
    }
}