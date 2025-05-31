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

        [Column("score")]
        public int Score { get; set; }
    }
}
