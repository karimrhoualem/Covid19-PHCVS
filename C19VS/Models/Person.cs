using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Models
{
    public class Person
    {
        public string SSN { get; set; }
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

        public Person()
        {

        }

        public Person(object obj)
        {
            var person = obj as Person;

            this.SSN = person.SSN;
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
        }

        public Person(object[] obj)
        {
            SSN = obj[0] as string;
            medicare = obj[1] as string;
            firstName = obj[2] as string;
            lastName = obj[3] as string;
            dob = (DateTime)obj[4];
            telephone = obj[5] as string;
            address = obj[6] as string;
            city = obj[7] as string;
            province = obj[8] as string;
            postalCode = obj[9] as string;
            citizenship = obj[10] as string;
            email = obj[11] as string;
            infected = (int)obj[12];
            ageGroup = obj[13] as string;
        }
    }
}
