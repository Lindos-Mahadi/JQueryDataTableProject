﻿using JqueryDataTableProject.Data;
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

        public JsonResult DeleteContact(int id)
        {
            try
            {
                var data = appDbContext.TblContacts.FirstOrDefault(x => x.Id == id);
                appDbContext.Remove(data);
                appDbContext.SaveChanges();
                return new JsonResult("Delete data Item.!");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult EditContact(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new JsonResult("Item not found.?");
                }
                var contact = appDbContext.TblContacts.Where(x => x.Id == id).SingleOrDefault();
                //appDbContext.Update(contact);
                //appDbContext.SaveChanges();
                return new JsonResult(contact);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public JsonResult Update(TblContact tblContact)
        {
            try
            {
                appDbContext.TblContacts.Update(tblContact);
                appDbContext.SaveChanges();
                return new JsonResult("Data Updated Successfully.!");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
