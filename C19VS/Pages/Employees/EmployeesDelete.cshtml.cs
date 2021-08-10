using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        public const string TASK_NAME = "Employees - DeleteModel";

        public Models.Employees Employees { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public DeleteModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string SSN, string EID)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(SSN), SSN);
            dictionary.Add(nameof(EID), EID);

            DatabaseHelper.ConnectDatabase();

            Employees = (Models.Employees) await DatabaseHelper.SelectRecordAsync(typeof(Models.Employees), dictionary);
                
            DatabaseHelper.DisconnectDatabase();

            if (Employees == null)
            {
                return RedirectToPage("/Employees/EmployeesIndex");
            }

            return Page();
        }

        public IActionResult OnPostAsync(Models.Employees employees)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(employees.SSN), employees.SSN);
            dictionary.Add(nameof(employees.EID), employees.EID);

            DatabaseHelper.ConnectDatabase();

            bool deleteSuccesful = DatabaseHelper.DeleteRecord(typeof(Models.Employees), dictionary);

            if (deleteSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] employee {employees.EID} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error deleting employee {employees.EID}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Employees/EmployeesIndex");
        }
    }
}
