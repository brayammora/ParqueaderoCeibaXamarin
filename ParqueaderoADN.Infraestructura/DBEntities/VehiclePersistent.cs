using System;
using Realms;

namespace ParqueaderoADN.Infraestructura.DBEntities
{
    public class VehiclePersistent : RealmObject
    {
        public string Type { get; set; }
        public string NumberPlate { get; set; }
        public int CC { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
