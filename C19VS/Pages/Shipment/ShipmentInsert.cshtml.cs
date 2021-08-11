using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Shipment
{
    public class ShipmentInsertModel : PageModel
    {
        public const string TASK_NAME = "Shipment - InsertModel";

        public Models.Shipment Shipment { get; private set; }
        public DatabaseHelper DatabaseHelper { get; private set; }

        public ShipmentInsertModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public IActionResult OnPostAsync(Models.Shipment shipment)
        {
            DatabaseHelper.ConnectDatabase();

            bool insertSuccessful = DatabaseHelper.InsertRecord(typeof(Models.Shipment), shipment);

            if (insertSuccessful)
            {
                Console.WriteLine($"[{TASK_NAME}] {shipment.shipmentID} inserted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error inserting {shipment.shipmentID}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Shipment/ShipmentIndex");
        }
    }
}
