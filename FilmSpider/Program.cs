using COMMON;
using DBHelper;
using Dapper;
using FilmSpider;
using MODEL.Film;
using MODEL.JsonFilm;


SpiderSingleton.GetInstance.SetConnectionString("server=localhost;database=film_db;user=film_dba;password=12345678;charset=utf8mb4");
SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
SimpleCRUD.SetTableNameResolver(new SpiderResolver());


string filmInfoDirectory = "/Users/kushtar/Desktop/Sources/Film/filmsInfo";
int maxId = FileHelper.GetAllFilePath(filmInfoDirectory).Count;

using var _connection = Utilities.GetOpenConnection();
int id = 4307;

DateTimeHelper.StartPoint(maxId, id);

for (; id <= maxId; id++)
{
    string filePath = Path.Combine(filmInfoDirectory, $"film{id}.json");
    string content = File.ReadAllText(filePath);
    PartialFilm pFilm = JsonHelper.DeSerializeObject<PartialFilm>(content);
    int count = 0;
    count = _connection.Query<int>("select count(1) from film where title = @Title and description = @Description", new { Title = pFilm.Title, Description = pFilm.Description }).FirstOrDefault();
    DateTimeHelper.CalculatePointInLoop();

    if (count < 2)
    {
        try
        {
            FilmHelper.InsertFilm(pFilm, id, _connection);
        }
        catch (Exception ex)
        {
            FileHelper.AppendLine("/Users/kushtar/Desktop/Logs/Saving2DB.txt", ex.Message);
        }
    }
}

_connection.Close();

