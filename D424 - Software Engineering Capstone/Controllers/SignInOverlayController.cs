using D424___Software_Engineering_Capstone.Database;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class SignInOverlayController
    {
        public DatabaseHandler _database { get; set; }

        public SignInOverlayController()
        {
            _database = new DatabaseHandler();
        }


        public async Task<(bool IsVerified, UserModel? SignedInUser)> VerifyLogin(string username, string password, Func<string, string, string, Task> displayAlert)
        {
            var hashSaltQuery = await _database.GetHashAndSalt(username);

            if (hashSaltQuery.Hash is null)
            {
                await displayAlert("Login Failed", "Invalid username or password.", "OK");

                return (false, null);
            }
            else if (PasswordHasher.VerifyPassword(password, hashSaltQuery.Hash, hashSaltQuery.Salt))
            {
                var userQuery = await _database.GetUserById(hashSaltQuery.UserId);

                UserModel signedInUser = new UserModel()
                {
                    Id = userQuery.Id,
                    FirstName = userQuery.FirstName,
                    LastName = userQuery.LastName,
                    PhoneNumber = userQuery.PhoneNumber,
                    Email = userQuery.Email,
                    StreetAddress = userQuery.StreetAddress,
                    AddressLine2 = userQuery.AddressLine2,
                    City = userQuery.City,
                    State = userQuery.State,
                    ZipCode = userQuery.ZipCode,
                    Country = userQuery.Country,
                    DateOfBirth = userQuery.DateOfBirth,
                    IsAdmin = userQuery.IsAdmin
                };

                return (true, signedInUser);
            }
            else
            {
                await displayAlert("Login Failed", "Invalid username or password.", "OK");

                return (false, null);
            }
        }

        public GuestModel GetGuest()
        {
            return new GuestModel();
        }

    }
}
