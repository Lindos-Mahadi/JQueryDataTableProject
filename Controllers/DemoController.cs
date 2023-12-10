using JqueryDataTableProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace JqueryDataTableProject.Controllers
{
    public class DemoController : Controller
    {
        private readonly AppDbContext appDbContext;

        public DemoController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetContactList()
        {
            var contactList = appDbContext.TblContacts.ToList();
            return new JsonResult(contactList);
        }
    }
}
