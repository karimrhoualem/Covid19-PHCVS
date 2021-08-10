using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Province
{
    public class EditModel : PageModel
    {
        public const string TASK_NAME = "Province - EditModel";

        public Models.Province Province { get; private set; }

        private DatabaseHelper DatabaseHelper;

        public EditModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string province)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(province), province);

            DatabaseHelper.ConnectDatabase();

            Province = (Models.Province)await DatabaseHelper.SelectRecordAsync(typeof(Models.Province), dictionary);

            DatabaseHelper.DisconnectDatabase();

            if (Province == null)
            {
                return RedirectToPage("/Province/ProvinceIndex");
            }

            return Page();
        }

        public IActionResult OnPost(Models.Province province)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(province.province), province.province);

            DatabaseHelper.ConnectDatabase();

            bool updateSuccesful = DatabaseHelper.UpdateRecord(typeof(Models.Province), province, dictionary);

            if (updateSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] {province.province} updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error updating {province.province}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Province/ProvinceIndex");
        }
    }
}
