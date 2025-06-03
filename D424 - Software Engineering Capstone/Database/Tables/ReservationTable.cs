using SQLite;

namespace D424___Software_Engineering_Capstone.Database.Tables
{
    public class ReservationTable
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("userId")]
        public int UserId { get; set; }

        [Column("isGuest")]
        public bool IsGuest { get; set; }

        [Column("teeTimeDate")]
        public DateTime ReservationDate { get; set; }

        [Column("teeTimeTime")]
        public DateTime ReservationTime { get; set; }

        [Column("numberOfPlayers")]
        public int NumberOfPlayers { get; set; }
    }
}
