namespace COMMON;

public class DateTimeHelper
{
    private static int TotalNum { get; set; } = 0;
    private static int Count { get; set; } = 0;
    private static DateTime BeginTime { get; set; }
    private static DateTime CurrentTime { get; set; }

    public static void StartPoint(int totalNum)
    {
        TotalNum = totalNum;
        BeginTime = DateTime.Now;
    }

    public static void StartPoint(int totalNum, int startNum)
    {
        TotalNum = totalNum - startNum;
        BeginTime = DateTime.Now;
    }

    public static void CalculatePointInLoop()
    {
        CurrentTime = DateTime.Now;
        Count++;

        TimeSpan timePassed = CurrentTime - BeginTime;
        long leftTotalSeconds = (int)Math.Round(timePassed.TotalSeconds / Count * (TotalNum - Count));
        TimeSpan timeLeft = new TimeSpan(TimeSpan.TicksPerSecond * leftTotalSeconds);

        Console.WriteLine($"Total Time Passed => " + FormatHMS(timePassed));
        Console.WriteLine($"Total Time Left   => " + FormatHMS(timeLeft) + Environment.NewLine);
    }

    public static string FormatHMS(string hour, string minute, string second)
    {
        return FormatTime(hour) + ":" + FormatTime(minute) + ":" + FormatTime(second);
    }

    public static string FormatHMS(int hour, int minute, int second)
    {
        return FormatTime(hour.ToString()) + ":" + FormatTime(minute.ToString()) + ":" + FormatTime(second.ToString());
    }

    public static string FormatHMS(TimeSpan time)
    {
        return FormatHMS(time.Hours, time.Minutes, time.Seconds);
    }

    public static string FormatTime(string time)
    {
        return time.Length < 2 ? $"0{time}" : time;
    }
}