using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;
using System.Collections.ObjectModel;

namespace D424___Software_Engineering_Capstone.Views;

public partial class AdminPortalOverlayView : ContentView
{
	private bool _isFilterByDateEnabled;
	private bool _isFilterByPlayerEnabled;
	private ObservableCollection<ReservationModel> _filteredReservations;
	private ObservableCollection<UserModel> _filteredUsers;

	public AdminPortalOverlayController _controller { get; set; }
	public bool IsFilterByDateEnabled
	{
		get => _isFilterByDateEnabled;
		set
		{
			if (_isFilterByDateEnabled != value)
			{
				_isFilterByDateEnabled = value;
				OnPropertyChanged(nameof(IsFilterByDateEnabled));
			}
		}
	}
	public bool IsFilterByPlayerEnabled
	{
		get => _isFilterByPlayerEnabled;
		set
		{
			if (_isFilterByPlayerEnabled != value)
			{
				_isFilterByPlayerEnabled = value;
				OnPropertyChanged(nameof(IsFilterByPlayerEnabled));
			}
		}
	}
	public List<ReservationModel> ReservationList { get; set; }
	public List<UserModel> UserList { get; set; }
	public List<ReservationModel> NameFilteredList { get; set; }
	public List<ReservationModel> DateFilteredList { get; set; }
	public ObservableCollection<ReservationModel> FilteredReservations
	{
		get => _filteredReservations;
		set
		{
			if (_filteredReservations != value)
			{
				_filteredReservations = value;
				OnPropertyChanged(nameof(FilteredReservations));
			}
		}
	}
	public ObservableCollection<UserModel> FilteredUsers
	{
		get => _filteredUsers;
		set
		{
			if (_filteredUsers != value)
			{
				_filteredUsers = value;
				OnPropertyChanged(nameof(FilteredUsers));
			}
		}
	}

	public AdminPortalOverlayView()
	{
		InitializeComponent();

		_controller = new AdminPortalOverlayController();

		BindingContext = this;
	}

	private event EventHandler<UserModel?> UserTapped;


	private void SetFilteredReservations(List<ReservationModel> list)
	{
		FilteredReservations = new ObservableCollection<ReservationModel>(list);
	}

	public async void OnViewReservationsClicked(object? sender,  EventArgs e)
	{
		ViewReservationsBorder.IsVisible = true;

		ReservationList = await _controller.GetReservationsListFromDatabase();
		SetFilteredReservations(ReservationList);
	}

	private void OnFilterByDateSwitchToggled(object sender, ToggledEventArgs e)
	{
		if (_isFilterByDateEnabled)
		{
			DateFilteredList = _controller.FilterReservationsByDate(FilteredReservations.ToList(), ReservationDatePicker.Date);
			SetFilteredReservations(DateFilteredList);
		}
		else
		{
			DateFilteredList = ReservationList;

			if (_isFilterByPlayerEnabled)
			{
				SetFilteredReservations(NameFilteredList);
			}
			else
			{
				SetFilteredReservations(ReservationList);
			}
		}
	}

	private void OnFilterByNameSwitchToggled(object sender, ToggledEventArgs e)
	{
		if (_isFilterByPlayerEnabled)
		{
			if (ReservationNameEntry.Text is null || ReservationNameEntry.Text == string.Empty)
			{
				NameFilteredList = ReservationList;
			}
			else
			{
                NameFilteredList = _controller.FilterReservationsByPlayerName(FilteredReservations.ToList(), ReservationNameEntry.Text);
            }

            SetFilteredReservations(NameFilteredList);
        }
		else
		{
			NameFilteredList = ReservationList;

			if (_isFilterByDateEnabled)
			{
				SetFilteredReservations(DateFilteredList);
			}
			else
			{
				SetFilteredReservations(ReservationList);
			}
		}
	}

	private void OnReservationDateChanged(object sender, DateChangedEventArgs e)
	{
		if (sender is DatePicker dp)
		{
			if (_isFilterByPlayerEnabled)
			{
				DateFilteredList = _controller.FilterReservationsByDate(NameFilteredList, dp.Date);
			}
			else
			{
				DateFilteredList = _controller.FilterReservationsByDate(ReservationList, dp.Date);
			}

            SetFilteredReservations(DateFilteredList);
        }
	}

	private void OnPlayerNameChanged(object sender, TextChangedEventArgs e)
	{
		if (sender is Entry entry)
		{
			if (_isFilterByDateEnabled)
			{
				if (entry.Text is null || entry.Text == string.Empty)
				{
					NameFilteredList = DateFilteredList;
				}
				else
				{
                    NameFilteredList = _controller.FilterReservationsByPlayerName(DateFilteredList, entry.Text);
                }
			}
			else
			{
				if (entry.Text is null || entry.Text == string.Empty)
				{
					NameFilteredList = ReservationList;
				}
				else
				{
                    NameFilteredList = _controller.FilterReservationsByPlayerName(ReservationList, entry.Text);
                }
			}

            SetFilteredReservations(NameFilteredList);
        }
	}

	private void ClearReservationViewFilters()
	{
        FilterByPlayerSwitch.IsToggled = false;
        FilterByDateSwith.IsToggled = false;
        ReservationDatePicker.Date = DateTime.Today;
        ReservationNameEntry.Text = string.Empty;
    }
	private void ClearUserViewFilter()
	{

	}

	private void OnViewReservationsClosedClicked(object sender, EventArgs e)
	{
		this.IsVisible = false;
		ViewReservationsBorder.IsVisible = false;
		ClearReservationViewFilters();
	}

	private void SetFilteredUsers(List<UserModel> list)
	{
		FilteredUsers = new ObservableCollection<UserModel>(list);
	}

	public async void OnViewUsersClicked(object? sender, EventArgs e)
	{
		ViewSignedUpUsersBorder.IsVisible = true;

		UserList = await _controller.GetUsersListFromDatabase();
		SetFilteredUsers(UserList);
	}

	private void OnNameFilterTextChanged(object sender, TextChangedEventArgs e)
	{
		if (NameFilterEntry.Text is null || NameFilterEntry.Text == string.Empty)
		{
			SetFilteredUsers(UserList);
		}
		else
		{
			SetFilteredUsers(_controller.FilterUserListByName(UserList, NameFilterEntry.Text));
		}
	}

	private void OnViewUsersCloseButtonClicked(object sender, EventArgs e)
	{
		ViewSignedUpUsersBorder.IsVisible = false;
		this.IsVisible = false;
		ClearUserViewFilter();
	}

	private void UserEntryTapped(object sender, TappedEventArgs e)
	{
		ViewSignedUpUsersBorder.IsVisible = false;

		SelectedOverlay.IsVisible = true;

		if (e.Parameter is UserModel user)
		{
			SelectedUserGrid.BindingContext = user;
		}

		if (e.Parameter is GuestModel guest)
		{

		}
	}

	private void OnSelectedUserCloseButtonClicked(object sender, EventArgs e)
	{
		SelectedOverlay.IsVisible = false;

		ViewSignedUpUsersBorder.IsVisible = true;
	}
}