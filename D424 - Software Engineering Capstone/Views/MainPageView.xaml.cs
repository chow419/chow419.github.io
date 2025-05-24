using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone
{
    public partial class MainPageView : ContentPage
    {
        public GuestModel CurrentUser { get; set; }
        public GuestMainPageController _mainPageController { get; set; }
        public Entry UsernameEntry => this.SignUpUsernameEntry;
        public Entry PasswordEntry => this.SignUpPasswordEntry;
        public Entry FirstNameEntry => this.SignUpFirstNameEntry;
        public Entry LastNameEntry => this.SignUpLastNameEntry;
        public Entry PhoneNumberEntry => this.SignUpPhoneNumberEntry;
        public Entry EmailEntry => this.SignUpEmailEntry;
        public Entry StreetAddressEntry => this.SignUpAddressEntry;
        public Entry Address2Entry => this.SignUpAddressLine2Entry;
        public Entry CityEntry => this.SignUpCityEntry;
        public Picker StatePicker => this.SignUpStatePicker;
        public Entry ZipCodeEntry => this.SignUpZipCodeEntry;
        public Picker CountryPicker => this.SignUpCountryPicker;
        public DatePicker DateOfBirthPicker => this.SignUpDateOfBirthPicker;
        public Grid MainOverlayBackground => this.MainOverlay;
        public Grid SignInOverlayWindow => this.SignInOverlay;
        public Border SignInOverlayBorder => this.SignInBorder;
        public Grid SignUpOverlayWindow => this.SignUpUNPWOverlay;
        public Border SignUpOverlayBorder => this.SignUpUNPWBorder;

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
                SignInBorder.IsVisible = false;
                MainOverlay.IsVisible = false;
                ClearSignInFields();

                InitializeNewMainPageController();
            }
            else
            {
                SignInPasswordEntry.Text = string.Empty;
            }
        }

        public void OnSignUpLabelTapped(object sender, EventArgs e)
        {
            SignInOverlay.IsVisible = false;
            SignInBorder.IsVisible = false;
            SignUpUNPWOverlay.IsVisible = true;
            SignUpUNPWBorder.IsVisible = true;
            ClearSignInFields();
        }

        public async void OnSignUpSubmitButtonClicked(object sender, EventArgs e)
        {
            if (await _mainPageController.ValidateLocationSignUp())
            {
                await _mainPageController.SignUpNewUser();

                InitializeNewMainPageController();

                SignUpLocationOverlay.IsVisible = false;
                SignUpLocationBorder.IsVisible = false;
                MainOverlay.IsVisible = false;
            }
        }

        public void OnSignUpCancelButtonClicked(object sender, EventArgs e)
        {
            SignUpUNPWOverlay.IsVisible = false;
            SignUpUNPWBorder.IsVisible = false;
            SignInOverlay.IsVisible = true;
            SignInBorder.IsVisible = true;
            ClearSignUpFields();
        }

        public async void OnSignUpUNPWNextButtonClicked(object sender, EventArgs e)
        {
            if (await _mainPageController.ValidateUNPWSignUp())
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
            if (await _mainPageController.ValidatePISignUp())
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

        public void OnContinueAsGuestLabelTapped(object sender, TappedEventArgs e)
        {
            _mainPageController.GetGuest();

            SignInOverlay.IsVisible = false;
            SignInBorder.IsVisible = false;
            MainOverlay.IsVisible = false;
        }

        public void OnTeeTimesButtonClicked(object sender, EventArgs e)
        {
            // Open TeeTimeView
        }

        public void OnCourseNewsButtonClicked(object sender, EventArgs e)
        {
            // Open CourseNewsView
        }

        public void OnContactUsButtonClicked(object sender, EventArgs e)
        {
            // Open ContactUsView
        }

        public void OnProfileIconTapped(object sender, TappedEventArgs e)
        {
            _mainPageController.PopulateProfileMenuOverlay();

            ProfileMenuOverlay.IsVisible = !ProfileMenuOverlay.IsVisible;
            ProfileMenuBorder.IsVisible = !ProfileMenuBorder.IsVisible;
        }
    }

}
