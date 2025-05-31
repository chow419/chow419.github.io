using SQLite;

namespace D424___Software_Engineering_Capstone.Database.Tables
{
    public class ContactUsInfoTable
    {
        [PrimaryKey,  AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("businessName")]
        public string Name { get; set; }

        [Column("businessAddress")]
        public string Address { get; set; }

        [Column("businessCity")]
        public string City { get; set; }

        [Column("businessState")]
        public string State { get; set; }

        [Column("businessZipCode")]
        public string ZipCode { get; set; }

        [Column("businessCountry")]
        public string Country { get; set; }

        [Column("businessPhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("businessEmail")]
        public string Email { get; set; }

        [Column("openTime")]
        public DateTime OpenTime { get; set; }

        [Column("closeTime")]
        public DateTime CloseTime { get; set; }
    }
}
