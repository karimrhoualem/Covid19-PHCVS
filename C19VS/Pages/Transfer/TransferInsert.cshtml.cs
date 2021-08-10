using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Transfer
{
    public class InsertModel : PageModel
    {
        public const string TASK_NAME = "Transfer - InsertModel";

        public Models.Transfer Transfer { get; private set; }
        public DatabaseHelper DatabaseHelper { get; private set; }

        public InsertModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper) databaseHelper;
        }

        public IActionResult OnPostAsync(Models.Transfer transfer)
        {
            DatabaseHelper.ConnectDatabase();

            bool insertSuccessful = DatabaseHelper.InsertRecord(typeof(Models.Transfer), transfer);

            if (insertSuccessful)
            {
                Console.WriteLine($"[{TASK_NAME}] vaccine transfer {transfer.transferID} inserted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error inserting vaccine transfer {transfer.transferID}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Transfer/TransferIndex");
        }
    }
}
