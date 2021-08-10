using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Vaccine
{
    public class DeleteModel : PageModel
    {
        public const string TASK_NAME = "Vaccine - DeleteModel";

        public Models.Vaccine Vaccine { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public DeleteModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string vactypeID)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(vactypeID), vactypeID);

            DatabaseHelper.ConnectDatabase();

            Vaccine = (Models.Vaccine) await DatabaseHelper.SelectRecordAsync(typeof(Models.Vaccine), dictionary);

            DatabaseHelper.DisconnectDatabase();

            if (Vaccine == null)
            {
                return RedirectToPage("/Vaccine/VaccineIndex");
            }

            return Page();
        }

        public IActionResult OnPostAsync(Models.Vaccine vaccine)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(vaccine.vacTypeID), vaccine.vacTypeID);

            DatabaseHelper.ConnectDatabase();

            bool deleteSuccesful = DatabaseHelper.DeleteRecord(typeof(Models.Vaccine), dictionary);

            if (deleteSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] vaccine {vaccine.vacTypeID} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error deleting vaccine {vaccine.vacTypeID}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Vaccine/VaccineIndex");
        }
    }
}
