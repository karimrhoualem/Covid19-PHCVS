using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Models
{
    public class AgeGroup
    {
        public string ageGroup { get; set; }
        public bool Allowed { get; set; }

        public AgeGroup()
        {

        }

        public AgeGroup(object obj)
        {
            var ageGroup = obj as AgeGroup;

            this.ageGroup = ageGroup.ageGroup;
            this.Allowed = ageGroup.Allowed;
        }

        public AgeGroup(object[] obj)
        {
            ageGroup = obj[0] as string;
            Allowed = (bool)obj[1];
        }
    }
}
