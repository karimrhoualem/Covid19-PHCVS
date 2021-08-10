using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Pages.Transfer
{
    public class IndexModel : PageModel
    {
        public string TABLE_NAME = "Transfer";
        public List<object[]> tableList;
        
        private DatabaseHelper DatabaseHelper;

        public IndexModel(IDatabaseHelper databaseHelper)
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
