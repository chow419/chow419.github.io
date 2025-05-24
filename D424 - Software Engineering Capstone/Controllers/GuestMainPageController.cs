using D424___Software_Engineering_Capstone.Database;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class GuestMainPageController
    {
        public MainPageView MainView { get; set; }
        public DatabaseHandler _database { get; set; }

        public GuestMainPageController(MainPageView mainPageView)
        {
            MainView = mainPageView;

            mainPageView.StatePicker.ItemsSource = Constants.StateList;
            mainPageView.CountryPicker.ItemsSource = Constants.CountryList;

            _database = new DatabaseHandler();

            InitializeDatabase();
        }

        public async void InitializeDatabase()
        {
            await _database.AddAll();
        }

        public async Task<bool> ValidateUNPWSignUp(MainPageView mainPageView)
        {
            if (!await Validation.ValidateUserUsername(_database, MainView, MainView.UsernameEntry.Text))
            {
                return false;
            }

            if (!await Validation.ValidateUserPassword(MainView, MainView.PasswordEntry.Text))
            {
                return false;
            }

            return true;
        }
        
        public async Task<bool> ValidatePISignUp(MainPageView mainPageView)
        {
            if (!await Validation.ValidateUserFirstName(MainView, MainView.FirstNameEntry.Text))
            {
                return false;
            }

            if (!await Validation.ValidateUserLastName(MainView, MainView.LastNameEntry.Text))
            {
                return false;
            }

            if (!await Validation.ValidateUserPhoneNumber(MainView, MainView.PhoneNumberEntry.Text))
            {
                return false;
            }

            if (!await Validation.ValidateUserEmail(MainView, MainView.EmailEntry.Text))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ValidateLocationSignUp(MainPageView mainPageView)
        {
            if (!await Validation.ValidateUserStreetAddress(MainView, MainView.StreetAddressEntry.Text))
            {
                return false;
            }

            if (!await Validation.ValidateUserCity(MainView, MainView.CityEntry.Text))
            {
                return false;
            }

            if (!await Validation.ValidateUserZipCode(MainView, MainView.ZipCodeEntry.Text))
            {
                return false;
            }

            return true;
        }

       public async Task SignUpNewUser()
        {
            UserModel newUserModel = new()
            {
                FirstName = MainView.FirstNameEntry.Text,
                LastName = MainView.LastNameEntry.Text,
                PhoneNumber = MainView.PhoneNumberEntry.Text,
                Email = MainView.EmailEntry.Text,
                StreetAddress = MainView.StreetAddressEntry.Text,
                AddressLine2 = MainView.Address2Entry.Text,
                City = MainView.CityEntry.Text,
                State = MainView.StatePicker.SelectedItem.ToString(),
                ZipCode = MainView.ZipCodeEntry.Text,
                Country = MainView.CountryPicker.SelectedItem.ToString(),
                DateOfBirth = DateTime.Parse(MainView.DateOfBirthPicker.Date.ToString())
            };

            await _database.AddNewUser(newUserModel, MainView.UsernameEntry.Text, MainView.PasswordEntry.Text);

            MainView.CurrentUser = newUserModel;
        }

        public async Task<bool> VerifyLogin(string username, string password)
        {
            var hashSaltQuery = await _database.GetHashAndSalt(username);

            if (hashSaltQuery.Hash is null)
            {
                await MainView.DisplayAlert("Login Failed", "Invalid username or password.", "OK");

                return false;
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
                    DateOfBirth = userQuery.DateOfBirth
                };

                MainView.CurrentUser = signedInUser;

                return true;
            }
            else
            {
                await MainView.DisplayAlert("Login Failed", "Invalid username or password.", "OK");

                return false;
            }
        }

        public void GetGuest()
        {
            MainView.CurrentUser = new GuestModel();
        }

        public virtual void PopulateProfileMenuOverlay()
        {
            // Populate the profile menu overlay with user information
        }
    }
}
