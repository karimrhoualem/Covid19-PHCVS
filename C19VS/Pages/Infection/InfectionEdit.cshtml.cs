using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Infection
{
    public class EditModel : PageModel
    {
        public const string TASK_NAME = "Infection - EditModel";

        public Models.Infection Infection { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public EditModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string SSN, string infectionDate) // 2 ATTRIBUTES
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
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

        public IActionResult OnPost(Models.Infection infection)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(infection.SSN), infection.SSN);
            dictionary.Add(nameof(infection.infectionDate), infection.infectionDate.ToString());

            DatabaseHelper.ConnectDatabase();

            bool updateSuccesful = DatabaseHelper.UpdateRecord(typeof(Models.Infection), infection, dictionary);

            if (updateSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] infection of {infection.SSN} at {infection.infectionDate} updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error updating infection of {infection.SSN} at {infection.infectionDate}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Infection/InfectionIndex");
        }
    }
}
