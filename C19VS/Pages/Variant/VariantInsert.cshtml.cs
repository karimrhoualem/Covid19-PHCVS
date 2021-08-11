using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Variant
{
    public class InsertModel : PageModel
    {
        public const string TASK_NAME = "Variant - InsertModel";

        public Models.Variant Variant { get; private set; }
        public DatabaseHelper DatabaseHelper { get; private set; }

        public InsertModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper) databaseHelper;
        }

        public IActionResult OnPost(Models.Variant variant)
        {
            DatabaseHelper.ConnectDatabase();

            bool insertSuccessful = DatabaseHelper.InsertRecord(typeof(Models.Variant), variant);

            if (insertSuccessful)
            {
                Console.WriteLine($"[{TASK_NAME}] variant {variant.variantID} {variant.variantName} inserted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error inserting variant {variant.variantID} {variant.variantName}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Variant/VariantIndex");
        }
    }
}
