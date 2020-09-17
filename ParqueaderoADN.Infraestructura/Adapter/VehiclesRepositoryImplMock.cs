using System;
using System.Collections.Generic;
using ParqueaderoADN.Dominio.Entities;
using ParqueaderoADN.Dominio.Port;

namespace ParqueaderoADN.Infraestructura.Adapter
{
    public class VehiclesRepositoryImplMock: IVehiclesRepository
    {
        private List<Vehicle> vehicles;

        public VehiclesRepositoryImplMock()
        {
            vehicles = new List<Vehicle>
            {
                new Vehicle
                {
                    type = "car",
                    numberPlate = "ABC123",
                    cc = 0,
                    date = new DateTime()
                }
            };
        }

        public void AddVehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
        }

        public bool FindVehicle(string numberPlate)
        {
            return vehicles.Exists(x => x.numberPlate == numberPlate);
        }

        public List<Vehicle> GetAllParkedVehicles()
        {
            return vehicles;
        }

        public int GetCountByVehicleType(string type)
        {
            List<Vehicle> vehiclesFiltered = vehicles.FindAll(x => x.type == type);
            return vehiclesFiltered.Count;
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            vehicles.RemoveAll(x => x.numberPlate == vehicle.numberPlate);
        }
    }
}
