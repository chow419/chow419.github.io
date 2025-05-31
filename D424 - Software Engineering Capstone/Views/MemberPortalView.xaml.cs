using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace D424___Software_Engineering_Capstone.Views;

public partial class MemberPortalView : ContentPage
{
	private Double _scoreAverage;
	private ObservableCollection<ScoreModel> _scoreList;

	public UserModel CurrentUser { get; set; }
	public MemberPortalController _controller { get; set; }
	public ObservableCollection<ScoreModel> ScoreList
	{
		get => _scoreList;
		set
		{
			if (_scoreList  != value)
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

		BindingContext = this;
	}

	private async void OnScoreSubmitButtonClicked(object sender, EventArgs e)
	{
		ScoreModel score = new()
		{
			Date = ScoreDatePicker.Date,
			Score = Convert.ToInt16(ScoreEntry.Text)
		};

		await _controller.AddRoundScore(CurrentUser, score);
	}

	private async void GetCourseAverage()
	{
		var courseAverage = await _controller.CalculateScoreAverage(CurrentUser);

		ScoreAverage = courseAverage;
	}

	private async void GetScoresList()
	{
		var result = await _controller.FetchUserScores(CurrentUser);

		ScoreList = new ObservableCollection<ScoreModel>(result);
	}

	private void OnEditContactInfoButtonClicked(object sender, EventArgs e)
	{
		// Open Edit Contact Info Overlay/View
	}

	private void ClearEditContactInfoFields()
	{
		//Clear Edit Contact Info Fields
	}

	private void OnCancelEditContactInfoButtonClicked(object sender, EventArgs e)
	{
		// Close Edit Contact Info Overlay/View
		ClearEditContactInfoFields();
	}

	private async Task OnSubmitEditContactInfoButtonClicked(object sender, EventArgs e)
	{
		await _controller.UpdateUserInformation(CurrentUser);

		// Close Edit Contact Info Overlay/View
		ClearEditContactInfoFields();
	}

	private void OnProfileMenuButtonTapped(object sender, EventArgs e)
	{
		_profileOptionsOverlay.IsVisible = !_profileOptionsOverlay.IsVisible;
	}
}