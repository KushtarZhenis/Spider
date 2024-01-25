using System.Reflection;
using Dapper;

namespace DBHelper;

public class SpiderResolver : SimpleCRUD.ITableNameResolver
{
    public string ResolveTableName(Type type)
    {
        return type.Name.ToLower();
    }
}