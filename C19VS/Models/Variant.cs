using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Models
{
    public class Variant
    {
        public string variantID { get; set; }
        public string variantName { get; set; }

        public Variant()
        {

        }

        public Variant(object obj)
        {
            var variant = obj as Variant;

            this.variantID = variant.variantID;
            this.variantName = variant.variantName ;
            
        }

        public Variant(object[] obj)
        {
            variantID = obj[0] as string;
            variantName= obj[1] as string;

        }
    }
}
