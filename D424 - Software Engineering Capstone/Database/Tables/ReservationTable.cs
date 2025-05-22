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

        [Column("teeTimeDate")]
        public DateTime TeeTimeDate { get; set; }

        [Column("teeTimeTime")]
        public DateTime TeeTimeTime { get; set; }

        [Column("numberOfPlayers")]
        public int NumberOfPlayers { get; set; }
    }
}
