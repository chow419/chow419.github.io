using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Views;

public partial class EditMemberInfoView : ContentView
{
	public UserModel CurrentUser { get; set; }
	public EditMemberInfoController _controller { get; set; }

	public EditMemberInfoView()
	{
		InitializeComponent();

		_controller = new();

		UpdateStatePicker.ItemsSource = Constants.StateList;
		UpdateCountryPicker.ItemsSource = Constants.CountryList;
	}

	public void OnEditContactInfoOpened(object? sender, UserModel? user)
	{
		CurrentUser = user;
	}

	private void OnEditContactInfoCancelButtonClicked(object sender, EventArgs e)
	{
		this.IsVisible = false;
	}

	private async void OnEditContactInfoSubmitButtonClicked(object sender, EventArgs e)
	{
		var tempUser = CurrentUser;

		tempUser.StreetAddress = UpdateAddressEntry.Text;
		tempUser.AddressLine2 = UpdateAddressLine2Entry.Text;
		tempUser.City = UpdateCityEntry.Text;
		tempUser.State = UpdateStatePicker.SelectedItem.ToString();
		tempUser.ZipCode = UpdateZipCodeEntry.Text;
		tempUser.Country = UpdateCountryPicker.SelectedItem.ToString();
		tempUser.PhoneNumber = UpdatePhoneNumberEntry.Text;

		await _controller.SubmitUserUpdates(tempUser);

		this.IsVisible = false;
	}
}