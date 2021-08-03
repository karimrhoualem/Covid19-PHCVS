using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Pages
{
    public class IndexModel : PageModel
    {
        public string TABLE_NAME = "Person";
        public List<object[]> tableList;
        
        private DatabaseHelper DatabaseHelper;

        public IndexModel(IDatabaseHelper databaseHelper)
        {
            DatabaseHelper = (DatabaseHelper)databaseHelper;
        }

        private async Task GetRowsAsync()
        {
            DatabaseHelper.ConnectDatabase();

            tableList = await DatabaseHelper.TableSelectQuery(TABLE_NAME);

            DatabaseHelper.DisconnectDatabase();
        }

        public async Task OnGetAsync()
        {
            await GetRowsAsync();
        }
    }
}
