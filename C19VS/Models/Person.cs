using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Models
{
    public class Person
    {
        public string Medicare { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Citizenship { get; set; }
        public string Email { get; set; }
        public bool Infected { get; set; }
        public string AgeGroup { get; set; }

        public Person()
        {

        }

        public Person MapToPerson(object[] personArray)
        {
            Medicare = personArray[0] as string;
            FirstName = personArray[1] as string;
            LastName = personArray[2] as string;
            DateOfBirth = (DateTime) personArray[3];
            Telephone = personArray[4] as string;
            Address = personArray[5] as string;
            City = personArray[6] as string;
            Province = personArray[7] as string;
            PostalCode = personArray[8] as string;
            Citizenship = personArray[9] as string;
            Email = personArray[10] as string;
            Infected = (bool) personArray[11];
            AgeGroup = personArray[12] as string;

            return this;
        }
    }
}
