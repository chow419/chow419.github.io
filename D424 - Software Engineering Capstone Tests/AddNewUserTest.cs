using Xunit;
using D424___Software_Engineering_Capstone.Controllers;
using D424___Software_Engineering_Capstone.Models;
using D424___Software_Engineering_Capstone.Database;
using System.Windows.Markup;

namespace D424___Software_Engineering_Capstone_Tests
{
    public class AddNewUserTest
    {
        [Fact]
        public async Task AddNewUser_RetrieveUser_DeleteUser()
        {
            var userUsername = "testuser";
            var userPassword = "Test@1234";

            var newUser = new UserModel()
            {
                FirstName = "Stan",
                LastName = "Lee",
                PhoneNumber = "9039755451",
                Email = "imprt_fan@msn.com",
                StreetAddress = "4307 Linda Vista Way",
                City = "Boise",
                State = "ID",
                Country = "United States",
                ZipCode = "83704",
                DateOfBirth = new DateTime(1993, 01, 26),
                IsAdmin = true
            };

            var controller = new SignUpOverlayController();

            var retVal = await controller.SignUpNewUser(userUsername, userPassword, newUser);

            Assert.Equal(1, retVal.UserRowsAdded);
            Assert.Equal("Stan Lee", retVal.UserRetrieved.FullName);
            Assert.Equal(1, retVal.CredentialRowsAdded);

            var database = new DatabaseHandler();

            var rowsDeleted = await database.DeleteUserByID(retVal.UserRetrieved.Id);

            Assert.Equal(1, rowsDeleted);
        }


        [Fact]
        public async Task ScheduleTeeTime_Test()
        {
            DatabaseHandler db = new();
            var retInt = await db.ClearReservationsTable();
            List<(int ID, bool IsGuest, ReservationModel TeeTime)> resList = new();
            Random rng = new();
            var rows = 0;

            for (int i = 0; i < 3; i++)
            {
                var userId = i + 10;
                var day = rng.Next(1, 29);
                var month = rng.Next(1, 13);
                var date = new DateTime(2025, month, day);
                var time = DateTime.Today.AddHours(rng.Next(7, 17));
                bool isGuest;

                if (rng.Next(0, 2) == 1)
                {
                    isGuest = true;
                }
                else isGuest = false;

                var players = rng.Next(1, 5);

                ReservationModel resMod = new()
                {
                    Date = date,
                    Time = time,
                    IsPlayerGuest = isGuest,
                    NumberOfPlayers = players
                };

                TeeTimeController controller = new();
                var values = (userId, isGuest, resMod);
                resList.Add(values);
                rows += await controller.ScheduleTeeTime(userId, isGuest, resMod);
            }

            Assert.Equal(3, rows);            

            var teeTimes = await db.FetchAllReservations();

            Assert.NotNull(teeTimes);
                
            for (int i = 0; i < teeTimes.Count; i++)
            {
                var tt = teeTimes.Where(t => t.UserId == i + 10).FirstOrDefault();

                Assert.NotNull(tt);
                Assert.Equal(tt.UserId, resList[i].ID);
                Assert.Equal(tt.IsGuest, resList[i].IsGuest);
                Assert.Equal(tt.ReservationTime, resList[i].TeeTime.Time);
            }
            
        }
    }
}
