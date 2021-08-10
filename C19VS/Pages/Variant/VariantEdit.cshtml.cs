using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Variant
{
    public class EditModel : PageModel
    {
        public const string TASK_NAME = "Variant - EditModel";

        public Models.Variant Variant { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public EditModel(IDatabaseHelper databaseHelper)
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

        public IActionResult OnPost(Models.Variant variant)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(Variant.variantID), variant.variantID);

            DatabaseHelper.ConnectDatabase();

            bool updateSuccesful = DatabaseHelper.UpdateRecord(typeof(Models.Variant), variant, dictionary);

            if (updateSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] Variant {variant.variantID} {variant.variantName} updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error updating variant {variant.variantID} {variant.variantName}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Variant/VariantIndex");
        }
    }
}
