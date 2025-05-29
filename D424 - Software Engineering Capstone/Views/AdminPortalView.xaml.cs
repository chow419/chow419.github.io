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
	}

	public event EventHandler? OnViewReservations;
	private event EventHandler? OnViewUsers;

	private void OnProfileMenuButtonTapped(object sender, EventArgs e)
	{
		_profileOptionsOverlay.IsVisible = !_profileOptionsOverlay.IsVisible;
	}

	private void OnViewReservationsClicked(object sender, EventArgs e)
	{
		_adminPortalOverlay.IsVisible = true;

		OnViewReservations?.Invoke(sender, e);
	}

	private void OnViewUsersClicked(object sender, EventArgs e)
	{
		_adminPortalOverlay.IsVisible = true;

		OnViewUsers?.Invoke(sender, e);
	}
}