using System;

namespace C19VS
{
    public class Person
    {
        public string _firstName { get; set; }
        public string _lastName { get; set; }
        public int _age { get; set; }

        public Person(string firstName, string lastName, int age)
        {
            _firstName = firstName;
            _lastName = lastName;
            _age = age;
        }
    }
} 
	
