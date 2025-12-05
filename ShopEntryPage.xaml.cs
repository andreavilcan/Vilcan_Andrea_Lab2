using System;
using Microsoft.Maui.Controls;
using Vilcan_Andrea_Lab7.Models;

namespace Vilcan_Andrea_Lab7;

public partial class ShopEntryPage : ContentPage
{
    public ShopEntryPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetShopsAsync();
    }

    async void OnShopAddedClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShopPage
        {
            BindingContext = new Shop()
        });
    }

    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Shop selectedShop)
        {
            await Navigation.PushAsync(new ShopPage
            {
                BindingContext = selectedShop
            });
        }

        ((ListView)sender).SelectedItem = null; // deselect
    }
}