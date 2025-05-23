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

            CreateTables();
        }

        public async Task AddMyselfAsAdmin()
        {
            var query = await _connection.Table<UserTable>().FirstOrDefaultAsync();

            if (query is not null)
            {
                return;
            }

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
            //await DeleteAll();

            await AddMyselfAsAdmin();
        }

        public async void CreateTables()
        {
            await _connection.CreateTableAsync<GuestTable>();
            await _connection.CreateTableAsync<UserTable>();
            await _connection.CreateTableAsync<ReservationTable>();
            await _connection.CreateTableAsync<ScoreTable>();
            await _connection.CreateTableAsync<CredentialsTable>();
        }

        public async Task DeleteAll()
        {
            await _connection.DeleteAllAsync<GuestTable>();
            await _connection.DeleteAllAsync<UserTable>();
            await _connection.DeleteAllAsync<ReservationTable>();
            await _connection.DeleteAllAsync<ScoreTable>();
            await _connection.DeleteAllAsync<CredentialsTable>();
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

            

            var newlyCreateUser = await GetUserByEmail(user.Email);

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

        public async Task<UserTable> GetUserByEmail(string email)
        {
            var query = await _connection.Table<UserTable>()
                                         .Where(x => x.Email == email)
                                         .FirstOrDefaultAsync();
            return query;
        }

        public async Task<(string? Hash, string? Salt, int UserId)> GetHashAndSalt(string username)
        {
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

    }
}
