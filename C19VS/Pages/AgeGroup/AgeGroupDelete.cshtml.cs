using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.AgeGroup
{
    public class AgeGroupDeleteModel : PageModel
    {
        public const string TASK_NAME = "AgeGroup - DeleteModel";

        public Models.AgeGroup AgeGroup { get; private set; }

        private DatabaseHelper DatabaseHelper;

        public AgeGroupDeleteModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string ageGroup)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(ageGroup), ageGroup);

            DatabaseHelper.ConnectDatabase();

            AgeGroup = (Models.AgeGroup)await DatabaseHelper.SelectRecordAsync(typeof(Models.AgeGroup), dictionary);

            DatabaseHelper.DisconnectDatabase();

            if (AgeGroup == null)
            {
                return RedirectToPage("/AgeGroup/AgeGroupIndex");
            }

            return Page();
        }

        public IActionResult OnPostAsync(Models.AgeGroup ageGroup)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(ageGroup.ageGroup), ageGroup.ageGroup);

            DatabaseHelper.ConnectDatabase();

            bool deleteSuccesful = DatabaseHelper.DeleteRecord(typeof(Models.AgeGroup), dictionary);

            if (deleteSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] {ageGroup.ageGroup} : {ageGroup.ageGroup} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error deleting {ageGroup.ageGroup} : {ageGroup.ageGroup}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/AgeGroup/AgeGroupIndex");
        }
    }
}
