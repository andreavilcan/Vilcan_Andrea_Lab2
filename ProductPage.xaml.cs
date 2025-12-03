using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Vilcan_Andrea_Lab7.Models;

namespace Vilcan_Andrea_Lab7
{
    public partial class ProductPage : ContentPage
    {
        private ShopList sl;

        public ProductPage(ShopList slist)
        {
            InitializeComponent();
            sl = slist;
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var product = (Product)BindingContext;
            await App.Database.SaveProductAsync(product);
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var product = listView.SelectedItem as Product;
            if (product == null)
                return;

            await App.Database.DeleteProductAsync(product);
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }

        // Adaugă produsul selectat la lista de cumpărături și revine la ListPage
        async void OnAddButtonClicked(object sender, EventArgs e)
        {
            if (listView.SelectedItem is Product p)
            {
                var lp = new ListProduct
                {
                    ShopListID = sl.ID,
                    ProductID = p.ID
                };

                await App.Database.SaveListProductAsync(lp);
                p.ListProducts = new List<ListProduct> { lp };

                await Navigation.PopAsync();
            }
        }
    }
}