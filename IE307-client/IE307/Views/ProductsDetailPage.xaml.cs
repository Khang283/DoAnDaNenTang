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
        public bool isFavorited;
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
            CheckFavorite();
        }


        private async void CheckFavorite()
        {
            var userID = Application.Current.Properties["userID"].ToString();
            var productID = sp.ProductId;
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("userID", userID);
            data.Add("productID", productID);
            StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var result = await client.PostAsync("http://" + Utility.API_Endpoint + ":5001/products/checkfavorite", content);
            var response = result.StatusCode.ToString();
            if (response == "OK")
            {
                isFavorited= true;
                btn_Favorite.ImageSource = "smallredheart.png";
            }
            else
            {
                isFavorited = false;
            }
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

        private async void btn_Favorite_Clicked(object sender, EventArgs e)
        {
            if (!isFavorited)
            {
                var userID = Application.Current.Properties["userID"].ToString();
                var productID = sp.ProductId;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("userID", userID);
                data.Add("productID", productID);
                StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var result = await client.PostAsync("http://" + Utility.API_Endpoint + ":5001/products/addToFavorite", content);
                var response = result.StatusCode.ToString();
                if (response == "OK")
                {
                    btn_Favorite.ImageSource = "smallredheart.png";
                    isFavorited = !isFavorited;
                    //await DisplayAlert("Thông báo", "Đã thêm vào yêu thích !", "OK");
                }
                else
                {
                    await DisplayAlert("Thông báo", "Đã xảy ra lỗi !", "OK");
                }
            }
            else
            {
                var userID = Application.Current.Properties["userID"].ToString();
                var productID = sp.ProductId;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("userID", userID);
                data.Add("productID", productID);
                StringContent reqdata = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var result=await client.PutAsync("http://"+Utility.API_Endpoint+":5001/products/removefavorite",reqdata);
                var response = result.StatusCode.ToString();
                if (response == "OK")
                {
                    isFavorited = !isFavorited;
                    btn_Favorite.ImageSource = "BlackHeart";
                    //await DisplayAlert("Thông báo", "Đã loại khỏi danh sách yêu thích", "OK");
                }
                else
                {
                    await DisplayAlert("Thông báo", "Đã xảy ra lỗi !", "OK");
                }
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