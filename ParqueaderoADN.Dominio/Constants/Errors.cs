using System;
namespace ParqueaderoADN.Dominio.Constants
{
    public static class GetAllParkedVehiclesErrors
    {
        public static string
            noDataAvaliable = "There is no information available to show.";
    }

    public static class AllowEntryVehicleErrors
    {
        public static string
            itsAlreadyParked = "The vehicle you are trying to enter is already parked.",
            parkIsFull = "The park is full. The vehicle can't enter.",
            canNotEnterToday = "The vehicle can't enter today because number plate not is permitted on monday and sunday.",
            vehicleCantAdded = "The vehicle can't added to database, please try later.";
    }
}
