namespace D424___Software_Engineering_Capstone.Models
{
    public class ReservationModel
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public UserModel User { get; set; }
        public int NumberOfPlayers { get; set; }

        // Constructor
        public ReservationModel(DateTime date, DateTime time, UserModel user, int numberOfPlayers)
        {
            Date = date;
            Time = time;
            User = user;
            NumberOfPlayers = numberOfPlayers;
        }
    }
}
