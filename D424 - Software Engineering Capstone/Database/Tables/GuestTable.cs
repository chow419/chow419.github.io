using SQLite;

namespace D424___Software_Engineering_Capstone.Database.Tables
{
    public class GuestTable
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("firstName")]
        public string FirstName { get; set; } = ""; // Default first name is empty string

        [Column("lastName")]
        public string LastName { get; set; } = ""; // Default last name is empty string

        [Column("phoneNumber")]
        public string PhoneNumber { get; set; } = ""; // Default phone number is empty string
    }
}
