using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;
using Microsoft.Maui.Controls.Shapes;

namespace D424___Software_Engineering_Capstone.Views;

public partial class ProfileOptionsOverlayView : ContentView
{
    public GuestProfileOptionsOverlayController _controller { get; set; }

    public ProfileOptionsOverlayView()
	{
		InitializeComponent();
    }


    // Public Events


    public event EventHandler? ProfileSignInTapped;

    public event EventHandler? ProfileSignUpTapped;

    public event EventHandler? MemberPortalTapped;

    public event EventHandler? AdminPortalTapped;

    public event EventHandler? LogOutTapped;


    // Private Methods


    private void GenerateProfileMenuOverlay()
    {
        var border = new Border()
        {
            AutomationId = "ProfileMenu",
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = new CornerRadius(10, 0, 20, 10)
            },
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            Margin = new Thickness(0, 0, 20, 0),
            Content = _controller.PopulateProfileMenuOverlay()
        };
        border.Shadow = new Shadow()
        {
            Brush = Colors.Black,
            Offset = new Point(10, 10),
            Radius = 16,
            Opacity = 0.5f
        };

        this.Content = border;
    }

    private void OnProfileSignInTapped(object? sender, EventArgs e)
    {
        this.IsVisible = false;

        ProfileSignInTapped?.Invoke(sender, e);
    }

    private void OnProfileSignUpTapped(object? sender, EventArgs e)
    {
        this.IsVisible = false;

        ProfileSignUpTapped?.Invoke(sender, e);
    }

    private void OnMemberPortalTapped(object? sender, EventArgs e)
    {
        this.IsVisible = false;
        MemberPortalTapped?.Invoke(sender, e);
    }

    private void OnAdminPortalTapped(object? sender, EventArgs e)
    {
        this.IsVisible = false;
        AdminPortalTapped?.Invoke(sender, e);
    }

    private void OnLogOutTapped(object? sender, EventArgs e)
    {
        this.IsVisible = false;
        LogOutTapped?.Invoke(sender, e);
    }


    // Public Methods


    public void InitializeController(GuestModel currentUser)
    {
        if (currentUser is UserModel user)
        {
            if (user.IsAdmin)
            {
                _controller = new AdminProfileOptionsOverlayController();

                if (_controller is AdminProfileOptionsOverlayController adminController)
                {
                    adminController.MemberPortalTapped += OnMemberPortalTapped;
                    adminController.AdminPortalTapped += OnAdminPortalTapped;
                    adminController.LogOutTapped += OnLogOutTapped;
                }
            }
            else
            {
                _controller = new UserProfileOptionsOverlayController();

                if (_controller is UserProfileOptionsOverlayController userController)
                {
                    userController.MemberPortalTapped += OnMemberPortalTapped;
                    userController.LogOutTapped += OnLogOutTapped;
                }
            }
        }
        else
        {
            _controller = new GuestProfileOptionsOverlayController();

            _controller.ProfileSignInTapped += OnProfileSignInTapped;
            _controller.ProfileSignUpTapped += OnProfileSignUpTapped;
        }

        GenerateProfileMenuOverlay();

    }
}