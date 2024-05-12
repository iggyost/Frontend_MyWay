using Frontend_MyWay.ApplicationData;
using Newtonsoft.Json;

namespace Frontend_MyWay.Views.ContentPages;

public partial class VisitPage : ContentPage
{
    public VisitPage()
    {
        InitializeComponent();
        nameLabel.Text = App.selectedVisit.Name;
        descriptionLabel.Text = App.selectedVisit.Description;
        levelLabel.Text = App.selectedVisit.LevelName;
        distanceLabel.Text = App.selectedVisit.DistanceKm.ToString() + " κμ";
        timeLabel.Text = App.selectedVisit.TimeHours.ToString();
        coverImage.Source = App.selectedVisit.CoverImage;
    }

    private async void goBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new RoutePage());
    }

    private async void backBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void favoriteButton_Clicked(object sender, EventArgs e)
    {
        if (isFavorite == true)
        {
            favoriteButton.IsEnabled = false;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}favoritevisitsview/remove/{App.enteredUser.UserId}/{App.selectedVisit.VisitId}");
            if (response.IsSuccessStatusCode)
            {
                favoriteButton.Source = "favorite_icon.png";
                isFavorite = false;
                favoriteButton.IsEnabled = true;
            }
            else
            {
                favoriteButton.IsEnabled = true;
            }
        }
        else
        {
            favoriteButton.IsEnabled = false;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{App.conString}favoritevisitsview/save/{App.enteredUser.UserId}/{App.selectedVisit.VisitId}");
            if (response.IsSuccessStatusCode)
            {
                favoriteButton.Source = "favorite_icon_filled.png";
                isFavorite = true;
                favoriteButton.IsEnabled = true;
            }
            else
            {
                favoriteButton.IsEnabled = true;
            }
        }
    }
    public static List<FavoriteVisitsView> currentFavoriteVisits;
    public static bool isFavorite = false;
    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"{App.conString}favoritevisitsview/get/{App.enteredUser.UserId}");
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<FavoriteVisitsView[]>(content);
            if (data != null)
            {
                currentFavoriteVisits = data.ToList();
                if (data.Where(x => x.VisitId == App.selectedVisit.VisitId).FirstOrDefault() != null)
                {
                    favoriteButton.Source = "favorite_icon_filled.png";
                    isFavorite = true;
                }
                else
                {
                    favoriteButton.Source = "favorite_icon.png";
                    isFavorite = false;
                }
            }
            else
            {
                favoriteButton.Source = "favorite_icon.png";
                isFavorite = false;
            }
        }
    }
}