using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Models
{
    public class Vaccine
    {
        public string vacTypeID { get; set; }
        public string vacName { get; set; }
        public int dosesNeeded { get; set; }
        public string approvalStatus { get; set; }
        public DateTime approvalDate { get; set; }
        public DateTime suspensionDate { get; set; }

        public Vaccine()
        {

        }

        public Vaccine(object obj)
        {
            var vaccine = obj as Vaccine;

            this.vacTypeID = vaccine.vacTypeID;
            this.vacName = vaccine.vacName;
            this.dosesNeeded = vaccine.dosesNeeded;
            this.approvalStatus = vaccine.approvalStatus;
            this.approvalDate = vaccine.approvalDate;
            this.suspensionDate = vaccine.suspensionDate;
        
        }

        public Vaccine(object[] obj)
        {
            vacTypeID = obj[0] as string;
            vacName = obj[1] as string;
            dosesNeeded = (int)obj[2];
            approvalStatus = obj[3] as string;
            approvalDate = (DateTime)obj[4];
            suspensionDate = (DateTime)obj[5];

        }
    }
}
