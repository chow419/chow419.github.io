using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;
using System.Collections.ObjectModel;

namespace D424___Software_Engineering_Capstone.Views;

public partial class CourseNewsView : ContentPage
{
	private bool _isLoadMoreEnabled;
	private int _currentOffset;
	private ObservableCollection<CourseNewsModel> _courseNews;
	private bool _isAdmin;

	public CourseNewsController _controller { get; set; }
	public ObservableCollection<CourseNewsModel> CourseNews
	{
		get => _courseNews;
		set
		{
			if (_courseNews != value)
			{
				_courseNews = value;
				OnPropertyChanged(nameof(CourseNews));
			}
		}
	}
	public int NumberOfNewsItems { get; set; }
	public int CurrentOffset
	{
		get => _currentOffset;
		set
		{
			if (_currentOffset != value)
			{
				_currentOffset = value;
				OnPropertyChanged(nameof(CurrentOffset));
			}
		}
	}
	public int PageSize { get; set; }
	public bool IsLoadMoreEnabled
	{
		get => _isLoadMoreEnabled;
		set
		{
			if (CurrentOffset >= NumberOfNewsItems)
			{
				_isLoadMoreEnabled = false;
			}
			else
			{
				_isLoadMoreEnabled = true;
			}

			OnPropertyChanged(nameof(IsLoadMoreEnabled));
		}
	}
	public GuestModel CurrentUser { get; set; }
	public bool IsAdmin
	{
		get => _isAdmin;
		set
		{
			if (_isAdmin != value)
			{
				_isAdmin = value;
				OnPropertyChanged(nameof(IsAdmin));
			}
		}
	}

	public CourseNewsView(GuestModel currentUser)
	{
		InitializeComponent();

        _controller = new CourseNewsController();

        InitializeCurrentUser(currentUser);

        _profileOptionsOverlay.InitializeController(CurrentUser);

        CurrentOffset = 0;
		PageSize = 10;

		InitializeCourseNews();

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

    private void InitializeCurrentUser(GuestModel currentUser)
    {
        if (currentUser is UserModel user)
        {
            IsAdmin = user.IsAdmin;
        }
        else
        {
            IsAdmin = false;
        }

        CurrentUser = currentUser;
    }

	private async void InitializeCourseNews()
	{
		var newsCount = await _controller.GetCourseNewsItemsCount();

        NumberOfNewsItems = newsCount;

		CourseNews = new ObservableCollection<CourseNewsModel>(await _controller.GetCourseNews(CurrentOffset, PageSize));

		CurrentOffset += 10;
	}

    private void OnProfileMenuButtonClicked(object sender, EventArgs e)
    {
        _profileOptionsOverlay.IsVisible = !_profileOptionsOverlay.IsVisible;
    }

    private void OnDeleteCourseNewsTapped(object sender, TappedEventArgs e)
    {
        // Delete selected news item
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