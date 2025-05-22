using D424___Software_Engineering_Capstone.Database;

namespace D424___Software_Engineering_Capstone.Controllers
{
    class AdminMainPageController : GuestMainPageController
    {
        public AdminMainPageController(MainPageView mainPageView) : base(mainPageView)
        {
            MainView = mainPageView;

            _database = new DatabaseHandler();
        }

        public override void PopulateProfileMenuOverlay()
        {
            base.PopulateProfileMenuOverlay();
        }
    }
}
