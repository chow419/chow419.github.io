namespace D424___Software_Engineering_Capstone.Models
{
    public class ReservationModel
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public GuestModel Player { get; set; }
        public bool IsPlayerGuest { get; set; }
        public int NumberOfPlayers { get; set; }
    }
}
