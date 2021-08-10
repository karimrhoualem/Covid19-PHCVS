using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Employment
{
    public class DeleteModel : PageModel
    {
        public const string TASK_NAME = "Employment - DeleteModel";

        public Models.Employment Employment { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public DeleteModel(IDatabaseHelper databaseHelper)
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
                return RedirectToPage("/Emploment/EmploymentIndex");
            }

            return Page();
        }

        public IActionResult OnPostAsync(Models.Employment employment)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(employment.contractNum), employment.contractNum);

            DatabaseHelper.ConnectDatabase();

            bool deleteSuccesful = DatabaseHelper.DeleteRecord(typeof(Models.Employment), dictionary);

            if (deleteSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] employment contract {employment.contractNum} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error deleting employment contract {employment.contractNum}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Employment/EmploymentIndex");
        }
    }
}
