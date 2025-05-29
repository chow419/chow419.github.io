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
            var reservation = new ReservationModel
            {
                Player = member,
                Date = e.TeeTime.Date,
                Time = e.TeeTime.Time,
                NumberOfPlayers = e.NumberOfPlayers
            };

            await _controller.ScheduleTeeTime(member.Id, false, reservation);
        }
        else
        {
            await _controller.AddGuest(e.FirstName, e.LastName, e.PhoneNumber);

            var guest = await _controller.GetGuest(e.FirstName, e.LastName, e.PhoneNumber);

            var reservation = new ReservationModel
            {
                Player = new GuestModel()
                {
                    FirstName = guest.FirstName,
                    LastName = guest.LastName,
                    PhoneNumber = guest.PhoneNumber
                },
                Date = e.TeeTime.Date,
                Time = e.TeeTime.Time,
                NumberOfPlayers = e.NumberOfPlayers
            };

            await _controller.ScheduleTeeTime(guest.Id, true, reservation);
        }

        _reserveTeeTimeView.IsVisible = false;

        SetAvailableTeeTimes();
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
