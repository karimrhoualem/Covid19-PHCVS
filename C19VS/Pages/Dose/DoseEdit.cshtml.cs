using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Dose
{
    public class EditModel : PageModel
    {
        public const string TASK_NAME = "Dose - EditModel";

        public Models.Dose Dose { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public EditModel(IDatabaseHelper databaseHelper)
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

        public IActionResult OnPost(Models.Dose dose)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(dose.SSN), dose.SSN);
            dictionary.Add(nameof(dose.doseNumber), dose.doseNumber.ToString());

            DatabaseHelper.ConnectDatabase();

            bool updateSuccesful = DatabaseHelper.UpdateRecord(typeof(Models.Dose), dose, dictionary);

            if (updateSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] dose number {dose.doseNumber} of {dose.SSN} updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error updating dose number {dose.doseNumber} of {dose.SSN}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Dose/DoseIndex");
        }
    }
}
