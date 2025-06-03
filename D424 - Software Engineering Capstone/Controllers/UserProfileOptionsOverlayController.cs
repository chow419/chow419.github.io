namespace D424___Software_Engineering_Capstone.Controllers
{
    public class UserProfileOptionsOverlayController : GuestProfileOptionsOverlayController
    {
        public UserProfileOptionsOverlayController()
        {
            // Initialize any necessary components or services here
        }

        public event EventHandler? MemberPortalTapped;
        public event EventHandler? LogOutTapped;

        public override VerticalStackLayout PopulateProfileMenuOverlay()
        {
            var layout = new VerticalStackLayout()
            {
                Spacing = 15,
                Padding = new Thickness(20),
                BackgroundColor = Colors.White
            };

            var memberPortalLabel = new Label { Text = "Member Portal", FontSize = 16, HorizontalOptions = LayoutOptions.End };

            var logOutLabel = new Label { Text = "Log Out", FontSize = 16, HorizontalOptions = LayoutOptions.End };

            var memberPortalTap = new TapGestureRecognizer();

            var logOutTap = new TapGestureRecognizer();

            memberPortalTap.Tapped += (s, e) => { MemberPortalTapped?.Invoke(s, e); };

            logOutTap.Tapped += (s, e) => { LogOutTapped?.Invoke(s, e); };

            memberPortalLabel.GestureRecognizers.Add(memberPortalTap);
            logOutLabel.GestureRecognizers.Add(logOutTap);

            layout.Children.Add(memberPortalLabel);
            layout.Children.Add(logOutLabel);

            return layout;
        }
    }
}
