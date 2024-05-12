using Frontend_MyWay.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_MyWay.Views.ContentPages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void tapped_countrySelector_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new CountrySelectPage());
    }
    public static int i = 0;
    public static string[] backgroundArray = { "background_sea.jpg", "background_rome.jpg", "background_forest.jpg", "background_mountains.jpg" };
    private async void StartAnimation()
    {
        for (; ; )
        {
            backgroundImage_first.Source = backgroundArray[i];
            await backgroundImage_first.FadeTo(1, 300);
            await Task.Delay(15000);
            await backgroundImage_first.FadeTo(0, 300);
            i++;
            if (i == 4)
            {
                i = 0;
            }
        }
    }
    public async void LoadCategories()
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"{App.conString}categories/get");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Category[]>(content);
            categoriesCv.ItemsSource = data.ToList();
        }
    }
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        StartAnimation();
        LoadCategories();
        if (App.trendingVisits != null)
        {

        }
        else
        {
            await DisplayAlert("Для продолжения выберите страну", "Пожалуйста, выберите одну из стран в списке для продолжения", "ОК");
            await Navigation.PushModalAsync(new CountrySelectPage());
        }
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (App.isCountrySelected == true)
        {
            trendingVisitsCv.ItemsSource = App.trendingVisits.ToList();
            countryNameLabel.Text = App.selectedCountry.Name;
            App.isCountrySelected = false;
        }
        else
        {

        }
    }
    private void categoryRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        RadioButton radioButton = sender as RadioButton;
        int categoryRadioButtonId = int.Parse(radioButton.AutomationId.ToString());
        if (categoryRadioButtonId != 0)
        {
            if (radioButton.IsChecked == false)
            {
                trendingVisitsCv.ItemsSource = App.trendingVisits.ToList();
            }
            else
            {
                trendingVisitsCv.ItemsSource = App.trendingVisits.Where(x => x.CategoryId == categoryRadioButtonId).ToList();
            }
        }

    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        RadioButton radioButton = sender as RadioButton;
        if (radioButton.IsChecked == true)
        {
            radioButton.IsChecked = false;
            trendingVisitsCv.ItemsSource = App.trendingVisits.ToList();
        }
    }

    private async void visitGesture_Tapped(object sender, TappedEventArgs e)
    {
        Border border = sender as Border;
        int visitId = int.Parse(border.AutomationId.ToString());
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"{App.conString}visitsview/get/{visitId}");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<VisitsView[]>(content);
            App.selectedVisit = data.FirstOrDefault();
            await Navigation.PushModalAsync(new VisitPage());
        }
        else
        {
            await DisplayAlert("Ошибка", "Ошибка при загрузке активности", "Закрыть");
        }
    }

    private void costLabel_Loaded(object sender, EventArgs e)
    {
        Label label = sender as Label;
        decimal costVisit = decimal.Parse(label.AutomationId.ToString());
        if (costVisit == 0)
        {
            label.Text = "Free";
        }
    }

    private async void searchVisit_SearchButtonPressed(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(searchVisit.Text) != true)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}trendingvisitsview/search/{App.selectedCountry.CountryId}/{searchVisit.Text}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<TrendingVisitsView[]>(content);
                trendingVisitsCv.ItemsSource = data.ToList();
            }
            else
            {
                await DisplayAlert("По вашему запросу ничего не найдено!", "Попробуйте изменить ваш запрос!", "ОК");
            }
        }
        else
        {
            trendingVisitsCv.ItemsSource = App.trendingVisits.ToList();
        }
    }

    private void clearSearchButton_Clicked(object sender, EventArgs e)
    {
        trendingVisitsCv.ItemsSource = App.trendingVisits.ToList();
    }
}