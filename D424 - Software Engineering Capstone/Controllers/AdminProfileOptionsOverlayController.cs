using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class AdminProfileOptionsOverlayController : GuestProfileOptionsOverlayController
    {
        public AdminProfileOptionsOverlayController()
        {
            // Initialize any necessary components or services here
        }

        public event EventHandler? MemberPortalTapped;
        public event EventHandler? AdminPortalTapped;
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

            var adminPortalLabel = new Label { Text = "Admin Portal", FontSize = 16, HorizontalOptions = LayoutOptions.End };

            var logOutLabel = new Label { Text = "Log Out", FontSize = 16, HorizontalOptions = LayoutOptions.End };

            var memberPortalTap = new TapGestureRecognizer();

            var adminPortalTap = new TapGestureRecognizer();

            var logOutTap = new TapGestureRecognizer();

            memberPortalTap.Tapped += (s, e) => { MemberPortalTapped?.Invoke(s, e); };

            adminPortalTap.Tapped += (s, e) => { AdminPortalTapped?.Invoke(s, e); };

            logOutTap.Tapped += (s, e) => { LogOutTapped?.Invoke(s, e); };

            memberPortalLabel.GestureRecognizers.Add(memberPortalTap);
            adminPortalLabel.GestureRecognizers.Add(adminPortalTap);
            logOutLabel.GestureRecognizers.Add(logOutTap);

            layout.Children.Add(memberPortalLabel);
            layout.Children.Add(adminPortalLabel);
            layout.Children.Add(logOutLabel);

            return layout;
        }
    }
}
