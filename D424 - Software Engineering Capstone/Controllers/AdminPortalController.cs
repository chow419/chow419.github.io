using D424___Software_Engineering_Capstone.Database;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class AdminPortalController
    {
        public DatabaseHandler _database { get; set; }

        public AdminPortalController()
        {
            _database = new DatabaseHandler();
        }

        public event EventHandler? OnViewReservations;
    }
}
