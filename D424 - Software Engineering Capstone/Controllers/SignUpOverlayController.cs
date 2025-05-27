using D424___Software_Engineering_Capstone.Database;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class SignUpOverlayController
    {
        public DatabaseHandler _database { get; set; }

        public SignUpOverlayController()
        {
            _database = new DatabaseHandler();
        }

        public List<string> PopulateStateList()
        {
            return Constants.StateList;
        }

        public List<string> PopulateCountryList()
        {
            return Constants.CountryList;
        }

        public event Func<string, string, string, Task>? DisplayAlert;

        public async Task<bool> ValidateUNPWSignUp(string username, string password)
        {
            if (!await Validation.ValidateUserUsername(_database, DisplayAlert, username))
            {

                return false;
            }

            if (!await Validation.ValidateUserPassword(DisplayAlert, username, password))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ValidatePISignUp(string firstName, string lastName, string phoneNumber, string email)
        {
            if (!await Validation.ValidateUserFirstName(DisplayAlert, firstName))
            {
                return false;
            }

            if (!await Validation.ValidateUserLastName(DisplayAlert, lastName))
            {
                return false;
            }

            if (!await Validation.ValidateUserPhoneNumber(DisplayAlert, phoneNumber))
            {
                return false;
            }

            if (!await Validation.ValidateUserEmail(DisplayAlert, email))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ValidateLocationSignUp(string address1, string city, string zipCode)
        {
            if (!await Validation.ValidateUserStreetAddress(DisplayAlert, address1))
            {
                return false;
            }

            if (!await Validation.ValidateUserCity(DisplayAlert, city))
            {
                return false;
            }

            if (!await Validation.ValidateUserZipCode(DisplayAlert, zipCode))
            {
                return false;
            }

            return true;
        }

        public async Task<UserModel> SignUpNewUser(string username, string password, string firstName, string lastName, string phoneNumber, string email,
            string address1, string address2, string city, string state, string zipCode, string country, DateTime dateOfBirth)
        {
            UserModel newUserModel = new()
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email,
                StreetAddress = address1,
                AddressLine2 = address2,
                City = city,
                State = state,
                ZipCode = zipCode,
                Country = country,
                DateOfBirth = dateOfBirth
            };

            await _database.AddNewUser(newUserModel, username, password);

            return newUserModel;
        }

    }
}
