using System;
using System.Collections.Generic;
using ParqueaderoADN.Dominio.Entities;

namespace ParqueaderoADN.Dominio.Port
{
    public interface IVehiclesRepository
    {
        List<Vehicle> GetAllParkedVehicles();
        void AddVehicle(Vehicle vehicle);
        bool FindVehicle(string numberPlate);
        int GetCountByVehicleType(string type);
        void RemoveVehicle(Vehicle vehicle);
    }
}
