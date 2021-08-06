using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.AgeGroup
{
    public class AgeGroupInsertModel : PageModel
    {
        public const string TASK_NAME = "AgeGroup - InsertModel";

        public Models.AgeGroup AgeGroup { get; private set; }
        public DatabaseHelper DatabaseHelper { get; private set; }

        public AgeGroupInsertModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public IActionResult OnPostAsync(Models.AgeGroup ageGroup)
        {
            DatabaseHelper.ConnectDatabase();

            bool insertSuccessful = DatabaseHelper.InsertRecord(typeof(Models.AgeGroup), ageGroup);

            if (insertSuccessful)
            {
                Console.WriteLine($"[{TASK_NAME}] {ageGroup.ageGroup} : {ageGroup.Allowed} inserted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error inserting {ageGroup.ageGroup} : {ageGroup.Allowed}.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/AgeGroup/AgeGroupIndex");
        }
    }
}
