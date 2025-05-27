using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Views;

public partial class SignInOverlayView : ContentView
{
    public SignInOverlayController _controller { get; set; }

    public SignInOverlayView()
	{
		InitializeComponent();

        _controller = new();
	}
    
    public event EventHandler? RequestSignUp;
    
    public event EventHandler<UserModel> SuccessfulSignIn;

    public event EventHandler<GuestModel?> ContinueAsGuest;
    
    private void ClearSignInFields()
    {
        SignInUsernameEntry.Text = string.Empty;
        SignInPasswordEntry.Text = string.Empty;
    }

    public async void OnSignInButtonClicked(object sender, EventArgs e)
    {
        var verificationResults = await _controller.VerifyLogin(
            SignInUsernameEntry.Text,
            SignInPasswordEntry.Text
        );

        if (verificationResults.IsVerified)
        {
            var signedInUser = verificationResults.SignedInUser;

            this.IsVisible = false;

            ClearSignInFields();

            SuccessfulSignIn?.Invoke(sender, signedInUser);
        }
        else
        {
            SignInPasswordEntry.Text = string.Empty;
        }
    }

    public void OnSignUpLabelTapped(object sender, EventArgs e)
    {
        ClearSignInFields();

        RequestSignUp?.Invoke(this, EventArgs.Empty);
    }

    public void OnContinueAsGuestLabelTapped(object sender, TappedEventArgs e)
    {
        var guest = _controller.GetGuest();

        ContinueAsGuest?.Invoke(this, guest);

        this.IsVisible = false;

        ClearSignInFields();
    }
}