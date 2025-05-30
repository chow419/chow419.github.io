using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Views;

public partial class AdminPortalView : ContentPage
{
	public AdminPortalController _controller { get; set; }
	public UserModel CurrentUser { get; set; }

	public AdminPortalView(UserModel admin)
	{
		InitializeComponent();

		_controller = new AdminPortalController();

		CurrentUser = admin;

		this.OnViewReservations += _adminPortalOverlay.OnViewReservationsClicked;
		this.OnViewUsers += _adminPortalOverlay.OnViewUsersClicked;
		this.OnViewGuests += _adminPortalOverlay.OnViewGuestsClicked;
		this.OnViewCourseNews += _adminPortalOverlay.OnAddCourseNewsButtonClicked;
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
}