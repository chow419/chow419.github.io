using SQLite;

namespace D424___Software_Engineering_Capstone.Database.Tables
{
    public class CredentialsTable
    {
        [PrimaryKey]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("username")]
        public string UserName { get; set; }

        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Column("salt")]
        public string Salt { get; set; }
    }
}
