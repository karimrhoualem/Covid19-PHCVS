using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Models
{
    public class Person
    {
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
        public string SSN { get; set; }

        public Person()
        {

        }

        public Person(object obj)
        {
            var person = obj as Person;

            this.medicare = person.medicare;
            this.firstName = person.firstName;
            this.lastName = person.lastName;
            this.dob = person.dob;
            this.telephone = person.telephone;
            this.address = person.address;
            this.city = person.city;
            this.province = person.province;
            this.postalCode = person.postalCode;
            this.citizenship = person.citizenship;
            this.email = person.email;
            this.infected = person.infected;
            this.ageGroup = person.ageGroup;
            this.SSN = person.SSN;
        }

        public Person(object[] obj)
        {
            medicare = obj[0] as string;
            firstName = obj[1] as string;
            lastName = obj[2] as string;
            dob = (DateTime)obj[3];
            telephone = obj[4] as string;
            address = obj[5] as string;
            city = obj[6] as string;
            province = obj[7] as string;
            postalCode = obj[8] as string;
            citizenship = obj[9] as string;
            email = obj[10] as string;
            infected = (bool)obj[11];
            ageGroup = obj[12] as string;
            SSN = obj[13] as string;
        }
    }
}
