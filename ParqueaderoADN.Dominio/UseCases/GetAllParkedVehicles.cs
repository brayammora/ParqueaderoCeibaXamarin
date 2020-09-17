using System;
using System.Collections.Generic;
using ParqueaderoADN.Dominio.Constants;
using ParqueaderoADN.Dominio.Entities;
using ParqueaderoADN.Dominio.Port;

namespace ParqueaderoADN.Dominio.UseCases
{
    public class GetAllParkedVehicles
    {
        private readonly IVehiclesRepository vehiclesRepository;

        public GetAllParkedVehicles (IVehiclesRepository vehiclesRepository)
        {
            this.vehiclesRepository = vehiclesRepository;
        }

        public Response<List<Vehicle>> Execute()
        {
            var vehicles = vehiclesRepository.GetAllParkedVehicles();

            if(vehicles.Count > 0)
            {
                return new Response<List<Vehicle>>(true, vehicles, null);
            } else
            {
                return new Response<List<Vehicle>>(false, null, GetAllParkedVehiclesErrors.noDataAvaliable);
            }
        }
    }
}
