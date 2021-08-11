using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Models
{
    public class Shipment
    {
        public string shipmentID { get; set; }
        public string VacTypeID { get; set; }
        public string facilityID { get; set; }
        public DateTime receptionDate { get; set; }
        public int numberVaccine { get; set; }

        public Shipment()
        {

        }

        public Shipment(object obj)
        {
            var shipment = obj as Shipment;

            this.shipmentID = shipment.shipmentID;
            this.VacTypeID = shipment.VacTypeID;
            this.facilityID = shipment.facilityID;
            this.receptionDate = shipment.receptionDate;
            this.numberVaccine = shipment.numberVaccine;
        }

        public Shipment(object[] obj)
        {
            this.shipmentID = obj[0] as string;
            this.VacTypeID = obj[1] as string;
            this.facilityID = obj[2] as string;
            this.receptionDate = (DateTime)obj[3];
            this.numberVaccine = (int)obj[4];
        }
    }
}
