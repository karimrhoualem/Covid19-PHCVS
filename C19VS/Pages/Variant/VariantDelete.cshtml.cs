using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Variant
{
    public class DeleteModel : PageModel
    {
        public const string TASK_NAME = "Variant - DeleteModel";

        public Models.Variant Variant { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public DeleteModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string variantID)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(variantID), variantID);
           
            DatabaseHelper.ConnectDatabase();

            Variant = (Models.Variant) await DatabaseHelper.SelectRecordAsync(typeof(Models.Variant), dictionary);

            DatabaseHelper.DisconnectDatabase();

            if (Variant == null)
            {
                return RedirectToPage("/Variant/VariantIndex");
            }

            return Page();
        }

        public IActionResult OnPostAsync(Models.Variant variant)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(variant.variantID), variant.variantID);

            DatabaseHelper.ConnectDatabase();

            bool deleteSuccesful = DatabaseHelper.DeleteRecord(typeof(Models.Variant), dictionary);

            if (deleteSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}]  Variant {variant.variantID} {variant.variantName} updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}]  Error updating variant {variant.variantID} {variant.variantName}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Variant/VariantIndex");
        }
    }
}
