using JqueryDataTableProject.Data;
using JqueryDataTableProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace JqueryDataTableProject.Controllers
{
    public class DemoController : Controller
    {
        private readonly ApplicationDbContext appDbContext;

        public DemoController(ApplicationDbContext appDbContext)
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

        [HttpPost]
        public JsonResult CreateContact([FromBody] TblContact tblContact)
        {
            try
            {
                var contactData = new TblContact()
                {
                    Name = tblContact.Name,
                    Email = tblContact.Email,
                    Status = tblContact.Status,
                    Subject = tblContact.Subject,
                    Message = tblContact.Message,
                    AddedDate = tblContact.AddedDate?.ToUniversalTime()
                };
                appDbContext.TblContacts.Add(contactData);
                appDbContext.SaveChanges();
                return new JsonResult("Data is Saved");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
