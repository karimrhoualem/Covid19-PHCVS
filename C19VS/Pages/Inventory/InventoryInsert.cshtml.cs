using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Inventory
{
    public class InventoryInsertModel : PageModel
    {

        public const string TASK_NAME = "InventoryInsertModel - InsertModel";

        public Models.Inventory Inventory { get; private set; }
        public DatabaseHelper DatabaseHelper { get; private set; }

        public InventoryInsertModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public IActionResult OnPostAsync(Models.Inventory Inventory)
        {
            DatabaseHelper.ConnectDatabase();

            bool insertSuccessful = DatabaseHelper.InsertRecord(typeof(Models.Inventory), Inventory);

            if (insertSuccessful)
            {
                Console.WriteLine($"[{TASK_NAME}] vaccine {Inventory.inventoryID} inserted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error inserting vaccine {Inventory.inventoryID}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Inventory/InventoryIndex");
        }
    }
}
