namespace D424___Software_Engineering_Capstone.Models
{
    public class TeeTimeModel
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int AvailablePlayerSlots { get; set; } = 4; // Default to 4 players per Tee Time

        // Constructor
        public TeeTimeModel(DateTime date, DateTime time)
        {
            Date = date;
            Time = time;
        }
    }
}
