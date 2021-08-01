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
        
        private readonly ILogger<IndexModel> _logger;
        private DatabaseHelper databaseHelper;

        public IndexModel(ILogger<IndexModel> logger)
        {
            databaseHelper = new DatabaseHelper(C19VS.User.KARIM);

            GetRowsAsync();
        }

        private async Task GetRowsAsync()
        {
            databaseHelper.Connect();

            tableList = await databaseHelper.QueryTable(TABLE_NAME);

            databaseHelper.Disconnect();
        }

        public void OnGet()
        {

        }
    }
}
