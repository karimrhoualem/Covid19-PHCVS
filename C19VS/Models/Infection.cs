using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Models
{
    public class Infection
    {
        public string SSN { get; set; }
        public DateTime infectionDate { get; set; }
        public string variantID { get; set; }

        public Infection()
        {

        }

        public Infection(object obj)
        {
            var infection = obj as Infection;

            this.SSN = infection.SSN;
            this.infectionDate = infection.infectionDate;
            this.variantID = infection.variantID;
        }

        public Infection(object[] obj)
        {
            SSN = obj[0] as string;
            infectionDate = (DateTime)obj[1];
            variantID = obj[2] as string;

        }
    }
}
