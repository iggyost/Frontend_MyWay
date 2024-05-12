using Frontend_MyWay.ApplicationData;
using Newtonsoft.Json;
using System.Net;

namespace Frontend_MyWay.Views.ContentPages;

public partial class RoutePage : ContentPage
{
	public RoutePage()
	{
		InitializeComponent();
	}

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
		isBusyIndicator.IsRunning = true;
		if (App.selectedVisit != null)
		{
			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.GetAsync($"{App.conString}visitroute/get/{App.selectedVisit.VisitId}");
			if (response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<VisitsRoute[]>(content);
				var currentVisitRoute = data.FirstOrDefault();
				string path = currentVisitRoute.RoutePath;
				routeWebView.Source = path;
			}
			else if (response.StatusCode == HttpStatusCode.NotFound)
			{
				await DisplayAlert("Маршрут не найден", "Маршрут для данной активности не найден или не указан!", "ОК");
				await Navigation.PopModalAsync();
			}
		}
		isBusyIndicator.IsRunning = false;
	}

    private async void backBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}