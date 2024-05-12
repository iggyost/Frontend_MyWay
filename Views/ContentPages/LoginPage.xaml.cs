using Frontend_MyWay.ApplicationData;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Frontend_MyWay.Views.ContentPages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        backgroundImage.IsAnimationPlaying = false;
        await Task.Delay(100);
        backgroundImage.IsAnimationPlaying = true;
    }
    private async void authBtn_Clicked(object sender, EventArgs e)
    {
        if (emailEntry.Text != null)
        {
            if (passwordEntry.Text != null)
            {
                if (emailRegex.IsMatch(emailEntry.Text))
                {
                    if (passwordEntry.Text.Length > 3)
                    {
                        HttpClient client = new HttpClient();
                        var response = await client.GetAsync($"{App.conString}users/enter/{emailEntry.Text}/{passwordEntry.Text}");
                        if (response.IsSuccessStatusCode)
                        {
                            string content = await response.Content.ReadAsStringAsync();
                            App.enteredUser = JsonConvert.DeserializeObject<User>(content);
                            Application.Current.MainPage = new AppShell();
                        }
                        else
                        {
                            await DisplayAlert("������!", "������������ ������ ������������!", "�������");
                        }
                    }
                    else
                    {
                        await DisplayAlert("������!", "����������� ����� ������ - 4 �������!", "�������");
                    }
                }
                else
                {
                    await DisplayAlert("������!", "E-mail �� ������������� �������!", "�������");
                }
            }
            else
            {
                await DisplayAlert("������!", "������� ������!", "�������");
            }
        }
        else
        {
            await DisplayAlert("������!", "������� E-mail!", "�������");
        }
    }

    private void regBtn_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new RegistrationPage();
    }
}