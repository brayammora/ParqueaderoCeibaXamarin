using System;
using ParqueaderoADN.Dominio.Constants;

namespace ParqueaderoADN.Dominio.UseCases
{
    public struct CalculateParkingTime
    {
        public static Tuple<int, int> GetParkingTime(DateTime vehicleEnterDate)
        {
            var days = (DateTime.Now - vehicleEnterDate).Days;
            var hours = (DateTime.Now - vehicleEnterDate).Hours;
            var minutes = (DateTime.Now - vehicleEnterDate).Minutes;
            var seconds = (DateTime.Now - vehicleEnterDate).Seconds;

            if (seconds != InfoConstants.zeroSeconds)
            {
                minutes += 1;
            }
            if (minutes != InfoConstants.zeroMinutes)
            {
                hours += 1;
            }
            if (hours > InfoConstants.maxHoursPerDay)
            {
                days += 1;
                hours = 0;
            }

            return new Tuple<int, int>(days, hours);
        }
    }
}
