using System;
using ParqueaderoADN.Dominio.Entities;

namespace ParqueaderoADN.Dominio.Prueba.DataBuilder
{
    public class VehicleTestDataBuilder
    {
        private string type;
        private string numberPlate;
        private int cc;
        private DateTime date;

        public VehicleTestDataBuilder()
        {
            type = "Default Value";
            numberPlate = "Default Value";
            cc = 0;
            date = new DateTime();
        }

        public void WithType(string type)
        {
            this.type = type;
        }

        public void WithNumberPlate(string numberPlate)
        {
            this.numberPlate = numberPlate;
        }

        public void WithCC(int cc)
        {
            this.cc = cc;
        }

        public void WithDate(DateTime date)
        {
            this.date = date;
        }

        public Vehicle Build()
        {
            return new Vehicle {
                type = type,
                numberPlate = numberPlate,
                cc = cc,
                date = date
            };
        }
    }
}
