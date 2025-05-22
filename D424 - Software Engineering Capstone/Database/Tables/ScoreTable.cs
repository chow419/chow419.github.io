using SQLite;

namespace D424___Software_Engineering_Capstone.Database.Tables
{
    public class ScoreTable
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("hole")]
        public int Hole { get; set; }

        [Column("score")]
        public int Score { get; set; }

        [Column("gir")]
        public bool GIR { get; set; }

        [Column("fir")]
        public bool FIR { get; set; }

        [Column("putts")]
        public int Putts { get; set; }
    }
}
