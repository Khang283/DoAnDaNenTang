using IE307.Models;
using IE307.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace IE307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
    {
        private string category;
        private int page;
        private ObservableCollection<Product> listProducts=new ObservableCollection<Product>();
        public CategoriesPage()
        {
            InitializeComponent();
            LoadList();
        }

        public CategoriesPage(string cat, int p)
        {
            InitializeComponent();
            category = cat;
            page = p;   
            Title = CategoryDefine(category);
            LoadList();
        }

        private String CategoryDefine(string category)
        {
            switch (category)
            {
                case "ring":
                    return "Nhẫn";
                case "necklace":
                    return "Dây chuyền";
                case "bracelet":
                    return "Vòng tay";
                case "gift":
                    return "Quà tặng";
                case "earing":
                    return "Bông tai";
                case "watch":
                    return "Đồng hồ";
                default:
                    return "Danh mục";
            }
        }

        public async void LoadList()
        {
            HttpClient http = new HttpClient();
            var result = await http.GetAsync("http://" + Utility.API_Endpoint + ":5001/products/"+category+"/"+page.ToString());
            var content = await result.Content.ReadAsStringAsync();
            try
            {
                var products = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<List<Product>>(content);
                foreach(var item in products)
                {
                    listProducts.Add(item);
                }
                CV_Category.ItemsSource = listProducts;
                CV_Category.RemainingItemsThreshold = 2;
                CV_Category.RemainingItemsThresholdReached += CV_Category_RemainingItemsThresholdReached;
            }
            catch (Exception err)
            {
                await DisplayAlert("Thông báo", err.ToString(), "OK");
            }
        }

        private async void CV_Category_RemainingItemsThresholdReached(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            page = page + 1;
            HttpClient client = new HttpClient();
            var result = await client.GetAsync("http://" + Utility.API_Endpoint + ":5001/products/" + category + "/" + page.ToString());
            var content = await result.Content.ReadAsStringAsync();
            var products = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<List<Product>>(content);
            foreach (var item in products)
            {
                listProducts.Add(item);
            }
        }

        private void CV_Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product product = CV_Category.SelectedItem as Product;
            Navigation.PushAsync(new ProductsDetailPage(product));
        }

        //int favouriteTapCount = 0;
        //private void ImgAddToWishlist_Tapped(object sender, EventArgs e)
        //{
        //    favouriteTapCount++;
        //    Image img = sender as Image;
        //    img.Source = favouriteTapCount % 2 == 0 ? "heart.png" : "redheart.png";
        //}
    }
}