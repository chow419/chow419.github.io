using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone
{
    public partial class MainPageView : ContentPage
    {
        public GuestModel CurrentUser { get; set; }
        public GuestMainPageController _mainPageController { get; set; }
        public Grid SignInOverlayGrid => this.SignInOverlay;
        public Grid SignUpOverlayGrid => this.SignUpUNPWOverlay;
        public Entry UsernameEntry => this.NewUserUsernameEntry;
        public Entry PasswordEntry => this.NewUserPasswordEntry;
        public Entry FirstNameEntry => this.NewUserFirstNameEntry;
        public Entry LastNameEntry => this.NewUserLastNameEntry;
        public Entry PhoneNumberEntry => this.NewUserPhoneNumberEntry;
        public Entry EmailEntry => this.NewUserEmailEntry;
        public Entry StreetAddressEntry => this.NewUserStreetAddressEntry;
        public Entry CityEntry => this.NewUserCityEntry;
        public Picker StatePicker => this.NewUserStatePicker;
        public Entry ZipCodeEntry => this.NewUserZipCodeEntry;
        public Picker CountryPicker => this.NewUserCountryPicker;
        public DatePicker DateOfBirthPicker => this.NewUserDateOfBirthPicker;

        public MainPageView()
        {
            InitializeComponent();

            _mainPageController = new GuestMainPageController(this);

            BindingContext = this;
        }

        private void ClearSignInFields()
        {
            SignInUsernameEntry.Text = string.Empty;
            SignInPasswordEntry.Text = string.Empty;
        }

        private void ClearSignUpFields()
        {
            NewUserUsernameEntry.Text = string.Empty;
            NewUserPasswordEntry.Text = string.Empty;
            NewUserFirstNameEntry.Text = string.Empty;
            NewUserLastNameEntry.Text = string.Empty;
            NewUserPhoneNumberEntry.Text = string.Empty;
            NewUserEmailEntry.Text = string.Empty;
            NewUserStreetAddressEntry.Text = string.Empty;
            NewUserCityEntry.Text = string.Empty;
            NewUserStatePicker.SelectedItem = null;
            NewUserZipCodeEntry.Text = string.Empty;
            NewUserCountryPicker.SelectedItem = null;
        }

        public void InitializeNewMainPageController()
        {
            if (CurrentUser is UserModel user)
            {
                if (user.IsAdmin)
                {
                    _mainPageController = new AdminMainPageController(this);
                }
                else
                {
                    _mainPageController = new UserMainPageController(this);
                }
            }
        }

        public async void OnSignInButtonClicked(object sender, EventArgs e)
        {
            if (await _mainPageController.VerifyLogin(
                SignInUsernameEntry.Text,
                SignInPasswordEntry.Text
            ))
            {
                SignInOverlay.IsVisible = false;
                SignUpUNPWOverlay.IsVisible = false;
                ClearSignInFields();

                InitializeNewMainPageController();
            }
        }

        public void OnSignUpLabelTapped(object sender, EventArgs e)
        {
            SignInOverlay.IsVisible = false;
            SignUpUNPWOverlay.IsVisible = true;
            ClearSignInFields();
        }

        public async void OnSignUpSubmitButtonClicked(object sender, EventArgs e)
        {
            await _mainPageController.SignUpNewUser();

            InitializeNewMainPageController();

            SignUpUNPWOverlay.IsVisible = false;
        }

        public void OnSignUpCancelLabelTapped(object sender, TappedEventArgs e)
        {
            SignUpUNPWOverlay.IsVisible = false;
            SignInOverlay.IsVisible = true;
            ClearSignUpFields();
        }

        public void OnContinueAsGuestLabelTapped(object sender, TappedEventArgs e)
        {
            _mainPageController.GetGuest();

            SignInOverlay.IsVisible = false;
        }

        public void OnScheduleATeeTimeButtonClicked(object sender, EventArgs e)
        {
            // Open TeeTimeView
        }

        public void OnCourseNewsButtonClicked(object sender, EventArgs e)
        {
            // Open CourseNewsView
        }

        public void ContactUsButtonClicked(object sender, EventArgs e)
        {
            // Open ContactUsView
        }

        public void OnProfileIconTapped(object sender, TappedEventArgs e)
        {
            // Open ProfileMenuOverlay
        }
    }

}
