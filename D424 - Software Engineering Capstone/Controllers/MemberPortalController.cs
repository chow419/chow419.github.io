using D424___Software_Engineering_Capstone.Database;
using D424___Software_Engineering_Capstone.Database.Tables;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class MemberPortalController
    {
        public DatabaseHandler _database { get; set; }


        public MemberPortalController()
        {
            _database = new();
        }

        private async Task<int> GetUserID(UserModel user)
        {
            var userInfo = await _database.GetUserByEmail(user.Email);

            return userInfo.Id;
        }

        public async Task<List<ScoreModel>> FetchUserScores(UserModel currentUser)
        {
            var userID = await GetUserID(currentUser);

            var result = await _database.GetUserScores(userID);

            List<ScoreModel> scores = new();

            foreach (var score in result)
            {
                ScoreModel scoreModel = new ScoreModel()
                {
                    Date = score.Date,
                    Score = score.Score
                };
                
                scores.Add(scoreModel);
            }

            return scores;
        }

        public async Task<double> CalculateScoreAverage(UserModel currentUser)
        {
            var userID = await GetUserID(currentUser);

            var scoreTotal = await _database.GetUserScoreTotal(userID);
            var totalScores = await _database.GetTotalNumberOfScoresEntered(userID);

            if (totalScores > 0)
            {
                return (double)scoreTotal / (double)totalScores;
            }

            return 0.00;
        }

        public async Task AddRoundScore(UserModel currentUser, ScoreModel score)
        {
            var userID = await GetUserID(currentUser);

            var scoreTable = new ScoreTable()
            {
                UserId = userID,
                Date = score.Date,
                Score = score.Score
            };

            await _database.AddScore(scoreTable);
        }

        public async Task UpdateUserInformation(UserModel currentUser)
        {
            UserTable userTable = await _database.GetUserByEmail(currentUser.Email);

            userTable.StreetAddress = currentUser.StreetAddress;
            userTable.AddressLine2 = currentUser.AddressLine2;
            userTable.City = currentUser.City;
            userTable.Country = currentUser.Country;
            userTable.State = currentUser.State;
            userTable.ZipCode = currentUser.ZipCode;
            userTable.PhoneNumber = currentUser.PhoneNumber;

            await _database.UpdateUserInformation(userTable);
        }
    }
}
