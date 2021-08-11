using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Inventory
{
    public class InventoryDeleteModel : PageModel
    {
        public const string TASK_NAME = "InventoryDeleteModel - DeleteModel";

        public Models.Inventory Inventory { get; private set; }

        private DatabaseHelper DatabaseHelper;

        public InventoryDeleteModel(IDatabaseHelper databaseHelper)
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

        public IActionResult OnPostAsync(Models.Inventory Inventory)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(Inventory.inventoryID), Inventory.inventoryID);

            DatabaseHelper.ConnectDatabase();

            bool deleteSuccesful = DatabaseHelper.DeleteRecord(typeof(Models.Inventory), dictionary);

            if (deleteSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] vaccine {Inventory.inventoryID} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error deleting vaccine {Inventory.inventoryID}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Inventory/InventoryIndex");
        }
    }
}
