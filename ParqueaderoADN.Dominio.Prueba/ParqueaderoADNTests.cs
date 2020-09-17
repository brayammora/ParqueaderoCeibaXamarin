using Xunit;
using ParqueaderoADN.Dominio.Prueba.DataBuilder;
using ParqueaderoADN.Infraestructura.Adapter;
using ParqueaderoADN.Dominio.UseCases;
using ParqueaderoADN.Dominio.Entities;
using System.Collections.Generic;
using ParqueaderoADN.Dominio.Constants;

namespace ParqueaderoADN.Dominio.Prueba.ParqueaderoADNTests
{
    public class ParqueaderoADNTests
    {
        private VehicleTestDataBuilder vehicleDataBuilder;
        private DateDataBuilder dateDataBuilder;
        private VehiclesRepositoryImplMock mockRepository;
        private GetAllParkedVehicles getAllParkedVehicles;
        private AllowEntryVehicle allowEntryVehicle;

        public ParqueaderoADNTests()
        {
            vehicleDataBuilder = new VehicleTestDataBuilder();
            dateDataBuilder = new DateDataBuilder();
            mockRepository = new VehiclesRepositoryImplMock();
            getAllParkedVehicles = new GetAllParkedVehicles(mockRepository);
            allowEntryVehicle = new AllowEntryVehicle(mockRepository);
        }

        [Fact]
        public void TestCanGetAllParkedVehiclesWith1VehicleInDataBase()
        {
            // Arrange
            int vehiclesCount = 0;   
            // Act
            Response<List<Vehicle>> response = getAllParkedVehicles.Execute();
            if (response.status)
            {
                vehiclesCount += response.data.Count;
            }
            // Assert
            Assert.Equal(1, vehiclesCount);
        }

        [Fact]
        public void TestCanPark()
        {
            // Arrange
            vehicleDataBuilder.WithNumberPlate("BBB222");
            dateDataBuilder.WithTuesday();
            vehicleDataBuilder.WithDate(dateDataBuilder.Build());
            Vehicle vehicle = vehicleDataBuilder.Build();
            string resultMessage = "";
            // Act
            Response<string> response = allowEntryVehicle.Execute(vehicle);
            if (response.status)
            {
                resultMessage = response.data;
            }
            // Assert
            Assert.Equal("Vehicle parked succesfully.", resultMessage);
        }

        [Fact]
        public void TestIsAlreadyParked()
        {
            // Arrange
            vehicleDataBuilder.WithNumberPlate("ABC123");
            Vehicle vehicle = vehicleDataBuilder.Build();
            string errorType = AllowEntryVehicleErrors.itsAlreadyParked;
            // Act
            Response<string> response = allowEntryVehicle.Execute(vehicle);
            if (!response.status)
            {
                errorType = response.error;
            }
            // Assert
            Assert.Equal(errorType, AllowEntryVehicleErrors.itsAlreadyParked);
        }

        [Fact]
        public void TestThereIsNoSiteAvaliableWithCarTypeFull()
        {
            // Arrange
            string errorType = "";
            Vehicle vehicle = new Vehicle();
            // Act
            for (int i = 0; i < 21; i++)
            {
                vehicleDataBuilder.WithNumberPlate("QWE321" + i);
                vehicleDataBuilder.WithType(InfoConstants.car);
                vehicle = vehicleDataBuilder.Build();
                allowEntryVehicle.Execute(vehicle);
            }
            Response<string> response = allowEntryVehicle.Execute(vehicle);
            if (!response.status)
            {
                errorType = response.error;
            }
            // Assert
            Assert.Equal(errorType, AllowEntryVehicleErrors.parkIsFull);
        }

        [Fact]
        public void TestThereIsNoSiteAvaliableWithMotorbikeTypeFull()
        {
            // Arrange
            string errorType = "";
            Vehicle vehicle = new Vehicle();
            // Act
            for (int i = 0; i < 11; i++)
            {
                vehicleDataBuilder.WithNumberPlate("QWE321" + i);
                vehicleDataBuilder.WithType(InfoConstants.motorbike);
                vehicle = vehicleDataBuilder.Build();
                allowEntryVehicle.Execute(vehicle);
            }
            Response<string> response = allowEntryVehicle.Execute(vehicle);
            if (!response.status)
            {
                errorType = response.error;
            }
            // Assert
            Assert.Equal(errorType, AllowEntryVehicleErrors.parkIsFull);
        }

        [Fact]
        public void TestCanNotEnterWhenIsMondayAndNumberPlateStartsWithA()
        {
            //Arrange
            vehicleDataBuilder.WithNumberPlate("AAA111");
            dateDataBuilder.WithMonday();
            vehicleDataBuilder.WithDate(dateDataBuilder.Build());
            Vehicle vehicle = vehicleDataBuilder.Build();
            string errorType = "";
            // Act
            Response<string> response = allowEntryVehicle.Execute(vehicle);
            if (!response.status)
            {
                errorType = response.error;
            }
            // Assert
            Assert.Equal(errorType, AllowEntryVehicleErrors.canNotEnterToday);
        }

        /* La tabla de precios es la siguiente:
            Valor hora carro = 1000
            Valor hora moto = 500
            Valor día carro = 8000
            Valor día moto = 4000
            Moto con CC mayor a 500 = valor total + 2000
        */

        [Fact]
        public void TestCalculateParkingTimeWithLessThan9Hours()
        {
            //Arrange
            dateDataBuilder.WithDays(0);
            dateDataBuilder.WithHours(4);
            dateDataBuilder.WithMinutes(30);
            dateDataBuilder.WithSeconds(0);
            vehicleDataBuilder.WithDate(dateDataBuilder.Build());
            Vehicle vehicle = vehicleDataBuilder.Build();
            //Act
            var daysHoursTuple = CalculateParkingTime.GetParkingTime(vehicle.date);
            int days = daysHoursTuple.Item1;
            int hours = daysHoursTuple.Item2;
            //Assert
            Assert.Equal(0, days);
            Assert.Equal(5, hours);
        }

        [Fact]
        public void TestCalculateParkingTimeWithMoreThan9HoursAndLessThan24Hours()
        {
            //Arrange
            dateDataBuilder.WithDays(0);
            dateDataBuilder.WithHours(15);
            dateDataBuilder.WithMinutes(30);
            vehicleDataBuilder.WithDate(dateDataBuilder.Build());
            Vehicle vehicle = vehicleDataBuilder.Build();
            //Act
            var daysHoursTuple = CalculateParkingTime.GetParkingTime(vehicle.date);
            int days = daysHoursTuple.Item1;
            int hours = daysHoursTuple.Item2;
            //Assert
            Assert.Equal(1, days);
            Assert.Equal(0, hours);
        }

        [Fact]
        public void TestCalculateParkingTimeWith15DaysAnd2Hours()
        {
            //Arrange
            dateDataBuilder.WithDays(15);
            dateDataBuilder.WithHours(1);
            dateDataBuilder.WithMinutes(30);
            vehicleDataBuilder.WithDate(dateDataBuilder.Build());
            Vehicle vehicle = vehicleDataBuilder.Build();
            //Act
            var daysHoursTuple = CalculateParkingTime.GetParkingTime(vehicle.date);
            int days = daysHoursTuple.Item1;
            int hours = daysHoursTuple.Item2;
            //Assert
            Assert.Equal(15, days);
            Assert.Equal(2, hours);
        }

        [Fact]
        public void TestCalculatePaymentCarWith4Hours()
        {
            //Arrange
            dateDataBuilder.WithDays(0);
            dateDataBuilder.WithHours(3);
            dateDataBuilder.WithMinutes(25);
            vehicleDataBuilder.WithDate(dateDataBuilder.Build());
            vehicleDataBuilder.WithType(InfoConstants.car);
            Vehicle vehicle = vehicleDataBuilder.Build();
            //Act
            var daysHoursTuple = CalculateParkingTime.GetParkingTime(vehicle.date);
            int days = daysHoursTuple.Item1;
            int hours = daysHoursTuple.Item2;
            var totalCharge = CalculatePayment.GetPaymentValue(days, hours, vehicle);
            //Assert
            Assert.Equal(4000, totalCharge);
        }

        [Fact]
        public void TestCalculatePaymentCarWithMoreThan9HoursAndLessThan24Hours()
        {
            //Arrange
            dateDataBuilder.WithDays(0);
            dateDataBuilder.WithHours(13);
            dateDataBuilder.WithMinutes(25);
            vehicleDataBuilder.WithDate(dateDataBuilder.Build());
            vehicleDataBuilder.WithType(InfoConstants.car);
            Vehicle vehicle = vehicleDataBuilder.Build();
            //Act
            var daysHoursTuple = CalculateParkingTime.GetParkingTime(vehicle.date);
            int days = daysHoursTuple.Item1;
            int hours = daysHoursTuple.Item2;
            var totalCharge = CalculatePayment.GetPaymentValue(days, hours, vehicle);
            //Assert
            Assert.Equal(8000, totalCharge);
        }

        [Fact]
        public void TestCalculatePaymentCarWith3daysAnd2Hours()
        {
            //Arrange
            dateDataBuilder.WithDays(3);
            dateDataBuilder.WithHours(1);
            dateDataBuilder.WithMinutes(15);
            vehicleDataBuilder.WithDate(dateDataBuilder.Build());
            vehicleDataBuilder.WithType(InfoConstants.car);
            Vehicle vehicle = vehicleDataBuilder.Build();
            //Act
            var daysHoursTuple = CalculateParkingTime.GetParkingTime(vehicle.date);
            int days = daysHoursTuple.Item1;
            int hours = daysHoursTuple.Item2;
            var totalCharge = CalculatePayment.GetPaymentValue(days, hours, vehicle);
            //Assert
            Assert.Equal(26000, totalCharge);
        }

        [Fact]
        public void TestCalculatePaymentMotorbikeWith4Hours()
        {
            //Arrange
            dateDataBuilder.WithDays(0);
            dateDataBuilder.WithHours(3);
            dateDataBuilder.WithMinutes(25);
            vehicleDataBuilder.WithDate(dateDataBuilder.Build());
            vehicleDataBuilder.WithType(InfoConstants.motorbike);
            Vehicle vehicle = vehicleDataBuilder.Build();
            //Act
            var daysHoursTuple = CalculateParkingTime.GetParkingTime(vehicle.date);
            int days = daysHoursTuple.Item1;
            int hours = daysHoursTuple.Item2;
            var totalCharge = CalculatePayment.GetPaymentValue(days, hours, vehicle);
            //Assert
            Assert.Equal(2000, totalCharge);
        }

        [Fact]
        public void TestCalculatePaymentMotorbikeWithMoreThan9HoursAndLessThan24Hours()
        {
            //Arrange
            dateDataBuilder.WithDays(0);
            dateDataBuilder.WithHours(13);
            dateDataBuilder.WithMinutes(25);
            vehicleDataBuilder.WithDate(dateDataBuilder.Build());
            vehicleDataBuilder.WithType(InfoConstants.motorbike);
            Vehicle vehicle = vehicleDataBuilder.Build();
            //Act
            var daysHoursTuple = CalculateParkingTime.GetParkingTime(vehicle.date);
            int days = daysHoursTuple.Item1;
            int hours = daysHoursTuple.Item2;
            var totalCharge = CalculatePayment.GetPaymentValue(days, hours, vehicle);
            //Assert
            Assert.Equal(4000, totalCharge);
        }

        [Fact]
        public void TestCalculatePaymentMotorbikeWith3daysAnd2Hours()
        {
            //Arrange
            dateDataBuilder.WithDays(3);
            dateDataBuilder.WithHours(1);
            dateDataBuilder.WithMinutes(15);
            vehicleDataBuilder.WithDate(dateDataBuilder.Build());
            vehicleDataBuilder.WithType(InfoConstants.motorbike);
            Vehicle vehicle = vehicleDataBuilder.Build();
            //Act
            var daysHoursTuple = CalculateParkingTime.GetParkingTime(vehicle.date);
            int days = daysHoursTuple.Item1;
            int hours = daysHoursTuple.Item2;
            var totalCharge = CalculatePayment.GetPaymentValue(days, hours, vehicle);
            //Assert
            Assert.Equal(13000, totalCharge);
        }

        [Fact]
        public void TestCalculatePaymentMotorbikeWith1DayAndExtraCC()
        {
            //Arrange
            dateDataBuilder.WithDays(1);
            dateDataBuilder.WithHours(0);
            dateDataBuilder.WithMinutes(0);
            dateDataBuilder.WithSeconds(0);
            vehicleDataBuilder.WithDate(dateDataBuilder.Build());
            vehicleDataBuilder.WithType(InfoConstants.motorbike);
            vehicleDataBuilder.WithCC(700);
            Vehicle vehicle = vehicleDataBuilder.Build();
            //Act
            var daysHoursTuple = CalculateParkingTime.GetParkingTime(vehicle.date);
            int days = daysHoursTuple.Item1;
            int hours = daysHoursTuple.Item2;
            var totalCharge = CalculatePayment.GetPaymentValue(days, hours, vehicle);
            //Assert
            Assert.Equal(6000, totalCharge);
        }
    }
}