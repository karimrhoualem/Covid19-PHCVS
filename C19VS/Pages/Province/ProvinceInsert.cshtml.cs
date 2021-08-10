using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.Province
{
    public class InsertModel : PageModel
    {
        public const string TASK_NAME = "Province - InsertModel";

        public Models.Province Province { get; private set; }
        public DatabaseHelper DatabaseHelper { get; private set; }

        public InsertModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public IActionResult OnPostAsync(Models.Province province)
        {
            DatabaseHelper.ConnectDatabase();

            bool insertSuccessful = DatabaseHelper.InsertRecord(typeof(Models.Province), province);

            if (insertSuccessful)
            {
                Console.WriteLine($"[{TASK_NAME}] {province.province} inserted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error inserting {province.province}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/Province/ProvinceIndex");
        }
    }
}
