using System;
using System.Globalization;

public class Date
{
    private DateTime date;

    // Конструктор
    public Date(int year, int month, int day)
    {
        date = new DateTime(year, month, day);
    }

    // Властивості
    public int Year
    {
        get { return date.Year; }
        set { date = new DateTime(value, date.Month, date.Day); }
    }

    public int Month
    {
        get { return date.Month; }
        set { date = new DateTime(date.Year, value, date.Day); }
    }

    public int Day
    {
        get { return date.Day; }
        set { date = new DateTime(date.Year, date.Month, value); }
    }

    // Сталі
    public static bool operator true(Date d) => d.date.Month == 1 && d.date.Day == 1;
    public static bool operator false(Date d) => !(d.date.Month == 1 && d.date.Day == 1);

    // Операція &
    public static bool operator &(Date d1, Date d2) => d1.date == d2.date;

    // Перетворення класу Date у тип string
    public static explicit operator string(Date d) => d.date.ToString("yyyy-MM-dd");

    // Перетворення типу string у клас Date
    public static explicit operator Date(string s) => new Date(DateTime.ParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture).Year, DateTime.ParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture).Month, DateTime.ParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture).Day);

    // Індексатор
    public Date this[int index]
    {
        get
        {
            return new Date(date.AddDays(index).Year, date.AddDays(index).Month, date.AddDays(index).Day);
        }
    }

    // Перевантаження операції !
    public static bool operator !(Date d) => d.date.Day != DateTime.DaysInMonth(d.date.Year, d.date.Month);

    // Вивід дати
    public void PrintDate()
    {
        Console.WriteLine($"Дата: {date:yyyy-MM-dd}");
    }
}

class Program
{
    static void Main()
    {
        Date date1 = new Date(2023, 1, 1);
        Date date2 = new Date(2023, 1, 2);

        // Вивід дати
        date1.PrintDate();

        // Перевірка на початок року
        if (date1)
        {
            Console.WriteLine("Ця дата є початком року.");
        }
        else
        {
            Console.WriteLine("Ця дата не є початком року.");
        }

        // Перевірка операції &
        bool areEqual = date1 & date2;
        Console.WriteLine($"Дати рівні: {areEqual}");

        // Перевірка перетворення до рядка і назад
        string dateString = (string)date1;
        Console.WriteLine($"Дата як рядок: {dateString}");
        Date dateFromString = (Date)dateString;
        dateFromString.PrintDate();

        // Перевірка індексатора
        Date dateAfterTenDays = date1[10];
        Console.WriteLine($"Дата через 10 днів: {(string)dateAfterTenDays}");
        Date dateBeforeTenDays = date1[-10];
        Console.WriteLine($"Дата 10 днів назад: {(string)dateBeforeTenDays}");

        // Перевірка операції !
        bool isNotLastDay = !date1;
        Console.WriteLine($"Дата не є останнім днем місяця: {isNotLastDay}");
    }
}
