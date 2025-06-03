using D424___Software_Engineering_Capstone.Database;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class ContactUsController
    {
        public DatabaseHandler _database { get; set; }

        public ContactUsController()
        {
            _database = new();
        }

        public async Task<ContactUsModel> GetContactInformation()
        {
            var result = await _database.GetContactInfo();

            var businessInfo = new ContactUsModel()
            {
                Name = result.Name,
                Address = result.Address,
                City = result.City,
                State = result.State,
                ZipCode = result.ZipCode,
                Country = result.Country,
                PhoneNumber = result.PhoneNumber,
                Email = result.Email,
                OpenTime = result.OpenTime,
                CloseTime = result.CloseTime
            };

            return businessInfo;
        }
    }
}
