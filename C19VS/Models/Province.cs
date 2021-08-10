using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Models
{
    public class Province
    {
        public string province { get; set; }
        public string ageGroup { get; set; }

        public Province()
        {

        }

        public Province(object obj)
        {
            var province = obj as Province;

            this.province = province.province;
            this.ageGroup = province.ageGroup;
        }

        public Province(object[] obj)
        {
            province = obj[0] as string;
            ageGroup = obj[1] as string;
        }
    }
}
