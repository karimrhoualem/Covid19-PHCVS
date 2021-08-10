using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Models
{
    public class Dose
    {
        public string SSN { get; set; }
        public int doseNumber { get; set; }
        public DateTime doseDate { get; set; }
        public string vacTypeID { get; set; }
        public string facilityID { get; set; }
        public string EID { get; set; }

        public Dose()
        {

        }

        public Dose(object obj)
        {
            var dose = obj as Dose;

            this.SSN = dose.SSN;
            this.doseNumber = dose.doseNumber;
            this.doseDate = dose.doseDate;
            this.vacTypeID = dose.vacTypeID;
            this.facilityID = dose.facilityID;
            this.EID = dose.EID;
        
        }

        public Dose(object[] obj)
        {
            SSN = obj[0] as string;
            doseNumber = (int)obj[1]; // CASTING OK?
            doseDate = (DateTime)obj[2];
            vacTypeID = obj[3] as string;
            facilityID = obj[4] as string;
            EID = obj[5] as string;

        }
    }
}
