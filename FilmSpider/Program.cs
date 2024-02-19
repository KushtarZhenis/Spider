using COMMON;
using DBHelper;
using Dapper;
using MODEL.Film;
using MODEL.JsonFilm;


SiteSingleton.GetInstance.SetConnectionString("server=localhost;database=film_db;user=film_dba;password=12345678;charset=utf8mb4");
SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
SimpleCRUD.SetTableNameResolver(new SpiderResolver());



