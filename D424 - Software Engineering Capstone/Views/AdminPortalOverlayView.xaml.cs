using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;
using System.Collections.ObjectModel;

namespace D424___Software_Engineering_Capstone.Views;

public partial class AdminPortalOverlayView : ContentView
{
	private bool _isFilterByDateEnabled;
	private bool _isFilterByPlayerEnabled;
	private bool _isCourseClosedSwitchEnabled;
	private ObservableCollection<ReservationModel> _filteredReservations;
	private ObservableCollection<GuestModel> _filteredUsers;

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
	public bool IsCourseClosedSwitchEnabled
	{
		get => _isCourseClosedSwitchEnabled;
		set
		{
			if (_isCourseClosedSwitchEnabled != value)
			{
				_isCourseClosedSwitchEnabled = value;
				OnPropertyChanged(nameof(IsCourseClosedSwitchEnabled));
			}
		}
	}
	public List<ReservationModel> ReservationList { get; set; }
	public List<GuestModel> UserList { get; set; }
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
	public ObservableCollection<GuestModel> FilteredUsers
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
	public UserModel SelectedUser { get; set; }


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
		NameFilterEntry.Text = string.Empty;
	}

	private void ClearGuestViewFilter()
	{
		GuestNameFilterEntry.Text = string.Empty;
	}

	private void OnViewReservationsClosedClicked(object sender, EventArgs e)
	{
		this.IsVisible = false;
		ViewReservationsBorder.IsVisible = false;
		ClearReservationViewFilters();
	}

	private void SetFilteredUsers(List<GuestModel> list)
	{
		FilteredUsers = new ObservableCollection<GuestModel>(list);
	}

	private void SetFilteredUsers(List<UserModel> list)
	{
		FilteredUsers = new ObservableCollection<GuestModel>(list.Cast<GuestModel>().ToList());
	}

	public async void OnViewUsersClicked(object? sender, EventArgs e)
	{
		ViewSignedUpUsersBorder.IsVisible = true;

		UserList = await _controller.GetUsersListFromDatabase();

        SetFilteredUsers(UserList.Cast<GuestModel>().ToList());
	}

	private void OnNameFilterTextChanged(object sender, TextChangedEventArgs e)
	{
		if (NameFilterEntry.Text is null || NameFilterEntry.Text == string.Empty)
		{
			SetFilteredUsers(UserList);
		}
		else
		{
			SetFilteredUsers(_controller.FilterUserListByName(UserList.Cast<UserModel>().ToList(), NameFilterEntry.Text));
		}
	}

	private void OnViewUsersCloseButtonClicked(object sender, EventArgs e)
	{
		ViewSignedUpUsersBorder.IsVisible = false;

		FilteredUsers = new();

		this.IsVisible = false;
		ClearUserViewFilter();
	}

	private void UserEntryTapped(object sender, TappedEventArgs e)
	{
        DateOfBirthLabel.IsVisible = true;
        Address1Label.IsVisible = true;
        Address2Label.IsVisible = true;
        CityStateLabels.IsVisible = true;
        CountryLabel.IsVisible = true;
        IsAdminSwitch.IsVisible = true;
        IsAdminSwitch.IsEnabled = true;

        ViewSignedUpUsersBorder.IsVisible = false;

		SelectedOverlay.IsVisible = true;

		if (e.Parameter is UserModel user)
		{
			SelectedUserGrid.BindingContext = user;

			if (user.AddressLine2 is null || user.AddressLine2 == string.Empty)
			{
				Address2Label.IsVisible = false;
			}

			SelectedUser = user;
		}
		else if (e.Parameter is GuestModel guest)
		{
			SelectedUserGrid.BindingContext = guest;

			PhoneNumberLabel.IsVisible = true;
			DateOfBirthLabel.IsVisible = false;
			Address1Label.IsVisible = false;
			Address2Label.IsVisible = false;
			CityStateLabels.IsVisible = false;
			CountryLabel.IsVisible = false;
			IsAdminSwitch.IsVisible = false;
			IsAdminSwitch.IsEnabled = false;
		}
	}

	private void OnSelectedUserCancelButtonClicked(object sender, EventArgs e)
	{
		SelectedOverlay.IsVisible = false;

		ViewSignedUpUsersBorder.IsVisible = true;
	}

	private async void OnSelectedUserSaveButtonClicked(object sender, EventArgs e)
	{
		SelectedOverlay.IsVisible = false;

		ViewSignedUpUsersBorder.IsVisible = false;

		var tempUser = SelectedUser;

		tempUser.IsAdmin = IsAdminSwitch.IsToggled;

		await _controller.UpdateAdminInformation(tempUser);
	}

	public async void OnViewGuestsClicked(object? sender, EventArgs e)
	{
		ViewGuestsBorder.IsVisible = true;

		UserList = await _controller.GetGuestsFromDatabase();
		SetFilteredUsers(UserList);
	}

	private void OnViewGuestsCloseButtonClicked(object sender, EventArgs e)
	{
		ViewGuestsBorder.IsVisible = false;

		FilteredUsers = new();

		this.IsVisible = false;
		ClearGuestViewFilter();
	}

	public void OnAddCourseNewsButtonClicked(object? sender, EventArgs e)
	{
		ViewCourseNewsBorder.IsVisible = true;
	}

	private void OnCourseNewsClosedButtonClicked(object sender, EventArgs e)
	{
		ViewCourseNewsBorder.IsVisible = false;
	}

	private void ClearCourseNewsFields()
	{
		CourseNewsTitleEntry.Text = string.Empty;
		CourseNewsDetailsEditor.Text = string.Empty;
		CourseClosedDate.Date = DateTime.Today;
		CourseClosedReasonPicker.SelectedIndex = -1;
	}

	private void OnCourseNewsCancelButtonClicked(object sender, EventArgs e)
	{
		ClearCourseNewsFields();
		ViewCourseNewsBorder.IsVisible = false;
		this.IsVisible = false;
	}

	private async void OnCourseNewsSubmitButtonClicked(object sender, EventArgs e)
	{
		CourseNewsModel news = new()
		{
			Title = CourseNewsTitleEntry.Text,
			NewsDetails = CourseNewsDetailsEditor.Text,
			PostedDate = DateTime.Now
		};

		if (IsCourseClosedSwitchEnabled)
		{
			news.IsClosed = IsCourseClosedSwitchEnabled;
			news.ClosureDate = CourseClosedDate.Date;
			news.ClosureReason = CourseClosedReasonPicker.SelectedItem.ToString();
        }

		await _controller.AddCourseNewsToDatabase(news);

		ViewCourseNewsBorder.IsVisible = false;
		this.IsVisible = false;
	}
}