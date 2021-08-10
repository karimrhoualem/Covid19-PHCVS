using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Employment
{
    public class EditModel : PageModel
    {
        public const string TASK_NAME = "Employment - EditModel";

        public Models.Employment Employment { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public EditModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string contractNum)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(contractNum), contractNum);

            DatabaseHelper.ConnectDatabase();

            Employment = (Models.Employment) await DatabaseHelper.SelectRecordAsync(typeof(Models.Employment), dictionary);

            DatabaseHelper.DisconnectDatabase();
            
            if (Employment == null)
            {
                return RedirectToPage("/Employment/EmploymentIndex");
            }

            return Page();
        }

        public IActionResult OnPost(Models.Employment employment)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(employment.contractNum), employment.contractNum);

            DatabaseHelper.ConnectDatabase();

            bool updateSuccesful = DatabaseHelper.UpdateRecord(typeof(Models.Employment), employment, dictionary);

            if (updateSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] employment contract {employment.contractNum} updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error updating employment contract {employment.contractNum}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Employment/EmploymentIndex");
        }
    }
}
