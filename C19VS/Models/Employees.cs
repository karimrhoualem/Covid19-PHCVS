using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Models
{
    public class Employees
    {
        public string SSN { get; set; }
        public string EID { get; set; }
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
        public int infected { get; set; }
        public string ageGroup { get; set; }
      

        public Employees()
        {

        }

        public Employees(object obj)
        {
            var employees = obj as Employees;

            this.SSN = employees.SSN;
            this.EID = employees.EID;
            this.medicare = employees.medicare;
            this.firstName = employees.firstName;
            this.lastName = employees.lastName;
            this.dob = employees.dob;
            this.telephone = employees.telephone;
            this.address = employees.address;
            this.city = employees.city;
            this.province = employees.province;
            this.postalCode = employees.postalCode;
            this.citizenship = employees.citizenship;
            this.email = employees.email;
            this.infected = employees.infected;
            this.ageGroup = employees.ageGroup;
        }

        public Employees(object[] obj)
        {
            SSN = obj[0] as string;
            EID = obj[1] as string;
            medicare = obj[2] as string;
            firstName = obj[3] as string;
            lastName = obj[4] as string;
            dob = (DateTime)obj[5];
            telephone = obj[6] as string;
            address = obj[7] as string;
            city = obj[8] as string;
            province = obj[9] as string;
            postalCode = obj[10] as string;
            citizenship = obj[11] as string;
            email = obj[12] as string;
            infected = (int)obj[13];
            ageGroup = obj[14] as string;
        }
    }
}
