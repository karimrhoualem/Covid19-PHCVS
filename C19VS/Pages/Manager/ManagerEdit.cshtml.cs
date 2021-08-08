using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Manager
{
    public class EditModel : PageModel
    {
        public const string TASK_NAME = "Manager - EditModel";

        public Models.Manager Manager { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public EditModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string facilityID)
        {
            DatabaseHelper.ConnectDatabase();

            Manager = (Models.Manager) await DatabaseHelper.SelectRecordAsync(typeof(Models.Manager), nameof(facilityID), facilityID);

            DatabaseHelper.DisconnectDatabase();
            
            if (Manager == null)
            {
                return RedirectToPage("/Manager/ManagerIndex");
            }

            return Page();
        }

        public IActionResult OnPost(Models.Manager manager)
        {
            DatabaseHelper.ConnectDatabase();

            bool updateSuccesful = DatabaseHelper.UpdateRecord(typeof(Models.Manager), manager, nameof(manager.facilityID), manager.facilityID);

            if (updateSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] manager of {manager.facilityID} manager updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error updating manager manager of {manager.facilityID}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Manager/ManagerIndex");
        }
    }
}
