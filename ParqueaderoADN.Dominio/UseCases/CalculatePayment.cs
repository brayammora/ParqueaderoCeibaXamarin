using System;
using ParqueaderoADN.Dominio.Entities;
using ParqueaderoADN.Dominio.Constants;

namespace ParqueaderoADN.Dominio.UseCases
{
    public class CalculatePayment
    {
        public static double GetPaymentValue(int days, int hours, Vehicle vehicle)
        {
            double totalCharge = 0;
            switch (vehicle.type.ToLower())
            {
                case nameof(InfoConstants.car):
                    totalCharge = (CarPrices.day * Convert.ToDouble(days)) + (CarPrices.hour * Convert.ToDouble(hours));
                    break;
                case nameof(InfoConstants.motorbike):
                    totalCharge = (MotorbikePrices.day * Convert.ToDouble(days)) + (MotorbikePrices.hour * Convert.ToDouble(hours));
                    if (vehicle.cc >= InfoConstants.ccLimitMotorbike)
                    {
                        totalCharge += MotorbikePrices.ccExtra;
                    }
                    break;
            }
            return totalCharge;
        }
    }
}
