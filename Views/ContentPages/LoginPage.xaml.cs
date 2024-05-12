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
                            await DisplayAlert("Ошибка!", "Неправильные данные пользователя!", "Закрыть");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Ошибка!", "Минимальная длина пароля - 4 символа!", "Закрыть");
                    }
                }
                else
                {
                    await DisplayAlert("Ошибка!", "E-mail не соответствует формату!", "Закрыть");
                }
            }
            else
            {
                await DisplayAlert("Ошибка!", "Введите пароль!", "Закрыть");
            }
        }
        else
        {
            await DisplayAlert("Ошибка!", "Введите E-mail!", "Закрыть");
        }
    }

    private void regBtn_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new RegistrationPage();
    }
}