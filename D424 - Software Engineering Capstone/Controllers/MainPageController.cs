using D424___Software_Engineering_Capstone.Database;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class MainPageController
    {
        public MainPageView MainView { get; set; }
        public DatabaseHandler _database { get; set; }

        public MainPageController(MainPageView mainPageView)
        {
            MainView = mainPageView;

            _database = new DatabaseHandler();

            InitializeDatabase();
        }

        public async void InitializeDatabase()
        {
            await _database.AddAll();
        }
    }
}
