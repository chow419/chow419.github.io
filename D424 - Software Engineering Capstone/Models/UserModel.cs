namespace D424___Software_Engineering_Capstone.Models
{
    public class UserModel : GuestModel
    {
        public string Email { get; set; }

        public string StreetAddress { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsAdmin { get; set; }
    }
}
