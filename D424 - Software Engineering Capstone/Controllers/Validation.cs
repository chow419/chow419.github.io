using D424___Software_Engineering_Capstone.Database;
using System.Net.Mail;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public static class Validation
    {
        public static async Task<bool> ValidateUserUsername(DatabaseHandler _database, Func<string, string, string, Task> displayAlert, string username)
        {
            if (username is null || username == string.Empty)
            {
                await displayAlert("Sign Up Failed", "Please enter a username.", "OK");
                return false;
            }

            var uniqueUserNameCheck = await _database.GetUserByUsername(username.Trim());

            if (uniqueUserNameCheck is not null)
            {
                await displayAlert("Sign Up Failed", "The username has already been taken.\n" +
                    "Please choose a unique username.", "OK");
                return false;
            }

            return true;
        }

        public static async Task<bool> ValidateUserPassword(Func<string, string, string, Task> displayAlert, string username, string password)
        {
            if (password is null || password == string.Empty)
            {
                await displayAlert("Sign Up Failed", "Please enter a password.", "OK");
                return false;
            }

            if (password.Length < 8)
            {
                await displayAlert("Sign Up Failed", "Password must be at least 8 characters long.", "OK");
                return false;
            }

            if (!password.Any(char.IsUpper))
            {
                await displayAlert("Sign Up Failed", "Password must contain at least one uppercase letter.", "OK");
                return false;
            }

            if (!password.Any(char.IsLower))
            {
                await displayAlert("Sign Up Failed", "Password must contain at least one lowercase letter.", "OK");
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                await displayAlert("Sign Up Failed", "Password must contain at least one digit.", "OK");
                return false;
            }

            if (!password.Any(c => "!@#$%^&*()_+-=[]{}|;':\",.<>?/`~".Contains(c)))
            {
                await displayAlert("Sign Up Failed", "Password must contain at least one special character.", "OK");
                return false;
            }

            if (password.Contains(" "))
            {
                await displayAlert("Sign Up Failed", "Password cannot contain spaces.", "OK");
                return false;
            }

            if (password == username)
            {
                await displayAlert("Sign Up Failed", "Password cannot be the same as the username.", "OK");
                return false;
            }

            if (password.Contains("password", StringComparison.OrdinalIgnoreCase))
            {
                await displayAlert("Sign Up Failed", "Password cannot contain the word 'password'.", "OK");
                return false;
            }

            return true;
        }

        public static async Task<bool> ValidateUserFirstName(Func<string, string, string, Task> displayAlert, string firstName)
        {
            if (firstName is null || firstName == string.Empty)
            {
                await displayAlert("Sign Up Failed", "Please enter a first name.", "OK");
                return false;
            }
            return true;
        }

        public static async Task<bool> ValidateUserLastName(Func<string, string, string, Task> displayAlert, string lastName)
        {
            if (lastName is null || lastName == string.Empty)
            {
                await displayAlert("Sign Up Failed", "Please enter a last name.", "OK");
                return false;
            }
            return true;
        }

        public static async Task<bool> ValidateUserPhoneNumber(Func<string, string, string, Task> displayAlert, string phoneNumber)
        {
            if (phoneNumber is null || phoneNumber == string.Empty)
            {
                await displayAlert("Sign Up Failed", "Please enter a phone number.", "OK");
                return false;
            }

            if (phoneNumber.Length != 10 || !phoneNumber.All(char.IsDigit))
            {
                await displayAlert("Sign Up Failed", "Phone number must be 10 digits long and contain only numbers.", "OK");
                return false;
            }

            return true;
        }

        public static async Task<bool> ValidateUserEmail(Func<string, string, string, Task> displayAlert, string email)
        {
            if (email is null || email == string.Empty)
            {
                await displayAlert("Sign Up Failed", "Please enter an email address.", "OK");
                return false;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                await displayAlert("Sign Up Failed", "Please enter a valid email address.", "OK");
                return false;
            }

            try
            {
                MailAddress mail = new MailAddress(email);
            }
            catch (FormatException)
            {
                await displayAlert("Sign Up Failed", "Please enter a valid email address.", "OK");
                return false;
            }

            return true;
        }

        public static async Task<bool> ValidateUserStreetAddress(Func<string, string, string, Task> displayAlert, string streetAddress)
        {
            if (streetAddress is null || streetAddress == string.Empty)
            {
                await displayAlert("Sign Up Failed", "Please enter a street address.", "OK");
                return false;
            }

            return true;
        }

        public static async Task<bool> ValidateUserCity(Func<string, string, string, Task> displayAlert, string city)
        {
            if (city is null || city == string.Empty)
            {
                await displayAlert("Sign Up Failed", "Please enter a city.", "OK");
                return false;
            }

            return true;
        }

        public static async Task<bool> ValidateUserZipCode(Func<string, string, string, Task> displayAlert, string zipCode)
        {
            if (zipCode is null || zipCode == string.Empty)
            {
                await displayAlert("Sign Up Failed", "Please enter a postal code.", "OK");
                return false;
            }
            if (zipCode.ToString().Length != 5 || !zipCode.All(char.IsDigit))
            {
                await displayAlert("Sign Up Failed", "Postal code must be 5 digits long and contain only numbers.", "OK");
                return false;
            }
            return true;
        }
    }
}
