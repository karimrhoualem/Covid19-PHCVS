using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Models
{
    public class Employee
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
        public bool infected { get; set; }
        public string ageGroup { get; set; }
      

        public Employee()
        {

        }

        public Employee(object obj)
        {
            var employee = obj as Employee;

            this.SSN = employee.SSN;
            this.EID = employee.EID;
            this.medicare = employee.medicare;
            this.firstName = employee.firstName;
            this.lastName = employee.lastName;
            this.dob = employee.dob;
            this.telephone = employee.telephone;
            this.address = employee.address;
            this.city = employee.city;
            this.province = employee.province;
            this.postalCode = employee.postalCode;
            this.citizenship = employee.citizenship;
            this.email = employee.email;
            this.infected = employee.infected;
            this.ageGroup = employee.ageGroup;
        }

        public Employee(object[] obj)
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
            infected = (bool)obj[13];
            ageGroup = obj[14] as string;
        }
    }
}
