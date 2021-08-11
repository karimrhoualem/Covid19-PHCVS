using System;
namespace C19VS.Models
{
    public class Inventory
    {
        public string inventoryID { get; set; }
        public string facilityID { get; set; }
        public string shipmentID { get; set; }
        public string vacTypeID { get; set; }
        public string transferID { get; set; }
        public int numberVaccine { get; set; }

        public Inventory()
        {

        }

        public Inventory(object obj)
        {
            var Inventory = obj as Inventory;

            this.inventoryID = Inventory.inventoryID;
            this.facilityID = Inventory.facilityID;
            this.shipmentID = Inventory.shipmentID;
            this.vacTypeID = Inventory.vacTypeID;
            this.transferID = Inventory.transferID;
            this.numberVaccine = Inventory.numberVaccine;
        }

        public Inventory(object[] obj)
        {
            inventoryID = obj[0] as string;
            facilityID = obj[1] as string;
            shipmentID = obj[2] as string;
            vacTypeID = obj[3] as string;
            transferID = obj[4] as string;
            numberVaccine = (int)obj[5];
        }

    }
}
