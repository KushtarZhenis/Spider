using AngleSharp.Text;

namespace COMMON;

public class PathHelper
{
    #region Combine 2 Paths +Combine(string path1, string path2)
    public static string Combine(string path1, string path2)
    {
        if (path1.EndsWith('/'))
        {
            for (int index = path1.Length - 1; index > 0; index--)
            {
                if (path1[index] != '/')
                {
                    path1 = path1[..(index + 1)];
                    break;
                }
            }
        }

        if (path2.StartsWith('/'))
        {
            for (int index = 1; index < path2.Length; index++)
            {
                if (path2[index] != '/')
                {
                    return path1 + path2[(index - 1)..];
                }
            }
        }

        return path1 + '/' + path2;
    }
    #endregion

    #region Combine 3 Paths +Combine(string path1, string path2, string path3)
    public static string Combine(string path1, string path2, string path3)
    {
        return Combine(path1, Combine(path2, path3));
    }
    #endregion

    #region Combine 4 Paths +Combine(string path1, string path2, string path3, string path4)
    public static string Combine(string path1, string path2, string path3, string path4)
    {
        return Combine(path1, Combine(path2, path3, path4));
    }
    #endregion

    #region Combine Array Of Paths +Combine(string[] paths)
    public static string Combine(string[] paths)
    {
        string res = paths[0];

        for (int i = 1; i < paths.Length; i++)
        {
            res = Combine(res, paths[i]);
        }

        return res;
    }
    #endregion

}
