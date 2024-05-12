using Frontend_MyWay.ApplicationData;
using Frontend_MyWay.Views.ContentPages;

namespace Frontend_MyWay;

public partial class App : Application
{
    public static string conString = "http://192.168.0.10:45455/api/";
	public static User enteredUser;
	public static Country selectedCountry;
	public static List<TrendingVisitsView> trendingVisits;
	public static VisitsView selectedVisit;
	public static bool isCountrySelected = false;
	public static List<FavoriteVisitsView> favoriteVisits;
	public static List<TrendingVisitsView> allTrendingVisits = null;
    public App()
	{
		InitializeComponent();

		MainPage = new WelcomePage();
	}
}
