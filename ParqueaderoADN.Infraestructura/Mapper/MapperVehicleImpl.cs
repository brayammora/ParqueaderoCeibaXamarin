using System;
using System.Collections.Generic;
using ParqueaderoADN.Dominio.Entities;
using ParqueaderoADN.Infraestructura.DBEntities;

namespace ParqueaderoADN.Infraestructura.Mapper
{
    public class MapperVehicleImpl: IMapperVehicle
    {
        public static Vehicle MapIntoVehicle(VehiclePersistent vehiclePersistent)
        {
            Vehicle vehicle = new Vehicle
            {
                type = vehiclePersistent.Type,
                numberPlate = vehiclePersistent.NumberPlate,
                cc = vehiclePersistent.CC,
                date = vehiclePersistent.Date.DateTime
            };
            return vehicle;
        }

        public static VehiclePersistent MapIntoVehiclePersistent(Vehicle vehicle)
        {
            VehiclePersistent vehicleP = new VehiclePersistent
            {
                Type = vehicle.type,
                NumberPlate = vehicle.numberPlate,
                CC = vehicle.cc,
                Date = vehicle.date
            };
            return vehicleP;
        }

        public static List<Vehicle> MapVehicleResultSetToArray(IList<VehiclePersistent> vehicleResultSet)
        {
            var vehicleList = new List<Vehicle>();
            foreach (var vPersistent in vehicleResultSet) {
                vehicleList.Add(MapIntoVehicle(vPersistent));
            }
            return vehicleList;
        }
    }
}
