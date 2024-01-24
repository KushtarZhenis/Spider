using COMMON;
using MODEL;
using MODEL.Film;
using MODEL.FilmInfo;
using FilmSpider;
using HtmlAgilityPack;

string filmInfoDirectory = "/Users/kushtar/Desktop/Sources/Film/filmsInfo";

List<string> filePathList = FileHelper.GetAllFilePath(filmInfoDirectory);
int id = 1;

// foreach (string filePath in filePathList)
// {
string filePath = filePathList[id - 1];
string content = File.ReadAllText(filePath);
PartialFilm pFilm = JsonHelper.DeSerializeObject<PartialFilm>(content);




// }

