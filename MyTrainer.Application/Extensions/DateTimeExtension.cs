namespace MyTrainer.Application.Extensions;

public static class DateTimeExtension
{
    public static DateOnly ToDateOnly(this DateTime dateTime)
        => new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
}
