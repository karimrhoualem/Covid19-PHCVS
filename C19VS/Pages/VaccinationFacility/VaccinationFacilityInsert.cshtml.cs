using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.VaccinationFacility
{
    public class VaccinationFacilityInsertModel : PageModel
    {
        public const string TASK_NAME = "VaccinationFacility - VaccinationFacilityInsertModel";

        public Models.VaccinationFacility VaccinationFacility { get; private set; }
        public DatabaseHelper DatabaseHelper { get; private set; }

        public VaccinationFacilityInsertModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public IActionResult OnPostAsync(Models.VaccinationFacility VaccinationFacility)
        {
            DatabaseHelper.ConnectDatabase();

            bool insertSuccessful =
                DatabaseHelper.InsertRecord(
                    typeof(Models.VaccinationFacility), VaccinationFacility);

            if (insertSuccessful)
            {
                Console.WriteLine($"[{TASK_NAME}] " +
                    $"Vaccination Facility " +
                    $"{VaccinationFacility.facilityName} " +
                    $"of {VaccinationFacility.facilityID} " +
                    $"inserted successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] " +
                    $"Error inserting " +
                    $"Vaccination Facility " +
                    $"{VaccinationFacility.facilityName} " +
                    $"of {VaccinationFacility.facilityID} " +
                    $"inserted successfully.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/VaccinationFacility/VaccinationFacilityIndex");
        }
    }
}
