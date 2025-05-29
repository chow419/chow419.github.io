using D424___Software_Engineering_Capstone.Database;
using D424___Software_Engineering_Capstone.Database.Tables;
using D424___Software_Engineering_Capstone.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class TeeTimeController
    {
        public DatabaseHandler _database {  get; set; }

        public TeeTimeController()
        {
            _database = new DatabaseHandler();
        }

        public async Task<List<TeeTimeModel>> GetTeeTimesForDate(DateTime date)
        {
            var result = new List<TeeTimeModel>();

            var closure = await _database.GetClosuresByDate(date.Date);

            if (closure is not null)
            {
                return result;
            }

            var teeTimes = new List<DateTime>();
            var startTime = date.Date.AddHours(7);
            var endTime = date.Date.AddHours(16);

            for (var time = startTime; time <= endTime; time = time.AddMinutes(20))
            {
                teeTimes.Add(time);
            }

            var reservations = await _database.GetReservationsByDate(date.Date);

            foreach (var teeTime in teeTimes)
            {
                int reservedPlayers = reservations.Where(r => r.ReservationTime.TimeOfDay == teeTime.TimeOfDay)
                                                  .Sum(r => r.NumberOfPlayers);

                if (reservedPlayers < 4)
                {
                    result.Add(new TeeTimeModel
                    {
                        Date = date.Date,
                        Time = teeTime,
                        AvailablePlayerSlots = 4 - reservedPlayers
                    });
                }
            }

            return result;
        }

        public async Task<int> GetMemberId(UserModel member)
        {
            var result = await _database.GetUserByEmail(member.Email);

            return result.Id;
        }

        public async Task AddGuest(string firstName, string lastName, string phoneNumber)
        {
            var guest = new GuestModel
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };

            await _database.AddNewGuest(guest);
        }

        public async Task<GuestTable> GetGuest(string firstName, string lastName, string phoneNumber)
        {
            var result = await _database.GetGuestByNameAndNumber(firstName, lastName, phoneNumber);

            return result;
        }

        public async Task ScheduleTeeTime(int playerIdNumber, bool isGuest, ReservationModel teeTime)
        {
            await _database.AddNewReservation(playerIdNumber, isGuest, teeTime);
        }
    }
}
