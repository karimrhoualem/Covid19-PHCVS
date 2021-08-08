using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Models
{
    public class Manager
    {
        public string SSN { get; set; }
        public string EID { get; set; }
        public string facilityID { get; set; }
        public string medicare { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dob { get; set; }
        public string telephone { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string postalCode { get; set; }
        public string citizenship { get; set; }
        public string email { get; set; }
        public bool infected { get; set; }
        public string ageGroup { get; set; }
      

        public Manager()
        {

        }

        public Manager(object obj)
        {
            var manager = obj as Manager;

            this.SSN = manager.SSN;
            this.EID = manager.EID;
            this.facilityID = manager.facilityID;
            this.medicare = manager.medicare;
            this.firstName = manager.firstName;
            this.lastName = manager.lastName;
            this.dob = manager.dob;
            this.telephone = manager.telephone;
            this.address = manager.address;
            this.city = manager.city;
            this.province = manager.province;
            this.postalCode = manager.postalCode;
            this.citizenship = manager.citizenship;
            this.email = manager.email;
            this.infected = manager.infected;
            this.ageGroup = manager.ageGroup;
        }

        public Manager(object[] obj)
        {
            SSN = obj[0] as string;
            EID = obj[1] as string;
            facilityID = obj[2] as string;
            medicare = obj[3] as string;
            firstName = obj[4] as string;
            lastName = obj[5] as string;
            dob = (DateTime)obj[6];
            telephone = obj[7] as string;
            address = obj[8] as string;
            city = obj[9] as string;
            province = obj[10] as string;
            postalCode = obj[11] as string;
            citizenship = obj[12] as string;
            email = obj[13] as string;
            infected = (bool)obj[14];
            ageGroup = obj[15] as string;
        }
    }
}
