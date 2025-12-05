using System;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;   // Geolocation
using Microsoft.Maui.ApplicationModel;   // Geocoding
// using Microsoft.Maui.Controls.Maps;            // Map
using Plugin.LocalNotification;    // după ce adaugi pachetul
using Vilcan_Andrea_Lab7.Models;

namespace Vilcan_Andrea_Lab7;

public partial class ShopPage : ContentPage
{
    public ShopPage()
    {
        InitializeComponent();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }

    /*async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        var address = shop.Adress;

        var locations = await Geocoding.GetLocationsAsync(address);
        var shopLocation = locations?.FirstOrDefault();

        if (shopLocation == null)
        {
            await DisplayAlert("Error", "Nu am găsit adresa.", "OK");
            return;
        }

        var myLocation = await Geolocation.GetLocationAsync();
        var distance = myLocation?.CalculateDistance(shopLocation, DistanceUnits.Kilometers);

        if (distance != null && distance < 5)
        {
            var request = new NotificationRequest
            {
                Title = "Ai de făcut cumpărături în apropiere!",
                Description = address,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                }
            };

            await LocalNotificationCenter.Current.Show(request);
        }

        var options = new MapLaunchOptions { Name = "Magazinul meu preferat" };
        await Map.OpenAsync(shopLocation, options);
    }*/
    async void OnShowMapButtonClicked(object sender, EventArgs e)
{
    try
    {
        var shop = (Shop)BindingContext;
        var address = shop.Adress;

        if (string.IsNullOrWhiteSpace(address))
        {
            await DisplayAlert("Eroare", "Adresa magazinului este goală.", "OK");
            return;
        }

        var locations = await Geocoding.GetLocationsAsync(address);
        var shopLocation = locations?.FirstOrDefault();

        if (shopLocation == null)
        {
            await DisplayAlert("Eroare", "Nu am găsit adresa.", "OK");
            return;
        }

        var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        if (status != PermissionStatus.Granted)
        {
            await DisplayAlert("Permisiune refuzată",
                "Aplicația are nevoie de acces la locație pentru a calcula distanța.",
                "OK");
            return;
        }

        var myLocation = await Geolocation.GetLocationAsync(new GeolocationRequest
        {
            DesiredAccuracy = GeolocationAccuracy.Medium,
            Timeout = TimeSpan.FromSeconds(10)
        });

        double? distance = null;
        if (myLocation != null)
        {
            distance = myLocation.CalculateDistance(shopLocation, DistanceUnits.Kilometers);
        }

        if (distance != null && distance < 5)
        {
            var request = new NotificationRequest
            {
                Title = "Ai de făcut cumpărături în apropiere!",
                Description = address,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                }
            };

            await LocalNotificationCenter.Current.Show(request);
        }

    
        var options = new MapLaunchOptions { Name = "Magazinul meu preferat" };
        await Map.OpenAsync(shopLocation, options);
    }
    catch (FeatureNotSupportedException)
    {
        await DisplayAlert("Eroare", "Funcția de hartă sau locație nu este suportată pe acest emulator.", "OK");
    }
    catch (PermissionException)
    {
        await DisplayAlert("Eroare", "Nu ai acordat permisiunea de locație.", "OK");
    }
    catch (Exception ex)
    {
        await DisplayAlert("Eroare neașteptată", ex.Message, "OK");
    }
}

    // Sarcina laborator – ștergere magazin
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;

        if (shop.ID == 0)
        {
            await Navigation.PopAsync();
            return;
        }

        bool confirm = await DisplayAlert("Confirmare", "Ștergi acest magazin?", "Da", "Nu");
        if (!confirm) return;

        await App.Database.DeleteShopAsync(shop);
        await Navigation.PopAsync();
    }
}