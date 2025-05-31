using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Views;

public partial class ContactUsView : ContentPage
{
	private ContactUsModel _businessInformation;
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
	public GuestModel CurrentUser { get; set; }

	public ContactUsView(GuestModel currentUser)
	{
		InitializeComponent();

		_controller = new();

		CurrentUser = currentUser;

		_profileOptionsOverlay.InitializeController(CurrentUser);

		InitializeBusinessInfo();

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
}