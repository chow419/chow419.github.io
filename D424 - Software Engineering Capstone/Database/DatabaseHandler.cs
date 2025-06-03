using SQLite;
using D424___Software_Engineering_Capstone.Database.Tables;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Database
{
    public class DatabaseHandler
    {
        SQLiteAsyncConnection _connection;

        public DatabaseHandler()
        {
        }

        private async Task<bool> CheckTableForValues<T>() where T: new()
        {
            var query = await _connection.Table<T>().FirstOrDefaultAsync();

            return query != null;
        }

        public async Task AddMyselfAsAdmin()
        {
            await Init();

            if (await CheckTableForValues<UserTable>())
            {
                return;
            }

            UserModel cameron = new()
            {
                FirstName = "Cameron",
                LastName = "Howard",
                PhoneNumber = "2088412881",
                Email = "socialenigma11@gmail.com",
                StreetAddress = "511 McMillan St.",
                AddressLine2 = "Apt. A",
                City = "Winnsboro",
                State = "Texas",
                ZipCode = "75494",
                Country = "United States",
                DateOfBirth = DateTime.ParseExact("1998-07-19", "yyyy-MM-dd", null),
                IsAdmin = true
            };

            await AddNewUser(cameron, "socialenigma11", "T#eD@rkUrg3");
        }

        public async Task AddHolidayClosures()
        {
            await Init();

            if (await CheckTableForValues<ClosuresTable>())
            {
                return;
            }

            var newYear = DateTime.Parse("2026/01/01");
            var independence = DateTime.Parse("2025/07/04");
            var christmas = DateTime.Parse("2025/12/25");

            List<DateTime> holidays = new();
            List<ClosuresTable> closures = new();

            holidays.Add(independence);
            holidays.Add(newYear);
            holidays.Add(christmas);

            foreach (var holiday in holidays)
            {
                var closureTable = new ClosuresTable()
                {
                    ClosureDate = holiday,
                    ClosureReason = "Holiday"
                };

                closures.Add(closureTable);
            }

            await _connection.InsertAllAsync(closures);
        }

        public async Task AddBusinessInfo()
        {
            await Init();

            if (await CheckTableForValues<ContactUsInfoTable>())
            {
                return;
            }

            var businessInfo = new ContactUsInfoTable()
            {
                Name = "Parallel 33 Country Club",
                Address = "201 E. Carnegie",
                City = "Winnsboro",
                State = "Texas",
                ZipCode = "75494",
                Country = "United States",
                PhoneNumber = "9038412881",
                Email = "contact@parallel33.com",
                OpenTime = DateTime.Today.AddHours(7),
                CloseTime = DateTime.Today.AddHours(20)
            };

            await _connection.InsertAsync(businessInfo);
        }

        public async Task AddAll()
        {
            await DeleteAll();

            await AddMyselfAsAdmin();
            await AddHolidayClosures();
            await AddBusinessInfo();
        }

        public async Task Init()
        {
            //Directory.CreateDirectory("C:\\Temp");

            Directory.CreateDirectory(FileSystem.AppDataDirectory);

            if (_connection is not null)
            {
                return;
            }

            _connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            await _connection.CreateTableAsync<ClosuresTable>();
            await _connection.CreateTableAsync<ContactUsInfoTable>();
            await _connection.CreateTableAsync<CourseNewsTable>();
            await _connection.CreateTableAsync<CredentialsTable>();
            await _connection.CreateTableAsync<GuestTable>();
            await _connection.CreateTableAsync<ReservationTable>();
            await _connection.CreateTableAsync<ScoreTable>();
            await _connection.CreateTableAsync<UserTable>();
        }

        public async Task DeleteAll()
        {
            await Init();

            await _connection.DropTableAsync<ClosuresTable>();
            await _connection.DropTableAsync<ContactUsInfoTable>();
            await _connection.DropTableAsync<CourseNewsTable>();
            await _connection.DropTableAsync<GuestTable>();
            await _connection.DropTableAsync<UserTable>();
            await _connection.DropTableAsync<ReservationTable>();
            await _connection.DropTableAsync<ScoreTable>();
            await _connection.DropTableAsync<CredentialsTable>();

            _connection = null;
        }

        public async Task<(int UserRowsAdded, UserTable UserRetrieved, int CredentialRowsAdded)> AddNewUser(UserModel user,
            string username, string password)
        {
            await Init();

            var passwordInfo = PasswordHasher.HashPassword(password);

            var userTableRowsAdded = await _connection.InsertAsync(new UserTable
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                StreetAddress = user.StreetAddress,
                AddressLine2 = user.AddressLine2,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode,
                Country = user.Country,
                DateOfBirth = user.DateOfBirth,
                IsAdmin = user.IsAdmin
            });

            

            var newlyCreatedUser = await GetUserByEmail(user.Email);

            var credentialTableRowsAdded = await _connection.InsertAsync(new CredentialsTable
            {
                UserId = newlyCreatedUser.Id,
                UserName = username,
                PasswordHash = passwordInfo.Hash,
                Salt = passwordInfo.Salt
            });

            return (userTableRowsAdded, newlyCreatedUser, credentialTableRowsAdded);
        }

        public async Task<int> DeleteUserByID(int userId)
        {
            await Init();

            return await _connection.DeleteAsync<UserTable>(userId);
        }

        public async Task<UserTable> GetUserById(int userId)
        {
            await Init();

            var query = await _connection.Table<UserTable>()
                                         .Where(x => x.Id == userId)
                                         .FirstOrDefaultAsync();
            return query;
        }

        public async Task<UserTable> GetUserByUsername(string username)
        {
            await Init();

            var usernameQuery = await _connection.Table<CredentialsTable>()
                                         .Where(x => x.UserName == username)
                                         .FirstOrDefaultAsync();

            if (usernameQuery is not null)
            {
                var query = await _connection.Table<UserTable>()
                                         .Where(x => x.Id == usernameQuery.UserId)
                                         .FirstOrDefaultAsync();

                return query;
            }

            return null;
        }

        public async Task<UserTable> GetUserByEmail(string email)
        {
            await Init();

            var query = await _connection.Table<UserTable>()
                                         .Where(x => x.Email == email)
                                         .FirstOrDefaultAsync();
            return query;
        }

        public async Task<(string? Hash, string? Salt, int UserId)> GetHashAndSalt(string username)
        {
            await Init();

            string hash;
            string salt;
            int userId;

            var query = await _connection.Table<CredentialsTable>()
                                         .Where(x => x.UserName == username)
                                         .FirstOrDefaultAsync();

            if (query is null)
            {
                return (null, null, -1);
            }

            hash = query.PasswordHash;
            salt = query.Salt;
            userId = query.UserId;

            return (hash, salt, userId);
        }

        public async Task<List<ReservationTable>> GetReservationsByDate(DateTime date)
        {
            await Init();

            var query = await _connection.Table<ReservationTable>()
                .Where(r => r.ReservationDate == date).ToListAsync();

            return query;
        }

        public async Task<ClosuresTable> GetClosuresByDate(DateTime date)
        {
            await Init();

            var query = await _connection.Table<ClosuresTable>()
                                         .Where(c => c.ClosureDate == date.Date)
                                         .FirstOrDefaultAsync();

            return query;
        }

        public async Task AddNewGuest(GuestModel guest)
        {
            await Init();

            await _connection.InsertAsync(new GuestTable
            {
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                PhoneNumber = guest.PhoneNumber
            });
        }

        public async Task<GuestTable> GetGuestByNameAndNumber(string firstName, string lastName, string phoneNumber)
        {
            await Init();

            var query = await _connection.Table<GuestTable>()
                                         .Where(g => g.FirstName == firstName && g.LastName == lastName && g.PhoneNumber == phoneNumber)
                                         .ToListAsync();

            return query.LastOrDefault();
        }

        public async Task<int> AddNewReservation(int id, bool isGuest, ReservationModel teeTime)
        {
            await Init();

            var rows = await _connection.InsertAsync(new ReservationTable
            {
                UserId = id,
                IsGuest = isGuest,
                ReservationDate = teeTime.Date.Date,
                ReservationTime = teeTime.Time,
                NumberOfPlayers = teeTime.NumberOfPlayers
            });

            return rows;
        }

        public async Task<List<ReservationTable>> FetchAllReservations()
        {
            await Init();

            var query = await _connection.Table<ReservationTable>()
                                         .OrderBy(r => r.ReservationDate)
                                         .ThenBy(r => r.ReservationTime)
                                         .ToListAsync();

            return query;
        }

        public async Task<int> ClearReservationsTable()
        {
            await Init();

            var retVal = await _connection.DeleteAllAsync<ReservationTable>();

            return retVal;
        }

        public async Task<List<UserTable>> FetchAllSignedUpUsers()
        {
            await Init();

            var query = await _connection.Table<UserTable>()
                                         .OrderBy(r => r.LastName)
                                         .ThenBy(r => r.FirstName) 
                                         .ToListAsync();

            return query;
        }

        public async Task<List<GuestTable>> FetchAllGuests()
        {
            await Init();

            var query = await _connection.Table<GuestTable>()
                                         .OrderBy(g => g.LastName)
                                         .ThenBy(g => g.FirstName)
                                         .ToListAsync();

            return query;
        }

        public async Task<int> AddCourseNews(CourseNewsTable news, ClosuresTable? closure = null)
        {
            await Init();

            if (closure != null)
            {
                await AddClosure(closure);

                var closureID = await GetLastClosureId();

                news.ClosureId = closureID;
            }

            return await _connection.InsertAsync(news);
        }

        public async Task AddClosure(ClosuresTable addedClosure)
        {
            await Init();

            await _connection.InsertAsync(addedClosure);
        }

        public async Task<int> GetLastClosureId()
        {
            await Init();

            var query = await _connection.Table<ClosuresTable>().ToListAsync();

            return query.LastOrDefault().Id;
        }

        public async Task<ContactUsInfoTable> FetchContactUsInfo()
        {
            await Init();

            var query = await _connection.Table<ContactUsInfoTable>().FirstOrDefaultAsync();

            return query;
        }

        public async Task<GuestTable> GetGuestById(int id)
        {
            await Init();

            var query = await _connection.Table<GuestTable>().Where(g => g.Id == id).FirstOrDefaultAsync();

            return query;
        }

        public async Task<List<CourseNewsTable>> GetCourseNews(int offset, int pageNumber)
        {
            await Init();

            var query = await _connection.Table<CourseNewsTable>().OrderByDescending(d => d.PostedDate).Skip(offset).Take(pageNumber).ToListAsync();

            return query;
        }

        public async Task<int> GetCourseNewsCount()
        {
            await Init();

            var query = await _connection.Table<CourseNewsTable>().CountAsync();

            return query;
        }

        public async Task<ContactUsInfoTable> GetContactInfo()
        {
            await Init();

            var query = await _connection.Table<ContactUsInfoTable>().FirstOrDefaultAsync();

            return query;
        }

        public async Task<List<ScoreTable>> GetUserScores(int userID)
        {
            await Init();

            var scoreList = await _connection.Table<ScoreTable>().Where(s => s.UserId == userID).ToListAsync();

            return scoreList;
        }

        public async Task<int> GetUserScoreTotal(int userID)
        {
            await Init();

            var total = await _connection.Table<ScoreTable>().Where(s => s.UserId == userID).ToListAsync();

            return total.Sum(s => s.Score);
        }

        public async Task<int> GetTotalNumberOfScoresEntered(int userID)
        {
            await Init();

            var count = await _connection.Table<ScoreTable>().Where( s => s.UserId == userID).CountAsync();

            return count;
        }

        public async Task AddScore(ScoreTable score)
        {
            await Init();

            await _connection.InsertAsync(score);
        }

        public async Task UpdateUserInformation(UserTable user)
        {
            await Init();

            await _connection.UpdateAsync(user);
        }
    }
}
