using D424___Software_Engineering_Capstone.Database;
using D424___Software_Engineering_Capstone.Models;
using System.Collections.ObjectModel;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class AdminPortalOverlayController
    {
        public DatabaseHandler _database { get; set; }

        public AdminPortalOverlayController()
        {
            _database = new();
        }

        public List<ReservationModel> FilterReservationsByDate(List<ReservationModel> list, DateTime date)
        {
            if (list is not null && list.Count > 0)
            {
                var reservationList = list.Where(r => r.Date == date).ToList();

                return reservationList;
            }

            return [];
        }

        public List<ReservationModel> FilterReservationsByPlayerName(List<ReservationModel> list, string playerName)
        {
            if (list is not null && list.Count > 0)
            {
                var reservationList = list.Where(r => $"{r.Player.FirstName} {r.Player.LastName}".ToLower().Contains(playerName.ToLower())).ToList();

                return reservationList;
            }

            return [];
        }

        public async Task<List<ReservationModel>> GetReservationsListFromDatabase()
        {
            var results = await _database.FetchAllReservations();

            List<ReservationModel> returnList = new();

            if (results is not null && results.Count > 0)
            {
                foreach (var result in results)
                {
                    ReservationModel reservation;

                    if (result.IsGuest)
                    {
                        var guest = await _database.GetGuestById(result.UserId);

                        reservation = new ReservationModel()
                        {
                            Date = result.ReservationDate,
                            Time = result.ReservationTime,
                            Player = new GuestModel()
                            {
                                FirstName = guest.FirstName,
                                LastName = guest.LastName,
                                PhoneNumber = guest.PhoneNumber
                            },
                            IsPlayerGuest = true,
                            NumberOfPlayers = result.NumberOfPlayers

                        };
                    }
                    else
                    {
                        var member = await _database.GetUserById(result.UserId);

                        reservation = new ReservationModel()
                        {
                            Date = result.ReservationDate,
                            Time = result.ReservationTime,
                            Player = new GuestModel()
                            {
                                FirstName = member.FirstName,
                                LastName = member.LastName,
                                PhoneNumber = member.PhoneNumber
                            },
                            IsPlayerGuest = false,
                            NumberOfPlayers = result.NumberOfPlayers
                        };
                    }

                    returnList.Add(reservation);
                }
            }

            return returnList;
        }

        public async Task<List<UserModel>> GetUsersListFromDatabase()
        {
            var tempList = await _database.FetchAllSignedUpUsers();
            List<UserModel> returnList = new();

            foreach (var user in tempList)
            {
                var userModel = new UserModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    StreetAddress = user.StreetAddress,
                    AddressLine2 = user.AddressLine2,
                    City = user.City,
                    ZipCode = user.ZipCode,
                    Country = user.Country,
                    DateOfBirth = user.DateOfBirth,
                    IsAdmin = user.IsAdmin
                };

                returnList.Add(userModel);
            }

            return returnList;
        }
        public List<UserModel> FilterUserListByName(List<UserModel> list, string name)
        {
            var filteredList = list.Where(u => u.FullName.ToLower().Contains(name.ToLower())).ToList();

            return filteredList;
        }
    }
}
