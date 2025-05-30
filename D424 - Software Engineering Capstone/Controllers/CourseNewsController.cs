using D424___Software_Engineering_Capstone.Database;
using D424___Software_Engineering_Capstone.Models;

namespace D424___Software_Engineering_Capstone.Controllers
{
    public class CourseNewsController
    {
        public DatabaseHandler _database { get; set; }

        public CourseNewsController()
        {
            _database = new DatabaseHandler();
        }

        public async Task<List<CourseNewsModel>> GetCourseNews(int offset, int pageSize)
        {
            var queryResults = await _database.GetCourseNews(offset, pageSize);

            List<CourseNewsModel> result = new List<CourseNewsModel>();

            if (queryResults is not null && queryResults.Count > 0)
            {
                foreach (var newsItem in queryResults)
                {
                    var courseNews = new CourseNewsModel()
                    {
                        Title = newsItem.Title,
                        NewsDetails = newsItem.NewsDetails,
                        PostedDate = newsItem.PostedDate
                    };

                    result.Add(courseNews);
                }
            }

            return result;
        }

        public async Task<int> GetCourseNewsItemsCount()
        {
            var result = await _database.GetCourseNewsCount();

            return result;
        }
    }
}
