using D424___Software_Engineering_Capstone.Database;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class UserMainPageController : GuestMainPageController
    {
        public UserMainPageController(MainPageView mainPageView) : base(mainPageView)
        {
            MainView = mainPageView;

            _database = new DatabaseHandler();
        }

        public override VerticalStackLayout PopulateProfileMenuOverlay()
        {
            var layout = new VerticalStackLayout()
            {
                Spacing = 15,
                Padding = new Thickness(20),
                BackgroundColor = Colors.White
            };

            var memberPageLabel = new Label()
            {
                Text = "Member Page",
                FontSize = 16,
                HorizontalOptions = LayoutOptions.End
            };
            
            var logOutLabel = new Label()
            {
                Text = "Log Out",
                FontSize = 16,
                HorizontalOptions = LayoutOptions.End
            };

            var memberPageTapGesture = new TapGestureRecognizer();

            memberPageTapGesture.Tapped += (s, e) =>
            {
                MainView.OnMemberPageLabelTapped(s, e);
            };

            memberPageLabel.GestureRecognizers.Add(memberPageTapGesture);

            var logOutTapGesture = new TapGestureRecognizer();

            logOutTapGesture.Tapped += (s, e) =>
            {
                MainView.OnLogOutLabelTapped(s, e);
            };

            logOutLabel.GestureRecognizers.Add(logOutTapGesture);

            layout.Children.Add(memberPageLabel);
            layout.Children.Add(logOutLabel);

            return layout;
        }
    }
}
