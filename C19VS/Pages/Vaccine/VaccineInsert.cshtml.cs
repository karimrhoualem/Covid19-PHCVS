using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Vaccine
{
    public class InsertModel : PageModel
    {
        public const string TASK_NAME = "Vaccine - InsertModel";

        public Models.Vaccine Vaccine { get; private set; }
        public DatabaseHelper DatabaseHelper { get; private set; }

        public InsertModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper) databaseHelper;
        }

        public IActionResult OnPostAsync(Models.Vaccine vaccine)
        {
            DatabaseHelper.ConnectDatabase();

            bool insertSuccessful = DatabaseHelper.InsertRecord(typeof(Models.Vaccine), vaccine);

            if (insertSuccessful)
            {
                Console.WriteLine($"[{TASK_NAME}] vaccine {vaccine.vacTypeID} inserted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error inserting vaccine {vaccine.vacTypeID}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Vaccine/VaccineIndex");
        }
    }
}
