using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Views;

public partial class SignUpOverlayView : ContentView
{
    public SignUpOverlayController _controller { get; set; }

    public SignUpOverlayView()
	{
		InitializeComponent();

        _controller = new SignUpOverlayController();

        PopulateStateAndCountryPickers();

        _controller.DisplayAlert += async (title, message, cancel) =>
        {
            var page = this.GetParentPage();
            if (page != null)
            {
                await page.DisplayAlert(title, message, cancel);
            }
        };
    }

    public event EventHandler? CancelSignUp;

    public event EventHandler<UserModel>? SignUpCompleted;

    private void PopulateStateAndCountryPickers()
    {
        SignUpStatePicker.ItemsSource = _controller.PopulateStateList();
        SignUpCountryPicker.ItemsSource = _controller.PopulateCountryList();
    }

    private void ClearSignUpFields()
    {
        SignUpUsernameEntry.Text = string.Empty;
        SignUpPasswordEntry.Text = string.Empty;
        SignUpFirstNameEntry.Text = string.Empty;
        SignUpLastNameEntry.Text = string.Empty;
        SignUpPhoneNumberEntry.Text = string.Empty;
        SignUpEmailEntry.Text = string.Empty;
        SignUpAddressEntry.Text = string.Empty;
        SignUpAddressLine2Entry.Text = string.Empty;
        SignUpCityEntry.Text = string.Empty;
        SignUpStatePicker.SelectedItem = null;
        SignUpZipCodeEntry.Text = string.Empty;
        SignUpCountryPicker.SelectedItem = null;
    }

    public void OnSignUpCancelButtonClicked(object sender, EventArgs e)
    {
        ClearSignUpFields();

        CancelSignUp?.Invoke(this, EventArgs.Empty);
    }

    public async void OnSignUpUNPWNextButtonClicked(object sender, EventArgs e)
    {
        if (await _controller.ValidateUNPWSignUp(SignUpUsernameEntry.Text, SignUpPasswordEntry.Text))
        {
            SignUpUNPWOverlay.IsVisible = false;
            SignUpUNPWBorder.IsVisible = false;
            SignUpPIOverlay.IsVisible = true;
            SignUpPIBorder.IsVisible = true;
        }
    }

    public void OnSignUpPIBackButtonClicked(object sender, EventArgs e)
    {
        SignUpUNPWOverlay.IsVisible = true;
        SignUpUNPWBorder.IsVisible = true;
        SignUpPIOverlay.IsVisible = false;
        SignUpPIBorder.IsVisible = false;
    }

    public async void OnSignUpPINextButtonClicked(object sender, EventArgs e)
    {
        if (await _controller.ValidatePISignUp(SignUpFirstNameEntry.Text,
            SignUpLastNameEntry.Text,
            SignUpPhoneNumberEntry.Text,
            SignUpEmailEntry.Text))
        {
            SignUpPIOverlay.IsVisible = false;
            SignUpPIBorder.IsVisible = false;
            SignUpLocationOverlay.IsVisible = true;
            SignUpLocationBorder.IsVisible = true;
        }
    }

    public void OnSignUpLocationBackButtonClicked(object sender, EventArgs e)
    {
        SignUpPIOverlay.IsVisible = true;
        SignUpPIBorder.IsVisible = true;
        SignUpLocationOverlay.IsVisible = false;
        SignUpLocationBorder.IsVisible = false;
    }

    public async void OnSignUpSubmitButtonClicked(object sender, EventArgs e)
    {
        if (await _controller.ValidateLocationSignUp(SignUpAddressEntry.Text, SignUpCityEntry.Text, SignUpZipCodeEntry.Text))
        {
            var newUser = await _controller.SignUpNewUser(SignUpUsernameEntry.Text, SignUpPasswordEntry.Text, SignUpFirstNameEntry.Text, SignUpLastNameEntry.Text,
                SignUpPhoneNumberEntry.Text, SignUpEmailEntry.Text, SignUpAddressEntry.Text, SignUpAddressLine2Entry.Text, SignUpCityEntry.Text,
                SignUpStatePicker.SelectedItem.ToString(), SignUpZipCodeEntry.Text, SignUpCountryPicker.SelectedItem.ToString(), SignUpDateOfBirthPicker.Date);

            this.IsVisible = false;

            SignUpCompleted?.Invoke(this, newUser);
        }
    }
}