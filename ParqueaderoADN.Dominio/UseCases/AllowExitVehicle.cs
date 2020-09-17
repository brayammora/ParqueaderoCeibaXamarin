using System;
using ParqueaderoADN.Dominio.Entities;
using ParqueaderoADN.Dominio.Port;

namespace ParqueaderoADN.Dominio.UseCases
{
    public class AllowExitVehicle
    {
        private readonly IVehiclesRepository vehiclesRepository;

        public AllowExitVehicle(IVehiclesRepository vehiclesRepository)
        {
            this.vehiclesRepository = vehiclesRepository;
        }

        public Response<string> Execute(Vehicle vehicle)
        {
            double totalCharge = GetPaymentValue(vehicle);
            string vehicleExit = "The cost of parking is: " + totalCharge + ".";
            vehiclesRepository.RemoveVehicle(vehicle);
            return new Response<string>(true, vehicleExit, null);
        }

        private double GetPaymentValue(Vehicle vehicle)
        {
            var daysHoursTuple = CalculateParkingTime.GetParkingTime(vehicle.date);
            int days = daysHoursTuple.Item1;
            int hours = daysHoursTuple.Item2;
            return CalculatePayment.GetPaymentValue(days, hours, vehicle);
        }
}
}
