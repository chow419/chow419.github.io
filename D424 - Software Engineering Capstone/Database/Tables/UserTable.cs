using SQLite;

namespace D424___Software_Engineering_Capstone.Database.Tables
{
    public class UserTable
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("street_address")]
        public string StreetAddress { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("zip_code")]
        public string ZipCode { get; set; }

        [Column("country")]
        public string Country { get; set; }

        [Column("dob")]
        public DateTime DateOfBirth { get; set; }

        [Column("isAdmin")]
        public bool IsAdmin { get; set; }
    }
}
