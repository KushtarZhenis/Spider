namespace COMMON;

public class DateTimeHelper
{
    private static int TotalNum { get; set; } = 0;
    private static int Count { get; set; } = 0;
    private static DateTime BeginTime { get; set; }
    private static DateTime CurrentTime { get; set; }

    #region Start Point For Timer +StartPoint(int totalNum)
    public static void StartPoint(int totalNum)
    {
        TotalNum = totalNum;
        BeginTime = DateTime.Now;
    }
    #endregion

    #region Start Point For Timer +StartPoint(int totalNum, int startNum)
    public static void StartPoint(int totalNum, int startNum)
    {
        TotalNum = totalNum - startNum;
        BeginTime = DateTime.Now;
    }

    #endregion

    #region Calculate Point For Lopp +CalculatePointInLoop()
    public static void CalculatePointInLoop()
    {
        CurrentTime = DateTime.Now;
        Count++;

        TimeSpan timePassed = CurrentTime - BeginTime;
        long leftTotalSeconds = (int)Math.Round(timePassed.TotalSeconds / Count * (TotalNum - Count));
        TimeSpan timeLeft = new TimeSpan(TimeSpan.TicksPerSecond * leftTotalSeconds);

        double percent = Math.Round(100.00 * Count / TotalNum, 2);
        Console.WriteLine($"{Count} / {TotalNum} => {percent}");
        Console.WriteLine($"Total Time Passed => " + FormatHMS(timePassed));
        Console.WriteLine($"Total Time Left   => " + FormatHMS(timeLeft) + Environment.NewLine);
    }
    #endregion

    #region Format Hour Minute Second +FormatHMS(string hour, string minute, string second)
    public static string FormatHMS(string hour, string minute, string second)
    {
        return FormatTime(hour) + ":" + FormatTime(minute) + ":" + FormatTime(second);
    }
    #endregion

    #region Format Hour Minute Second +FormatHMS(int hour, int minute, int second)
    public static string FormatHMS(int hour, int minute, int second)
    {
        return FormatTime(hour.ToString()) + ":" + FormatTime(minute.ToString()) + ":" + FormatTime(second.ToString());
    }
    #endregion

    #region Format Hour Minute Second +FormatHMS(TimeSpan time)
    public static string FormatHMS(TimeSpan time)
    {
        return FormatHMS(time.Hours, time.Minutes, time.Seconds);
    }
    #endregion

    #region Format Time +FormatTime(string time)
    public static string FormatTime(string time)
    {
        return time.Length < 2 ? $"0{time}" : time;
    }
    #endregion

}