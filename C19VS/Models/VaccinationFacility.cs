using System;
namespace C19VS.Models
{
    public class VaccinationFacility
    {
        public string facilityID { get; set; }
        public string facilityType { get; set; }
        public string facilityName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string province { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public string webAddress { get; set; }

        public VaccinationFacility()
        {
        }

        public VaccinationFacility(object obj)
        {
            var VaccinationFacility = obj as VaccinationFacility;

            this.facilityID = VaccinationFacility.facilityID;
            this.facilityType = VaccinationFacility.facilityType;
            this.facilityName = VaccinationFacility.facilityName;
            this.address = VaccinationFacility.address;
            this.city = VaccinationFacility.city;
            this.postalCode = VaccinationFacility.postalCode;
            this.province = VaccinationFacility.province;
            this.telephone = VaccinationFacility.telephone;
            this.email = VaccinationFacility.email;
            this.webAddress = VaccinationFacility.webAddress;
        }

        public VaccinationFacility(object[] obj)
        {
            facilityID = obj[0] as string;
            facilityType = obj[1] as string;
            facilityName = obj[2] as string;
            address = obj[3] as string;
            city = obj[4] as string;
            postalCode = obj[5] as string;
            province = obj[6] as string;
            telephone = obj[7] as string;
            email = obj[8] as string;
            webAddress = obj[9] as string;
           
        }


    }
}
