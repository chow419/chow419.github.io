using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace D424___Software_Engineering_Capstone.Views;

public partial class MemberPortalView : ContentPage
{
	private Double _scoreAverage;
	private ObservableCollection<ScoreModel> _scoreList;
	private GuestModel currentUser;


    public GuestModel CurrentUser
    {
        get => currentUser;
        set
        {
            if (currentUser != value)
            {
                currentUser = value;

                if (GlobalVariables.CurrentUser != value)
                {
                    GlobalVariables.CurrentUser = value;
                }
            }

            OnPropertyChanged(nameof(CurrentUser));
        }
    }
    public MemberPortalController _controller { get; set; }
	public ObservableCollection<ScoreModel> ScoreList
	{
		get => _scoreList;
		set
		{
			if (_scoreList != value)
			{
				_scoreList = value;
				OnPropertyChanged(nameof(ScoreList));
			}
		}
	}
	public Double ScoreAverage
	{
		get => _scoreAverage;
		set
		{
			if (_scoreAverage != value)
			{
				_scoreAverage = value;
				OnPropertyChanged(nameof(ScoreAverage));
			}
		}
	}


	public MemberPortalView(UserModel currentUser)
	{
		InitializeComponent();

		_controller = new();

		CurrentUser = currentUser;

		GetScoresList();

		GetCourseAverage();

		_profileOptionsOverlay.InitializeController(CurrentUser);

		this.EditContactInfo += _editContactInformationOverlay.OnEditContactInfoOpened;

		_profileOptionsOverlay.AdminPortalTapped += OnAdminPortalLabelTapped;


		BindingContext = this;
	}

	private event EventHandler<UserModel?> EditContactInfo;

	private async void OnScoreSubmitButtonClicked(object sender, EventArgs e)
	{
		ScoreModel score = new()
		{
			Date = ScoreDatePicker.Date,
			Score = Convert.ToInt16(ScoreEntry.Text)
		};

		ScoreEntry.Text = string.Empty;

		await _controller.AddRoundScore((UserModel)CurrentUser, score);

		GetCourseAverage();
		GetScoresList();
	}

	private async void GetCourseAverage()
	{
		var courseAverage = await _controller.CalculateScoreAverage((UserModel)CurrentUser);

		ScoreAverage = courseAverage;
	}

	private async void GetScoresList()
	{
		var result = await _controller.FetchUserScores((UserModel)CurrentUser);

		ScoreList = new ObservableCollection<ScoreModel>(result);
	}

	private void OnEditContactInfoButtonClicked(object sender, EventArgs e)
	{
		_editContactInformationOverlay.IsVisible = true;

		if (CurrentUser is UserModel user)
		{
			EditContactInfo?.Invoke(this, user);
		}
	}

	private void OnProfileMenuButtonTapped(object sender, EventArgs e)
	{
		_profileOptionsOverlay.IsVisible = !_profileOptionsOverlay.IsVisible;
	}







	private void SetNavigationBarColor(Color color)
	{
		if (Application.Current.MainPage is NavigationPage navPage)
		{
			navPage.BarBackgroundColor = color;
		}
	}

	private async void OnAdminPortalLabelTapped(object? sender, EventArgs e)
	{
		await Navigation.PopToRootAsync();

		await Navigation.PushAsync(new AdminPortalView((UserModel)CurrentUser));
	}

	private async Task OnLogOutLabelTapped(object? sender, EventArgs e)
	{
		CurrentUser = null;

		SetNavigationBarColor(Color.FromRgb(8, 105, 8));

		ProfileMenuIcon.IsEnabled = false;

		_signInOverlay.IsVisible = true;

		await Navigation.PopToRootAsync();
	}
}