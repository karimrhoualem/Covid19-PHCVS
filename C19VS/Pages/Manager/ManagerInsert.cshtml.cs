using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Manager
{
    public class InsertModel : PageModel
    {
        public const string TASK_NAME = "Manager - InsertModel";

        public Models.Manager Manager { get; private set; }
        public DatabaseHelper DatabaseHelper { get; private set; }

        public InsertModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper) databaseHelper;
        }

        public IActionResult OnPostAsync(Models.Manager manager)
        {
            DatabaseHelper.ConnectDatabase();

            bool insertSuccessful = DatabaseHelper.InsertRecord(typeof(Models.Manager), manager);

            if (insertSuccessful)
            {
                Console.WriteLine($"[{TASK_NAME}] manager of {manager.facilityID} inserted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error inserting manager of {manager.facilityID}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Manager/ManagerIndex");
        }
    }
}
