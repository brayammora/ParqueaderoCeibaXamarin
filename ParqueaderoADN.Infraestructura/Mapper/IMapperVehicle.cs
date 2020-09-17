using System;
using System.Collections.Generic;
using ParqueaderoADN.Dominio.Entities;
using ParqueaderoADN.Infraestructura.DBEntities;
using Realms;

namespace ParqueaderoADN.Infraestructura.Mapper
{
    public interface IMapperVehicle
    {
        static VehiclePersistent MapIntoVehiclePersistent(Vehicle vehicle) => throw new NotImplementedException();
        static Vehicle MapIntoVehicle(VehiclePersistent vehiclePersistent) => throw new NotImplementedException();
        static Vehicle[] MapVehicleResultSetToArray(IList<VehiclePersistent> vehicleResultSet) => throw new NotImplementedException();
    }
}
