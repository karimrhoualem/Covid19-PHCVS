using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Transfer
{
    public class EditModel : PageModel
    {
        public const string TASK_NAME = "Transfer - EditModel";

        public Models.Transfer Transfer { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public EditModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string transferID)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(transferID), transferID);

            DatabaseHelper.ConnectDatabase();

            Transfer = (Models.Transfer) await DatabaseHelper.SelectRecordAsync(typeof(Models.Transfer), dictionary);

            DatabaseHelper.DisconnectDatabase();
            
            if (Transfer == null)
            {
                return RedirectToPage("/Transfer/TransferIndex");
            }

            return Page();
        }

        public IActionResult OnPost(Models.Transfer transfer)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(transfer.transferID), transfer.transferID);

            DatabaseHelper.ConnectDatabase();

            bool updateSuccesful = DatabaseHelper.UpdateRecord(typeof(Models.Transfer), transfer, dictionary);

            if (updateSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] vaccine transfer {transfer.transferID} updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error updating vaccine transfer {transfer.transferID}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Transfer/TransferIndex");
        }
    }
}
