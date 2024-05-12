using Frontend_MyWay.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_MyWay.Views.ContentPages;

public partial class FavoritesPage : ContentPage
{
    public FavoritesPage()
    {
        InitializeComponent();
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
    public async void LoadAllTrending()
    {
        isBusyIndicator.IsRunning = true;
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"{App.conString}trendingvisitsview/get/all");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<TrendingVisitsView[]>(content);
            var visitsData = data.ToList();
            App.allTrendingVisits = visitsData;
        }
        else
        {

        }
        isBusyIndicator.IsRunning = false;
    }
    public async void LoadFavoriteVisits()
    {
        isBusyIndicator.IsRunning = true;
        HttpClient favoriteClient = new HttpClient();
        HttpResponseMessage favoriteResponse = await favoriteClient.GetAsync($"{App.conString}favoritevisitsview/get/{App.enteredUser.UserId}");
        List<TrendingVisitsView> currentFavoriteVisitsView = null;
        if (favoriteResponse.IsSuccessStatusCode)
        {
            string favoriteContent = await favoriteResponse.Content.ReadAsStringAsync();
            var favoriteData = JsonConvert.DeserializeObject<FavoriteVisitsView[]>(favoriteContent);
            var favoriteVisitsView = favoriteData.ToList();
            //foreach (var item in favoriteVisitsView)
            //{
            //    var currentVisit = App.allTrendingVisits.Where(x => x.VisitId == item.VisitId).FirstOrDefault();
            //    if (currentVisit != null)
            //    {

            //    }
            //}
            var favoriteVisits = App.allTrendingVisits.IntersectBy(favoriteVisitsView.Select(x => x.VisitId), x => x.VisitId).ToList();
            //favoriteVisitsCv.ItemsSource = currentFavoriteVisitsView.ToList();            
            favoriteVisitsCv.ItemsSource = favoriteVisits.ToList();
        }
        isBusyIndicator.IsRunning = false;
    }
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        StartAnimation();
        if (App.allTrendingVisits == null)
        {
            LoadAllTrending();

            await Task.Delay(1000);

            LoadFavoriteVisits();
        }
        else
        {
            LoadFavoriteVisits();
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

    private void refreshFavoriteCv_Refreshing(object sender, EventArgs e)
    {
        refreshFavoriteCv.IsRefreshing = true;
        LoadFavoriteVisits();
        refreshFavoriteCv.IsRefreshing = false;
    }
}