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
                if (tblContact == null)
                {
                    return new JsonResult("Data is Invalid!");
                }
                var contactData = new TblContact()
                {
                    Name = tblContact.Name,
                    Email = tblContact.Email,
                    Status = tblContact.Status ?? false,
                    Subject = tblContact.Subject,
                    Message = tblContact.Message,
                    AddedDate = tblContact.AddedDate
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
