using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Inventory
{
    public class InventoryEditModel : PageModel
    {
        public const string TASK_NAME = "InventoryEditModel - EditModel";

        public Models.Inventory Inventory { get; private set; }

        private DatabaseHelper DatabaseHelper;

        public InventoryEditModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string inventoryID)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(inventoryID), inventoryID);

            DatabaseHelper.ConnectDatabase();

            Inventory = (Models.Inventory)await DatabaseHelper.SelectRecordAsync(typeof(Models.Inventory), dictionary);

            DatabaseHelper.DisconnectDatabase();

            if (Inventory == null)
            {
                return RedirectToPage("/Inventory/InventoryIndex");
            }

            return Page();
        }

        public IActionResult OnPost(Models.Inventory Inventory)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(Inventory.inventoryID), Inventory.inventoryID);


            DatabaseHelper.ConnectDatabase();

            bool updateSuccesful = DatabaseHelper.UpdateRecord(typeof(Models.Inventory), Inventory, dictionary);

            if (updateSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] Inventory  {Inventory.inventoryID} of Facility {Inventory.facilityID} updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error updating Inventory  {Inventory.inventoryID} of Facility {Inventory.facilityID}");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Inventory/InventoryIndex");
        }
    }
}
