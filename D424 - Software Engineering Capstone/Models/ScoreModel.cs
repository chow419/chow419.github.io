namespace D424___Software_Engineering_Capstone.Models
{
    class ScoreModel
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int Hole { get; set; }
        public int Score { get; set; }
        public bool GIR { get; set; }
        public bool FIR { get; set; }
        public int Putts { get; set; }

        public ScoreModel(int hole, int score)
        {
            Hole = hole;
            Score = score;
        }
    }
}
