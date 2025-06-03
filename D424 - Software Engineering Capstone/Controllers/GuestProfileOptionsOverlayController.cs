using System.Security.Cryptography.X509Certificates;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class GuestProfileOptionsOverlayController
    {

        public GuestProfileOptionsOverlayController()
        {
            // Initialize any necessary components or services here
        }

        public event EventHandler? ProfileSignInTapped;

        public event EventHandler? ProfileSignUpTapped;

        public virtual VerticalStackLayout PopulateProfileMenuOverlay()
        {
            var layout = new VerticalStackLayout()
            {
                Spacing = 15,
                Padding = new Thickness(20),
                BackgroundColor = Colors.White
            };

            var logInLabel = new Label { Text = "Sign In", FontSize = 16, HorizontalOptions = LayoutOptions.End };

            var registerLabel = new Label { Text = "Sign Up", FontSize = 16, HorizontalOptions = LayoutOptions.End };

            var logInTap = new TapGestureRecognizer();

            var registerTap = new TapGestureRecognizer();

            var needAccountLabel = new Label { Text = "Need an account?", FontSize = 16, HorizontalOptions = LayoutOptions.End };

            registerLabel.SetDynamicResource(Label.StyleProperty, "Link");

            var haveAccountLabel = new Label { Text = "Already have an account?", FontSize = 16, HorizontalOptions = LayoutOptions.End };

            logInLabel.SetDynamicResource(Label.StyleProperty, "Link");

            logInTap.Tapped += (s, e) => { ProfileSignInTapped?.Invoke(s, e); };

            registerTap.Tapped += (s, e) => { ProfileSignUpTapped?.Invoke(s, e); };

            logInLabel.GestureRecognizers.Add(logInTap);
            registerLabel.GestureRecognizers.Add(registerTap);

            layout.Children.Add(haveAccountLabel);
            layout.Children.Add(logInLabel);
            layout.Children.Add(needAccountLabel);
            layout.Children.Add(registerLabel);

            return layout;
        }

    }
}
