using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.VaccinationFacility
{
    public class VaccinationFacilityEditModel : PageModel
    {
        public const string TASK_NAME = "VaccinationFacilityEditModel - EditModel";

        public Models.VaccinationFacility VaccinationFacility { get; private set; }

        private DatabaseHelper DatabaseHelper;

        public VaccinationFacilityEditModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task<IActionResult> OnGetAsync(string facilityID)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(facilityID), facilityID);
           
            DatabaseHelper.ConnectDatabase();

            VaccinationFacility = (Models.VaccinationFacility)await DatabaseHelper.SelectRecordAsync(typeof(Models.VaccinationFacility), dictionary);

            DatabaseHelper.DisconnectDatabase();

            if (VaccinationFacility == null)
            {
                return RedirectToPage("/VaccinationFacility/VaccinationFacilityIndex");
            }

            return Page();
        }

        public IActionResult OnPost(Models.VaccinationFacility vaccinationFacility)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(nameof(VaccinationFacility.facilityID), vaccinationFacility.facilityID);
          

            DatabaseHelper.ConnectDatabase();

            bool updateSuccesful = DatabaseHelper.UpdateRecord(typeof(Models.VaccinationFacility), vaccinationFacility, dictionary);

            if (updateSuccesful)
            {
                Console.WriteLine($"[{TASK_NAME}] Vaccination Facility {vaccinationFacility.facilityName} of {vaccinationFacility.facilityID} updated successfully.");
            }
            else
            {
                Console.WriteLine($"[{TASK_NAME}] Error updating Vaccination Facility {vaccinationFacility.facilityName} of {vaccinationFacility.facilityID} updated successfully.");
            }

            DatabaseHelper.DisconnectDatabase();

            return RedirectToPage("/VaccinationFacility/VaccinationFacilityIndex");
        }
    }
}
