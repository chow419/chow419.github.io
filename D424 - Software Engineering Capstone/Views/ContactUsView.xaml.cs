using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Views;

public partial class ContactUsView : ContentPage
{
	private ContactUsModel _businessInformation;
	private GuestModel currentUser;

	public ContactUsController _controller { get; set; }
	public ContactUsModel BusinessInformation
	{
		get => _businessInformation;
		set
		{
			if (_businessInformation != value)
			{
				_businessInformation = value;
				OnPropertyChanged(nameof(BusinessInformation));
			}
		}
	}
    public GuestModel CurrentUser
    {
        get => currentUser;
        set
        {
            if (currentUser != value)
            {
                currentUser = value;

                if (GlobalVariables.CurrentUser != value)
                {
                    GlobalVariables.CurrentUser = value;
                }
            }

            OnPropertyChanged(nameof(CurrentUser));
        }
    }

    public ContactUsView(GuestModel currentUser)
	{
		InitializeComponent();

		_controller = new();

		CurrentUser = currentUser;

		_profileOptionsOverlay.InitializeController(CurrentUser);

		InitializeBusinessInfo();

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

	private async void InitializeBusinessInfo()
	{
		BusinessInformation = await _controller.GetContactInformation();
	}

	private void OnProfileMenuButtonClicked(object sender, EventArgs e)
	{
		_profileOptionsOverlay.IsVisible = !_profileOptionsOverlay.IsVisible;
	}





    private void SetNavigationBarColor(Color color)
    {
        if (Application.Current.MainPage is NavigationPage navPage)
        {
            navPage.BarBackgroundColor = color;
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

    private void OnMemberPageLabelTapped(object? sender, EventArgs e)
    {
        // Open MemberPageView
    }

    private void OnAdminPortalLabelTapped(object? sender, EventArgs e)
    {

        // Open AdminPortalView
    }

    private void OnLogOutLabelTapped(object? sender, EventArgs e)
    {
        CurrentUser = null;

        SetNavigationBarColor(Color.FromRgb(8, 105, 8));

        ProfileMenuIcon.IsEnabled = false;

        _signInOverlay.IsVisible = true;
    }
}