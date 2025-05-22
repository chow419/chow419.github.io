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
            Directory.CreateDirectory(FileSystem.AppDataDirectory);

            _connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            _connection.CreateTableAsync<GuestTable>();
            _connection.CreateTableAsync<UserTable>();
            _connection.CreateTableAsync<ReservationTable>();
            _connection.CreateTableAsync<ScoreTable>();
        }

        public async Task AddMyselfAsAdmin()
        {
            var query = _connection.Table<UserTable>().FirstOrDefaultAsync();

            if (query is not null)
            {
                return;
            }

            var passwordInfo = PasswordHasher.HashPassword("T#eD@rkUrg3");

            UserModel cameron = new()
            {
                FirstName = "Cameron",
                LastName = "Howard",
                PhoneNumber = "208-841-2881",
                Email = "socialenigma11@gmail.com",
                StreetAddress = "511 McMillan St., Apt. A",
                City = "Winnsboro",
                State = "TX",
                ZipCode = "75494",
                Country = "United States",
                DateOfBirth = DateTime.ParseExact("1998-07-19", "yyyy-MM-dd", null),
                IsAdmin = true
            };

            await AddNewUser(cameron, "socialenigma11", "T#eD@rkUrg3");
        }

        public async Task AddAll()
        {
            await AddMyselfAsAdmin();
        }

        public async Task AddNewUser(UserModel user, string username, string password)
        {
            var passwordInfo = PasswordHasher.HashPassword(password);

            await _connection.InsertAsync(new UserTable
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                StreetAddress = user.StreetAddress,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode,
                Country = user.Country,
                DateOfBirth = user.DateOfBirth
            });

            var newlyCreateUser = await GetUserByUsername(username);

            await _connection.InsertAsync(new CredentialsTable
            {
                UserId = newlyCreateUser.Id,
                UserName = username,
                PasswordHash = passwordInfo.Hash,
                Salt = passwordInfo.Salt
            });
        }

        public async Task<UserTable> GetUserById(int userId)
        {
            var query = await _connection.Table<UserTable>()
                                         .Where(x => x.Id == userId)
                                         .FirstOrDefaultAsync();
            return query;
        }

        public async Task<UserTable> GetUserByUsername(string username)
        {
            var usernameQuery = await _connection.Table<CredentialsTable>()
                                         .Where(x => x.UserName == username)
                                         .FirstOrDefaultAsync();

            var query = await _connection.Table<UserTable>()
                                         .Where(x => x.Id == usernameQuery.UserId)
                                         .FirstOrDefaultAsync();
            return query;
        }

        public async Task<(string Hash, string Salt, int UserId)> GetHashAndSalt(string username)
        {
            string hash;
            string salt;
            int userId;

            var query = await _connection.Table<CredentialsTable>()
                                         .Where(x => x.UserName == username)
                                         .FirstOrDefaultAsync();

            hash = query.PasswordHash;
            salt = query.Salt;
            userId = query.UserId;

            return (hash, salt, userId);
        }

    }
}
