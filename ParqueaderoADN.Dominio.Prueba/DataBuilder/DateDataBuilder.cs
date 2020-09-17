using System;
namespace ParqueaderoADN.Dominio.Prueba.DataBuilder
{
    public class DateDataBuilder
    {
        private DateTime date;

        public DateDataBuilder()
        {
            date = DateTime.Now;
        }

        public void WithYear(int year)
        {
            date.AddYears(year);
        }

        public void WithMonth(int month)
        {
            date = date.AddMonths(month);
        }

        public void WithDays(int days)
        {
            date -= TimeSpan.FromDays(days);
        }

        public void WithHours(int hours)
        {
            date -= TimeSpan.FromHours(hours);
        }

        public void WithMinutes(int minutes)
        {
            date -= TimeSpan.FromMinutes(minutes);
        }

        public void WithSeconds(int seconds)
        {
            date -= TimeSpan.FromMinutes(seconds);
        }

        public void WithMonday()
        {
            var year = 2020;
            var day = 24;
            var month = 8;
            date = new DateTime(year, month, day);
        }

        public void WithTuesday()
        {
            var year = 2020;
            var day = 25;
            var month = 8;
            date = new DateTime(year, month, day);
        }

        public DateTime Build()
        {
            return date;
        }
    }
}
