using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string Name { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            Name = "Gabbi";
        }

        public void OnGet()
        {

        }
    }
}
