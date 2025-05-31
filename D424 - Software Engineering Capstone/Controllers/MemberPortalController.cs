using D424___Software_Engineering_Capstone.Database;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class MemberPortalController
    {
        public DatabaseHandler _database { get; set; }


        public MemberPortalController()
        {
            _database = new();
        }
    }
}
