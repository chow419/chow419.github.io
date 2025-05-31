using D424___Software_Engineering_Capstone.Database;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class EditMemberInfoController
    {
        public DatabaseHandler _database { get; set; }

        public EditMemberInfoController()
        {
            _database = new();
        }

        public async Task SubmitUserUpdates(UserModel user)
        {
            var userToUpdate = await _database.GetUserByEmail(user.Email);

            userToUpdate.StreetAddress = user.StreetAddress;
            userToUpdate.AddressLine2 = user.AddressLine2;
            userToUpdate.City = user.City;
            userToUpdate.State = user.State;
            userToUpdate.ZipCode = user.ZipCode;
            userToUpdate.Country = user.Country;
            userToUpdate.PhoneNumber = user.PhoneNumber;

            await _database.UpdateUserInformation(userToUpdate);
        }
    }
}
