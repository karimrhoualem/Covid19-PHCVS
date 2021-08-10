using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Vaccine
{
    public class EditModel : PageModel
    {
        public const string TASK_NAME = "Vaccine - EditModel";

        public Models.Vaccine Vaccine { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public EditModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string vacTypeID)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(vacTypeID), vacTypeID);

            DatabaseHelper.ConnectDatabase();

            Vaccine = (Models.Vaccine) await DatabaseHelper.SelectRecordAsync(typeof(Models.Vaccine), dictionary);

            DatabaseHelper.DisconnectDatabase();
            
            if (Vaccine == null)
            {
                return RedirectToPage("/Vaccine/VaccineIndex");
            }

            return Page();
        }

        public IActionResult OnPost(Models.Vaccine vaccine)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(vaccine.vacTypeID), vaccine.vacTypeID);

            DatabaseHelper.ConnectDatabase();

            bool updateSuccesful = DatabaseHelper.UpdateRecord(typeof(Models.Vaccine), vaccine, dictionary);

            if (updateSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] vaccine {vaccine.vacTypeID} updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error updating vaccine {vaccine.vacTypeID}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Vaccine/VaccineIndex");
        }
    }
}
