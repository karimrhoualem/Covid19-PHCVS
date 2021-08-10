using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Transfer
{
    public class DeleteModel : PageModel
    {
        public const string TASK_NAME = "Transfer - DeleteModel";

        public Models.Transfer Transfer { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public DeleteModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string transferID, string vacTypeID)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(transferID), vacTypeID);
            dictionary.Add(nameof(transferID), vacTypeID);

            DatabaseHelper.ConnectDatabase();

            Transfer = (Models.Transfer) await DatabaseHelper.SelectRecordAsync(typeof(Models.Transfer), dictionary);
                
            DatabaseHelper.DisconnectDatabase();

            if (Transfer == null)
            {
                return RedirectToPage("/Transfer/TransferIndex");
            }

            return Page();
        }

        public IActionResult OnPostAsync(Models.Transfer transfer)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(transfer.transferID), transfer.transferID);
            dictionary.Add(nameof(transfer.vacTypeID), transfer.vacTypeID);

            DatabaseHelper.ConnectDatabase();

            bool deleteSuccesful = DatabaseHelper.DeleteRecord(typeof(Models.Transfer), dictionary);

            if (deleteSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] vaccine transfer {transfer.transferID} of {transfer.vacTypeID} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error deleting vaccine transfer {transfer.transferID} of {transfer.vacTypeID}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Tranfers/TransferIndex");
        }
    }
}
