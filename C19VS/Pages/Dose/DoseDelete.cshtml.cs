using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Dose
{
    public class DeleteModel : PageModel
    {
        public const string TASK_NAME = "Dose - DeleteModel";

        public Models.Dose Dose { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public DeleteModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string SSN, int doseNumber)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(SSN), SSN);
            dictionary.Add(nameof(doseNumber), doseNumber.ToString());
            DatabaseHelper.ConnectDatabase();

            Dose = (Models.Dose) await DatabaseHelper.SelectRecordAsync(typeof(Models.Dose), dictionary);

            DatabaseHelper.DisconnectDatabase();

            if (Dose == null)
            {
                return RedirectToPage("/Dose/DoseIndex");
            }

            return Page();
        }

        public IActionResult OnPostAsync(Models.Dose dose)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(dose.SSN), dose.SSN);
            dictionary.Add(nameof(dose.doseNumber), dose.doseNumber.ToString());

            DatabaseHelper.ConnectDatabase();

            bool deleteSuccesful = DatabaseHelper.DeleteRecord(typeof(Models.Dose), dictionary);

            if (deleteSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] dose number {dose.doseNumber} of {dose.SSN} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error deleting dose number {dose.doseNumber} of {dose.SSN}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Dose/DoseIndex");
        }
    }
}
