using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Dose
{
    public class InsertModel : PageModel
    {
        public const string TASK_NAME = "Dose - InsertModel";

        public Models.Dose Dose { get; private set; }
        public DatabaseHelper DatabaseHelper { get; private set; }

        public InsertModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper) databaseHelper;
        }

        public IActionResult OnPostAsync(Models.Dose dose)
        {
            DatabaseHelper.ConnectDatabase();

            bool insertSuccessful = DatabaseHelper.InsertRecord(typeof(Models.Dose), dose);

            if (insertSuccessful)
            {
                Console.WriteLine($"[{TASK_NAME}] dose number {dose.doseNumber} of {dose.SSN} inserted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error inserting dose number {dose.doseNumber} of {dose.SSN}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Dose/DoseIndex");
        }
    }
}
