using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Models
{
    public class Employment
    {
        public string contractNum { get; set; }
        public string EID { get; set; }
        public string facilityID { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public Employment()
        {

        }

        public Employment(object obj)
        {
            var employment = obj as Employment;

            this.contractNum = employment.contractNum;
            this.EID = employment.EID;
            this.facilityID = employment.facilityID;
            this.startDate = employment.startDate;
            this.endDate = employment.endDate;
        }

        public Employment(object[] obj)
        {
            contractNum = obj[0] as string;
            EID = obj[1] as string;
            facilityID = obj[2] as string;
            startDate = (DateTime)obj[3];
            endDate = (DateTime)obj[4];
        }
    }
}
