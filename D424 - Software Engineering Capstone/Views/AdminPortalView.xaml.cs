using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Views;

public partial class AdminPortalView : ContentPage
{
	private GuestModel currentUser;


	public AdminPortalController _controller { get; set; }
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

    public AdminPortalView(UserModel admin)
	{
		InitializeComponent();

		_controller = new AdminPortalController();

		CurrentUser = admin;

		this.OnViewReservations += _adminPortalOverlay.OnViewReservationsClicked;
		this.OnViewUsers += _adminPortalOverlay.OnViewUsersClicked;
		this.OnViewGuests += _adminPortalOverlay.OnViewGuestsClicked;
		this.OnViewCourseNews += _adminPortalOverlay.OnAddCourseNewsButtonClicked;

        _profileOptionsOverlay.MemberPortalTapped += OnMemberPageLabelTapped;
        _profileOptionsOverlay.LogOutTapped += OnLogOutLabelTapped;

        BindingContext = this; 
	}

	private event EventHandler? OnViewReservations;
	private event EventHandler? OnViewUsers;
	private event EventHandler? OnViewGuests;
	private event EventHandler? OnViewCourseNews;

	private void OnProfileMenuButtonTapped(object sender, EventArgs e)
	{
		_profileOptionsOverlay.IsVisible = !_profileOptionsOverlay.IsVisible;
	}

	private void OnViewReservationsClicked(object sender, EventArgs e)
	{
		_adminPortalOverlay.IsVisible = true;

		OnViewReservations?.Invoke(this, e);
	}

	private void OnViewUsersClicked(object sender, EventArgs e)
	{
		_adminPortalOverlay.IsVisible = true;

		OnViewUsers?.Invoke(this, e);
	}

	private void OnViewGuestsClicked(object sender, EventArgs e)
	{
		_adminPortalOverlay.IsVisible = true;

		OnViewGuests?.Invoke(this, e);
	}

	private void OnCourseNewsClicked(object sender, EventArgs e)
	{
		_adminPortalOverlay.IsVisible = true;

		OnViewCourseNews?.Invoke(this, e);
	}



    private void SetNavigationBarColor(Color color)
    {
        if (Application.Current.MainPage is NavigationPage navPage)
        {
            navPage.BarBackgroundColor = color;
        }
    }

    private void OnMemberPageLabelTapped(object? sender, EventArgs e)
    {
        // Open MemberPageView
    }

    private async void OnLogOutLabelTapped(object? sender, EventArgs e)
    {
        CurrentUser = null;

        SetNavigationBarColor(Color.FromRgb(8, 105, 8));

        ProfileMenuIcon.IsEnabled = false;

        _signInOverlay.IsVisible = true;

        await Navigation.PopToRootAsync();
    }
}