namespace Frontend_MyWay.Views.ContentPages;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		backgroundImage.IsAnimationPlaying = false;
		await Task.Delay(100);
		backgroundImage.IsAnimationPlaying = true;
	}

    private void continueBtn_Clicked(object sender, EventArgs e)
    {
		Application.Current.MainPage = new RegistrationPage();
    }
}