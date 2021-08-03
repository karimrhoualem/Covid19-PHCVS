using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Person
{
    public class EditModel : PageModel
    {
        public Models.Person Person { get; private set; }
        
        private DatabaseHelper DatabaseHelper;

        public EditModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string medicareNum)
        {
            DatabaseHelper.ConnectDatabase();

            Person = await DatabaseHelper.PersonSelectQueryAsync(medicareNum);

            DatabaseHelper.DisconnectDatabase();
            
            if (Person == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Models.Person person)
        {
            DatabaseHelper.ConnectDatabase();
            Person = await DatabaseHelper.UpdatePersonAsync(person);
            DatabaseHelper.DisconnectDatabase();
            return RedirectToPage("/Index");
        }
    }
}
