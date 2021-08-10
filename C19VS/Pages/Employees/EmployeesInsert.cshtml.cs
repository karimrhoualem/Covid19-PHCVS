using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Employees
{
    public class InsertModel : PageModel
    {
        public const string TASK_NAME = "Employees - InsertModel";

        public Models.Employees Employees { get; private set; }
        public DatabaseHelper DatabaseHelper { get; private set; }

        public InsertModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper) databaseHelper;
        }

        public IActionResult OnPostAsync(Models.Employees employees)
        {
            DatabaseHelper.ConnectDatabase();

            bool insertSuccessful = DatabaseHelper.InsertRecord(typeof(Models.Employees), employees);

            if (insertSuccessful)
            {
                Console.WriteLine($"[{TASK_NAME}] employee {employees.EID} inserted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error inserting employee {employees.EID}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Employees/EmployeesIndex");
        }
    }
}
