using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MultiSelectDropdownTest.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MultiSelectDropdownTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

       
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var dbConn = _configuration.GetConnectionString("DefaultConnection");
            using(var connection = new SqlConnection(dbConn))
            {
                List<Names> names = new List<Names>();
                List<int> selected = new List<int> { 1, 2, 3, 4, 5};
                names = connection.Query<Names>("SP_GET_NAMES",CommandType.StoredProcedure).ToList();
                ViewBag.Names = names;
                ViewBag.Selected = selected;
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(Names names)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}