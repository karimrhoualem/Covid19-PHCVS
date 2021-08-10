using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Employees
{
    public class EditModel : PageModel
    {
        public const string TASK_NAME = "Employees - EditModel";

        public Models.Employees Employees { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public EditModel(IDatabaseHelper databaseHelper)
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

        public IActionResult OnPost(Models.Employees employees)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(employees.SSN), employees.SSN);
            dictionary.Add(nameof(employees.EID), employees.EID);

            DatabaseHelper.ConnectDatabase();

            bool updateSuccesful = DatabaseHelper.UpdateRecord(typeof(Models.Employees), employees, dictionary);

            if (updateSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] employee {employees.EID} updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error updating employee {employees.EID}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Employees/EmployeesIndex");
        }
    }
}
