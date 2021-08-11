using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Shipment
{
    public class ShipmentEditModel : PageModel
    {
        public const string TASK_NAME = "Shipment - EditModel";

        public Models.Shipment Shipment { get; private set; }

        private DatabaseHelper DatabaseHelper;

        public ShipmentEditModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string shipmentID)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(shipmentID), shipmentID);

            DatabaseHelper.ConnectDatabase();

            Shipment = (Models.Shipment)await DatabaseHelper.SelectRecordAsync(typeof(Models.Shipment), dictionary);

            DatabaseHelper.DisconnectDatabase();

            if (Shipment == null)
            {
                return RedirectToPage("/Shipment/ShipmentIndex");
            }

            return Page();
        }

        public IActionResult OnPost(Models.Shipment shipment)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(shipment.shipmentID), shipment.shipmentID);

            DatabaseHelper.ConnectDatabase();

            bool updateSuccesful = DatabaseHelper.UpdateRecord(typeof(Models.Shipment), shipment, dictionary);

            if (updateSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] {shipment.shipmentID} updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error updating {shipment.shipmentID}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Shipment/ShipmentIndex");
        }
    }
}
