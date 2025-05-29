namespace D424___Software_Engineering_Capstone.Models
{
    public class GuestModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = ""; // Default first name is empty string
        public string LastName { get; set; } = ""; // Default last name is empty string
        public string FullName => $"{FirstName} {LastName}";
        public string PhoneNumber { get; set; } = ""; // Default phone number is empty string
    }
}
