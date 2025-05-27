using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Views;

public partial class TeeTimeView : ContentPage
{
	public GuestModel CurrentUser { get; set; }

    public TeeTimeView(GuestModel currentUser)
	{
		InitializeComponent();

		CurrentUser = currentUser;

		_profileOptionsOverlay.InitializeController(CurrentUser);

        BindingContext = this;
    }

	private void OnProfileMenuButtonClicked(object sender, EventArgs e)
    {
        _profileOptionsOverlay.IsVisible = !_profileOptionsOverlay.IsVisible;
    }
}