using Frontend_MyWay.ApplicationData;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Frontend_MyWay.Views.ContentPages;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage()
    {
        InitializeComponent();
    }

    Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

    public static bool isPasswordVisible = false;
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        backgroundImage.IsAnimationPlaying = false;
        await Task.Delay(100);
        backgroundImage.IsAnimationPlaying = true;
    }
    private void hidePasswordBtn_Clicked(object sender, EventArgs e)
    {
        if (isPasswordVisible == false)
        {
            passwordEntry.IsPassword = false;
            isPasswordVisible = true;
            hidePasswordBtn.Source = "eye_open_icon.png";
        }
        else
        {
            passwordEntry.IsPassword = true;
            isPasswordVisible = false;
            hidePasswordBtn.Source = "eye_closed_icon.png";
        }
    }

    private async void regBtn_Clicked(object sender, EventArgs e)
    {
        if (emailEntry.Text != null)
        {
            if (nameEntry.Text != null)
            {
                if (passwordEntry.Text != null)
                {
                    if (emailRegex.IsMatch(emailEntry.Text))
                    {
                        if (passwordEntry.Text.Length > 3)
                        {
                            if (politicsCheckBox.IsChecked == true)
                            {
                                HttpClient client = new HttpClient();
                                var response = await client.GetAsync($"{App.conString}users/reg/{nameEntry.Text}/{emailEntry.Text}/{passwordEntry.Text}");
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
                                await DisplayAlert("Внимание!", "Необходимо принять пользовательское соглашение, чтобы продолжить регистрацию!", "Закрыть");
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
                    await DisplayAlert("Ошибка!", "Поле для пароля не может быть пустым", "Закрыть");
                }
            }
            else
            {
                await DisplayAlert("Ошибка!", "Поле для имени не может быть пустым", "Закрыть");
            }
        }
        else
        {
            await DisplayAlert("Ошибка!", "Поле для E-mail не может быть пустым", "Закрыть");
        }
    }

    private void loginBtn_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new LoginPage();
    }
}