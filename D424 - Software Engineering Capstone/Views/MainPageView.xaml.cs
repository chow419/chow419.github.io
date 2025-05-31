using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;
using D424___Software_Engineering_Capstone.Views;

namespace D424___Software_Engineering_Capstone
{
    public partial class MainPageView : ContentPage
    {
        public GuestModel CurrentUser { get; set; }
        public MainPageController _controller { get; set; }

        public MainPageView()
        {
            InitializeComponent();

            _controller = new MainPageController(this);

            _signInOverlay.IsVisible = true;
            _signUpOverlay.IsVisible = false;
            _profileOptionsOverlay.IsVisible = false;

            _signInOverlay.ContinueAsGuest += OnContinueAsGuest;
            _signInOverlay.RequestSignUp += OnRequestSignUp;
            _signInOverlay.SuccessfulSignIn += OnSuccessfulSignIn;

            _signUpOverlay.CancelSignUp += OnCancelSignUp;
            _signUpOverlay.SignUpCompleted += OnSignUpCompleted;

            _profileOptionsOverlay.ProfileSignInTapped += OnProfileSignInTapped;
            _profileOptionsOverlay.ProfileSignUpTapped += OnProfileSignUpTapped;
            _profileOptionsOverlay.MemberPortalTapped += OnMemberPageLabelTapped;
            _profileOptionsOverlay.AdminPortalTapped += OnAdminPortalLabelTapped;
            _profileOptionsOverlay.LogOutTapped += OnLogOutLabelTapped;

            BindingContext = this;
        }


        // Private Methods

        private void SetNavigationBarColor(Color color)
        {
            if (Application.Current.MainPage is NavigationPage navPage)
            {
                navPage.BarBackgroundColor = color;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_signInOverlay.IsVisible || _signUpOverlay.IsVisible)
            {
                SetNavigationBarColor(Color.FromRgb(8, 105, 8));
            }
        }

        private void OnSuccessfulSignIn(object? sender, UserModel user)
        {
            CurrentUser = user;

            SetNavigationBarColor(Colors.White);

            ProfileMenuIcon.IsEnabled = true;

            _profileOptionsOverlay.InitializeController(CurrentUser);
        }

        private void OnCancelSignUp(object? sender, EventArgs e)
        {
            _signUpOverlay.IsVisible = false;
            _signInOverlay.IsVisible = true;
        }

        private void OnRequestSignUp(object? sender, EventArgs e)
        {
            _signInOverlay.IsVisible = false;
            _signUpOverlay.IsVisible = true;
        }

        private void OnSignUpCompleted(object? sender, UserModel user)
        {
            CurrentUser = user;

            SetNavigationBarColor(Colors.White);

            ProfileMenuIcon.IsEnabled = true;

            _profileOptionsOverlay.InitializeController(CurrentUser);
        }

        private void OnContinueAsGuest(object? sender, GuestModel? guest)
        {
            if (guest != null)
            {
                CurrentUser = guest;

                SetNavigationBarColor(Colors.White);

                ProfileMenuIcon.IsEnabled = true;

                _profileOptionsOverlay.InitializeController(CurrentUser);
            }
        }

        private void OnProfileSignInTapped(object? sender, EventArgs e)
        {
            _signInOverlay.IsVisible = true;

            SetNavigationBarColor(Color.FromRgb(8, 105, 8));

            ProfileMenuIcon.IsEnabled = false;
        }

        private void OnProfileSignUpTapped(object? sender, EventArgs e)
        {
            _signUpOverlay.IsVisible = true;

            SetNavigationBarColor(Color.FromRgb(8, 105, 8));

            ProfileMenuIcon.IsEnabled = false;
        }

        private void OnTeeTimesButtonClicked(object? sender, EventArgs e)
        {
            Navigation.PushAsync(new TeeTimeView(CurrentUser));
        }

        private void OnCourseNewsButtonClicked(object? sender, EventArgs e)
        {
            Navigation.PushAsync(new CourseNewsView(CurrentUser));
        }

        private void OnContactUsButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ContactUsView(CurrentUser));
        }

        private void OnProfileIconTapped(object sender, TappedEventArgs e)
        {
            _profileOptionsOverlay.IsVisible = !_profileOptionsOverlay.IsVisible;
        }

        private void OnProfileMenuButtonTapped(object sender, EventArgs e)
        {
            _profileOptionsOverlay.IsVisible = !_profileOptionsOverlay.IsVisible;
        }

        private void OnMemberPageLabelTapped(object? sender, EventArgs e)
        {
            if (CurrentUser is UserModel user)
            {
                Navigation.PushAsync(new MemberPortalView(user));
            }
        }

        private void OnAdminPortalLabelTapped(object? sender, EventArgs e)
        {

            Navigation.PushAsync(new AdminPortalView((UserModel)CurrentUser));
        }

        private void OnLogOutLabelTapped(object? sender, EventArgs e)
        {
            CurrentUser = null;

            SetNavigationBarColor(Color.FromRgb(8, 105, 8));

            ProfileMenuIcon.IsEnabled = false;

            _signInOverlay.IsVisible = true;
        }
    }

}
