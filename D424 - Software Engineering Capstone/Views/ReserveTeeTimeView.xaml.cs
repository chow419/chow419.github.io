using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace D424___Software_Engineering_Capstone.Views;

public partial class ReserveTeeTimeView : ContentView
{
	public TeeTimeModel SelectedTeeTime {get; set;}
	public int NumberOfPlayersSelected {get; set;}
	public GuestModel CurrentUser { get; set; }
	public ReserveTeeTimeViewController _controller { get; set; }

	public ReserveTeeTimeView()
	{
		InitializeComponent();

		_controller = new ReserveTeeTimeViewController();

        _controller.DisplayAlert += async (title, message, cancel) =>
        {
            var page = this.GetParentPage();
            if (page != null)
            {
                await page.DisplayAlert(title, message, cancel);
            }
        };
    }

	public event EventHandler? ReservationCancelled;
	public event EventHandler<(TeeTimeModel? TeeTime, int NumberOfPlayers, string? FirstName, string? LastName, string? PhoneNumber)> SubmitReservation;


    public void InitializeTeeTimeScheduler(object? sender, (GuestModel? user, TeeTimeModel? teeTime, int numberOfPlayers) e)
	{
		this.BindingContext = e.teeTime;
		SelectedTeeTime = e.teeTime;
		CurrentUser = e.user;
		NumberOfPlayersSelected = e.numberOfPlayers;

		var list = _controller.PopulatePickerList(e.teeTime);

		NumberOfPlayersPicker.ItemsSource = list;
		NumberOfPlayersPicker.SelectedIndex = e.numberOfPlayers;

		if (CurrentUser is UserModel member)
		{
			ReserveTeeTimeFirstNameEntry.Text = member.FirstName;
			ReserveTeeTimeLastNameEntry.Text = member.LastName;
			ReserveTeeTimePhoneNumberEntry.Text = member.PhoneNumber;
		}
	}

	private void ClearReservationFields()
	{
		ReserveTeeTimeFirstNameEntry.Text = string.Empty;
		ReserveTeeTimeLastNameEntry.Text = string.Empty;
		ReserveTeeTimePhoneNumberEntry.Text = string.Empty;
	}

	private void OnReserveTeeTimeCancelClicked(object sender, EventArgs e)
	{
		this.BindingContext = null;
		SelectedTeeTime = null;

		ReservationCancelled?.Invoke(this, EventArgs.Empty);

		ClearReservationFields();
	}

	private async void OnReserveTeeTimeSubmitClicked(object sender, EventArgs e)
	{
		if (await _controller.ValidateReservationFirstName(ReserveTeeTimeFirstNameEntry.Text)
			&& await _controller.ValidateReservationLastName(ReserveTeeTimeLastNameEntry.Text)
			&& await _controller.ValidateReservationPhoneNumber(ReserveTeeTimePhoneNumberEntry.Text))
		{
			SubmitReservation?.Invoke(this, (SelectedTeeTime, NumberOfPlayersPicker.SelectedIndex + 1, ReserveTeeTimeFirstNameEntry.Text, ReserveTeeTimeLastNameEntry.Text, ReserveTeeTimePhoneNumberEntry.Text));

			this.BindingContext = null;

			SelectedTeeTime = null;

			ClearReservationFields();
		}		
	}
}