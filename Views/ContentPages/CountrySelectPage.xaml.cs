using Frontend_MyWay.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_MyWay.Views.ContentPages;

public partial class CountrySelectPage : ContentPage
{
    public CountrySelectPage()
    {
        InitializeComponent();
    }

    private async void exploreBtn_Clicked(object sender, EventArgs e)
    {
        var currentCountry = countriesCv.CurrentItem as Country;
        if (currentCountry != null)
        {
            App.selectedCountry = currentCountry;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}trendingvisitsview/get/{App.selectedCountry.CountryId}");
            if (response.IsSuccessStatusCode)
            {
                App.isCountrySelected = true;
                string content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<TrendingVisitsView[]>(content);
                App.trendingVisits = data.ToList();
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Ошибка", "Ошибка при выборе страны", "Закрыть");
            }
        }
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"{App.conString}countries/get");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Country[]>(content);
            countriesCv.ItemsSource = data.ToList();
        }
    }

    private async void countriesCv_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
    {
        var selectedItem = countriesCv.CurrentItem as Country;
        int countryId;
        countryId = selectedItem.CountryId;
        switch (countryId)
        {
            case 2:
                await blurBackgroundImage.FadeTo(0, 200);
                blurBackgroundImage.Source = "russia_background.jpg";
                await blurBackgroundImage.FadeTo(1, 200);
                break;
            case 3:
                await blurBackgroundImage.FadeTo(0, 200);
                blurBackgroundImage.Source = "bulgaria_background.jpg";
                await blurBackgroundImage.FadeTo(1, 200);
                break;
            case 4:
                await blurBackgroundImage.FadeTo(0, 200);
                blurBackgroundImage.Source = "israel_background.jpg";
                await blurBackgroundImage.FadeTo(1, 200);
                break;
        }
    }
}