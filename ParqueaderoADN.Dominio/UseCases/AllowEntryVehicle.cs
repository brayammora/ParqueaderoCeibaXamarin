using System;
using ParqueaderoADN.Dominio.Entities;
using ParqueaderoADN.Dominio.Port;
using ParqueaderoADN.Dominio.Constants;

namespace ParqueaderoADN.Dominio.UseCases
{
    public class AllowEntryVehicle
    {
        private readonly IVehiclesRepository vehiclesRepository;
        private readonly string vehicleAdded = "Vehicle parked succesfully.";

        public AllowEntryVehicle(IVehiclesRepository vehiclesRepository)
        {
            this.vehiclesRepository = vehiclesRepository;
        }

        public Response<string> Execute(Vehicle vehicle)
        {
            if (IsAlreadyParked(vehicle.numberPlate))
            {
                return new Response<string>(false, null, AllowEntryVehicleErrors.itsAlreadyParked);
            }
            if (IsThereSiteAvaliableByVehicleType(vehicle.type))
            {
                return new Response<string>(false, null, AllowEntryVehicleErrors.parkIsFull);
            }
            if (CanNotEnterToday(vehicle.numberPlate, vehicle.date))
            {
                return new Response<string>(false, null, AllowEntryVehicleErrors.canNotEnterToday);
            }
            vehiclesRepository.AddVehicle(vehicle);
            return new Response<string>(true, vehicleAdded, null);
        }

        private bool IsAlreadyParked(string numberPlate)
        {
            return vehiclesRepository.FindVehicle(numberPlate);
        }

        private bool IsThereSiteAvaliableByVehicleType(string type)
        {
            int count = vehiclesRepository.GetCountByVehicleType(type);
            return (type == InfoConstants.car && count == InfoConstants.carLimit) ||
            (type == InfoConstants.motorbike && count == InfoConstants.motorbikeLimit);
        }

        private bool CanNotEnterToday(string numberPlate, DateTime date)
        {
            string today = date.ToString("dddd");
            return (today.ToLower().Equals(InfoConstants.sunday) ||
                today.ToLower().Equals(InfoConstants.monday)) &&
                (numberPlate[0].ToString()).Equals(InfoConstants.numberPlateStartsWithA);
        }
    }
}
