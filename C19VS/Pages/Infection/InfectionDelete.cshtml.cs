using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Infection
{
    public class DeleteModel : PageModel
    {
        public const string TASK_NAME = "Infection - DeleteModel";

        public Models.Infection Infection { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public DeleteModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string SSN, string infectionDate)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>(); // MAYBE CHANGE STRING INTO DATETIME
            dictionary.Add(nameof(SSN), SSN);
            dictionary.Add(nameof(infectionDate), infectionDate.ToString());

            DatabaseHelper.ConnectDatabase();

            Infection = (Models.Infection) await DatabaseHelper.SelectRecordAsync(typeof(Models.Infection), dictionary);

            DatabaseHelper.DisconnectDatabase();

            if (Infection == null)
            {
                return RedirectToPage("/Infection/InfectionIndex");
            }

            return Page();
        }

        public IActionResult OnPostAsync(Models.Infection infection) // IF CHANGE ABOVE CHANGE HERE TOO
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(infection.SSN), infection.SSN); 
            dictionary.Add(nameof(infection.infectionDate), infection.infectionDate.ToShortDateString());

            DatabaseHelper.ConnectDatabase();

            bool deleteSuccesful = DatabaseHelper.DeleteRecord(typeof(Models.Infection), dictionary);

            if (deleteSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}]  infection of {infection.SSN} at {infection.infectionDate} updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}]  Error updating infection of {infection.SSN} at {infection.infectionDate}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Infection/InfectionIndex");
        }
    }
}
