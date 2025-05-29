using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;
using System.Collections.ObjectModel;

namespace D424___Software_Engineering_Capstone.Views;

public partial class AdminPortalOverlayView : ContentView
{
	private bool _isFilterByDateEnabled;
	private bool _isFilterByPlayerEnabled;
	private ObservableCollection<ReservationModel> _filteredList;

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
	public List<ReservationModel> NameFilteredList { get; set; }
	public List<ReservationModel> DateFilteredList { get; set; }
	public ObservableCollection<ReservationModel> FilteredList
	{
		get => _filteredList;
		set
		{
			if (_filteredList != value)
			{
				_filteredList = value;
				OnPropertyChanged(nameof(FilteredList));
			}
		}
	}

	public AdminPortalOverlayView()
	{
		InitializeComponent();

		_controller = new AdminPortalOverlayController();

		if (Parent is VisualElement parent)
		{
			ViewReservationsBorder.MaximumHeightRequest = parent.Height;
		}

		BindingContext = this;
	}

	public async void OnViewReservationsClicked(object? sender,  EventArgs e)
	{
		ViewReservationsBorder.IsVisible = true;

		ReservationList = await _controller.GetReservationsListFromDatabase();
		FilteredList = new ObservableCollection<ReservationModel>(ReservationList);
	}

	private void OnFilterByDateSwitchToggled(object sender, ToggledEventArgs e)
	{
		if (_isFilterByDateEnabled)
		{
			DateFilteredList = _controller.FilterReservationsByDate(FilteredList.ToList(), ReservationDatePicker.Date);
			FilteredList = new ObservableCollection<ReservationModel>(DateFilteredList);
		}
		else
		{
			DateFilteredList = ReservationList;

			if (_isFilterByPlayerEnabled)
			{
				FilteredList = new ObservableCollection<ReservationModel>(NameFilteredList);
			}
			else
			{
				FilteredList = new ObservableCollection<ReservationModel>(ReservationList);
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
                NameFilteredList = _controller.FilterReservationsByPlayerName(FilteredList.ToList(), ReservationNameEntry.Text);
            }

            FilteredList = new ObservableCollection<ReservationModel>(NameFilteredList);
        }
		else
		{
			NameFilteredList = ReservationList;

			if (_isFilterByDateEnabled)
			{
				FilteredList = new ObservableCollection<ReservationModel>(DateFilteredList);
			}
			else
			{
				FilteredList = new ObservableCollection<ReservationModel>(ReservationList);
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

            FilteredList = new ObservableCollection<ReservationModel>(DateFilteredList);
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

            FilteredList = new ObservableCollection<ReservationModel>(NameFilteredList);
        }
	}

	private void ClearReservationViewFilters()
	{
        FilterByPlayerSwitch.IsToggled = false;
        FilterByDateSwith.IsToggled = false;
        ReservationDatePicker.Date = DateTime.Today;
        ReservationNameEntry.Text = string.Empty;
    }

	private void OnViewReservationsClosedClicked(object sender, EventArgs e)
	{
		this.IsVisible = false;
		ClearReservationViewFilters();
	}

}