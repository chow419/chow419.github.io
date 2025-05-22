using D424___Software_Engineering_Capstone.Database;
using System.Net.Mail;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public static class Validation
    {
        public static async Task<bool> ValidateUserUsername(DatabaseHandler _database, ContentPage currentView, string username)
        {
            if (username is null || username == string.Empty)
            {
                await currentView.DisplayAlert("Sign Up Failed", "Please enter a username.", "OK");
                return false;
            }

            var uniqueUserNameCheck = await _database.GetUserByUsername(username.Trim());

            if (uniqueUserNameCheck is not null)
            {
                await currentView.DisplayAlert("Sign Up Failed", "The username has already been taken.\n" +
                    "Please choose a unique username.", "OK");
                return false;
            }

            return true;
        }

        public static async Task<bool> ValidateUserPassword(ContentPage currentView, string password)
        {
            if (password is null || password == string.Empty)
            {
                await currentView.DisplayAlert("Sign Up Failed", "Please enter a password.", "OK");
                return false;
            }

            if (password.Length < 8)
            {
                await currentView.DisplayAlert("Sign Up Failed", "Password must be at least 8 characters long.", "OK");
                return false;
            }

            if (!password.Any(char.IsUpper))
            {
                await currentView.DisplayAlert("Sign Up Failed", "Password must contain at least one uppercase letter.", "OK");
                return false;
            }

            if (!password.Any(char.IsLower))
            {
                await currentView.DisplayAlert("Sign Up Failed", "Password must contain at least one lowercase letter.", "OK");
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                await currentView.DisplayAlert("Sign Up Failed", "Password must contain at least one digit.", "OK");
                return false;
            }

            if (!password.Any(c => "!@#$%^&*()_+-=[]{}|;':\",.<>?/`~".Contains(c)))
            {
                await currentView.DisplayAlert("Sign Up Failed", "Password must contain at least one special character.", "OK");
                return false;
            }

            if (password.Contains(" "))
            {
                await currentView.DisplayAlert("Sign Up Failed", "Password cannot contain spaces.", "OK");
                return false;
            }

            if (currentView is MainPageView mainPageView)
            {
                if (password == mainPageView.UsernameEntry.Text)
                {
                    await currentView.DisplayAlert("Sign Up Failed", "Password cannot be the same as the username.", "OK");
                    return false;
                }
            }

            if (password.Contains("password", StringComparison.OrdinalIgnoreCase))
            {
                await currentView.DisplayAlert("Sign Up Failed", "Password cannot contain the word 'password'.", "OK");
                return false;
            }

            return true;
        }

        public static async Task<bool> ValidateUserFirstName(ContentPage currentView, string firstName)
        {
            if (firstName is null || firstName == string.Empty)
            {
                await currentView.DisplayAlert("Sign Up Failed", "Please enter a first name.", "OK");
                return false;
            }
            return true;
        }

        public static async Task<bool> ValidateUserLastName(ContentPage currentView, string lastName)
        {
            if (lastName is null || lastName == string.Empty)
            {
                await currentView.DisplayAlert("Sign Up Failed", "Please enter a last name.", "OK");
                return false;
            }
            return true;
        }

        public static async Task<bool> ValidateUserPhoneNumber(ContentPage currentView, string phoneNumber)
        {
            if (phoneNumber is null || phoneNumber == string.Empty)
            {
                await currentView.DisplayAlert("Sign Up Failed", "Please enter a phone number.", "OK");
                return false;
            }

            if (phoneNumber.Length != 10 || !phoneNumber.All(char.IsDigit))
            {
                await currentView.DisplayAlert("Sign Up Failed", "Phone number must be 10 digits long and contain only numbers.", "OK");
                return false;
            }

            return true;
        }

        public static async Task<bool> ValidateUserEmail(ContentPage currentView, string email)
        {
            if (email is null || email == string.Empty)
            {
                await currentView.DisplayAlert("Sign Up Failed", "Please enter an email address.", "OK");
                return false;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                await currentView.DisplayAlert("Sign Up Failed", "Please enter a valid email address.", "OK");
                return false;
            }

            try
            {
                MailAddress mail = new MailAddress(email);
            }
            catch (FormatException)
            {
                await currentView.DisplayAlert("Sign Up Failed", "Please enter a valid email address.", "OK");
                return false;
            }

            return true;
        }

        public static async Task<bool> ValidateUserStreetAddress(ContentPage currentView, string streetAddress)
        {
            if (streetAddress is null || streetAddress == string.Empty)
            {
                await currentView.DisplayAlert("Sign Up Failed", "Please enter a street address.", "OK");
                return false;
            }

            return true;
        }

        public static async Task<bool> ValidateUserCity(ContentPage currentView, string city)
        {
            if (city is null || city == string.Empty)
            {
                await currentView.DisplayAlert("Sign Up Failed", "Please enter a city.", "OK");
                return false;
            }

            return true;
        }

        public static async Task<bool> ValidateUserZipCode(ContentPage currentView, string zipCode)
        {
            if (zipCode == string.Empty)
            {
                await currentView.DisplayAlert("Sign Up Failed", "Please enter a postal code.", "OK");
                return false;
            }
            if (zipCode.ToString().Length != 5 || !zipCode.All(char.IsDigit))
            {
                await currentView.DisplayAlert("Sign Up Failed", "Postal code must be 5 digits long and contain only numbers.", "OK");
                return false;
            }
            return true;
        }
    }
}
