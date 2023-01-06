using IE307.Models;
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
    public partial class HistoryDetailPage : ContentPage
    {
        public HistoryDetailPage()
        {
            InitializeComponent();
        }

        public HistoryDetailPage(Cart cart)
        {
            InitializeComponent();
            cv_CartList.ItemsSource = cart.items;
            SV_CartDetail.BindingContext = cart;    
        }
    }
}