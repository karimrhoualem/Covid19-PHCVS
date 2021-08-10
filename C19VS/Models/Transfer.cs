using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Models
{
    public class Transfer
    {
        public string transferID { get; set; }
        public string vacTypeID { get; set; }
        public string facilityID { get; set; }
        public string exportFacilityID { get; set; }
        public int numberVaccines { get; set; }
        public DateTime receptionDate { get; set; }
      

        public Transfer()
        {

        }

        public Transfer(object obj)
        {
            var transfer = obj as Transfer;

            this.transferID = transfer.transferID;
            this.vacTypeID = transfer.vacTypeID;
            this.facilityID = transfer.facilityID;
            this.exportFacilityID = transfer.exportFacilityID;
            this.numberVaccines = transfer.numberVaccines;
            this.receptionDate = transfer.receptionDate;
        }

        public Transfer(object[] obj)
        {
            transferID = obj[0] as string;
            vacTypeID = obj[1] as string;
            facilityID = obj[2] as string;
            exportFacilityID = obj[3] as string;
            numberVaccines = (int)obj[4];
            receptionDate = (DateTime)obj[5];
        }
    }
}
