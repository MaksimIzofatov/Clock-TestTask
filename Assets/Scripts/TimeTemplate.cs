using JetBrains.Annotations;

public class TimeTemplate
{
    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;
    public int seconds;
    public int milliSeconds;
    public string dateTime;
    [CanBeNull] public string date;
    [CanBeNull] public string time;
    [CanBeNull] public string timeZone;
    public string dayOfWeek;
    public bool dstActive;
}
