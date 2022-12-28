﻿using IE307.Models;
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
        public ProductsDetailPage()
        {
            InitializeComponent();
        }

        public ProductsDetailPage(Product product)
        {
            InitializeComponent();
            Title = product.name;
            SV_ProductDetail.BindingContext = product;
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