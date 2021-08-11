using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C19VS.Pages.VaccinationFacility
{
    public class VaccinationFacilityIndexModel : PageModel
    {
        public string TABLE_NAME = "VaccinationFacility";
        public List<object[]> tableList;

        private DatabaseHelper DatabaseHelper;

        public VaccinationFacilityIndexModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        public async Task OnGetAsync()
        {
            DatabaseHelper.ConnectDatabase();

            tableList = await DatabaseHelper.SelectAllRecords(TABLE_NAME);

            DatabaseHelper.DisconnectDatabase();
        }
    }
}
