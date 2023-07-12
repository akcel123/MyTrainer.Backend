using MyTrainer.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrainer.Application.Extensions;

public static class DateTimeExtension
{
    public static DateOnly ToDateOnly(this DateTime dateTime)
        => new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
}
