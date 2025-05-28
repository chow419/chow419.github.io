using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace D424___Software_Engineering_Capstone.Views;

public partial class TeeTimeView : ContentPage
{
    public TeeTimeController _controller { get; set; }
    public GuestModel CurrentUser { get; set; }
    public ObservableCollection<TeeTimeModel> AvailableTeeTimes { get; set; }

    public TeeTimeView(GuestModel currentUser)
    {
        InitializeComponent();

        _controller = new TeeTimeController();

        CurrentUser = currentUser;

        InitializePickers();

        _profileOptionsOverlay.InitializeController(CurrentUser);

        _reserveTeeTimeView.ReservationCancelled += OnReservationCancelled;
        _reserveTeeTimeView.SubmitReservation += OnSubmitReservation;

        this.OnTeeTimeSelected += _reserveTeeTimeView.InitializeTeeTimeScheduler;

        BindingContext = this;
    }

    public event EventHandler<(GuestModel? User, TeeTimeModel? TeeTime, int index)> OnTeeTimeSelected;

    private async void SetAvailableTeeTimes()
    {
        AvailableTeeTimes = new ObservableCollection<TeeTimeModel>(await _controller.GetTeeTimesForDate(ReservationDatePicker.Date));
        AvailableTeeTimes = new ObservableCollection<TeeTimeModel>(AvailableTeeTimes.Where(att => att.AvailablePlayerSlots >= NumberOfPlayersPicker.SelectedIndex + 1).ToList());

        OnPropertyChanged(nameof(AvailableTeeTimes));

        BindingContext = this;
    }

    private void InitializePickers()
    {
        NumberOfPlayersPicker.ItemsSource = PopulateNumberOfPlayersPicker();
        NumberOfPlayersPicker.SelectedIndex = 0;

        ReservationDatePicker.Date = DateTime.Now;
        ReservationDatePicker.MinimumDate = DateTime.Today;
    }

    private ObservableCollection<int> PopulateNumberOfPlayersPicker()
    {
        return new ObservableCollection<int> { 1, 2, 3, 4 };
    }

    private void OnProfileMenuButtonClicked(object sender, EventArgs e)
    {
        _profileOptionsOverlay.IsVisible = !_profileOptionsOverlay.IsVisible;
    }

    private void OnDateChanged(object sender, EventArgs e)
    {
        SetAvailableTeeTimes();
    }

    private void OnNumberOfPlayersChanged(object sender, EventArgs e)
    {
        SetAvailableTeeTimes();
    }

    private void OnTeeTimeObjectTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is TeeTimeModel tappedTeeTime)
        {
            OnTeeTimeSelected?.Invoke(this, (CurrentUser, tappedTeeTime, NumberOfPlayersPicker.SelectedIndex));

            _reserveTeeTimeView.IsVisible = true;
        }
    }

    private void OnReservationCancelled(object? sender, EventArgs e)
    {
        _reserveTeeTimeView.IsVisible = false;
    }

    private async void OnSubmitReservation(object? sender, (TeeTimeModel? TeeTime, int NumberOfPlayers, string? FirstName, string? LastName, string? PhoneNumber) e)
    {
        if (CurrentUser is UserModel member)
        {
            var memberId = await _controller.GetMemberId(member);

            var reservation = new ReservationModel
            {
                UserId = memberId,
                Date = e.TeeTime.Date,
                Time = e.TeeTime.Time,
                NumberOfPlayers = e.NumberOfPlayers
            };

            await _controller.ScheduleTeeTime(memberId, false, reservation);
        }
        else
        {
            await _controller.AddGuest(e.FirstName, e.LastName, e.PhoneNumber);

            var guestId = await _controller.GetGuestId(e.FirstName, e.LastName, e.PhoneNumber);

            var reservation = new ReservationModel
            {
                UserId = guestId,
                Date = e.TeeTime.Date,
                Time = e.TeeTime.Time,
                NumberOfPlayers = e.NumberOfPlayers
            };

            await _controller.ScheduleTeeTime(guestId, true, reservation);
        }

        _reserveTeeTimeView.IsVisible = false;

        SetAvailableTeeTimes();
    }
}
