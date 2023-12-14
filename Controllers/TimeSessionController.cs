using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JqueryDataTableProject.Data;
using JqueryDataTableProject.Models;
using JqueryDataTableProject.Helper;

namespace JqueryDataTableProject.Controllers
{
    public class TimeSessionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeSessionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TimeSession
        public async Task<IActionResult> Index()
        {
            List<TimeSession> timeSessions = await _context.TimeSession.ToListAsync();

            var userTimeZoneId = Request.Cookies["UserTimeZoneId"];

            // Convert UTC times to user's local time for display
            List<TimeSessionViewModel> timeSessionsViewModel = timeSessions
                .Select(session => new TimeSessionViewModel
                {
                    Name = session.Name,
                    StartTime = TimeZoneHelper.GetUserLocalTime(session.StartTime, userTimeZoneId),
                    EndTime = TimeZoneHelper.GetUserLocalTime(session.EndTime, userTimeZoneId),
                    UserTimeZoneId = session.UserTimeZoneId
                })
                .ToList();

            return View(timeSessionsViewModel);
        }

        // GET: TimeSession/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TimeSession == null)
            {
                return NotFound();
            }

            var timeSession = await _context.TimeSession
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeSession == null)
            {
                return NotFound();
            }

            return View(timeSession);
        }

        // GET: TimeSession/Create
        public IActionResult Create()
        {
            ViewBag.TimeZoneList = TimeZoneInfo.GetSystemTimeZones();
            return View();
        }

        // POST: TimeSession/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TimeSessionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var getCooke = Request.Cookies["UserTimeZoneId"];

                string userTimeZoneId = getCooke ?? "DefaultTimeZoneId";

                // Get the user's time zone from the submitted form
                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(userTimeZoneId);

                // Convert the local times to UTC before saving to the database
                TimeSession timeSession = new TimeSession
                {
                    Name = model.Name,
                    StartTime = TimeZoneInfo.ConvertTimeToUtc(model.StartTime, userTimeZone),
                    EndTime = TimeZoneInfo.ConvertTimeToUtc(model.EndTime, userTimeZone),
                    UserTimeZoneId = userTimeZoneId
                };

                // Save the time session to the database
                _context.TimeSession.Add(timeSession);
                 await _context.SaveChangesAsync();
                return RedirectToAction("Index", "TimeSession");
            }

            // If the model state is not valid, return to the create view with errors
            ViewBag.TimeZoneList = TimeZoneInfo.GetSystemTimeZones();
            return View(model);
        }
    
        // GET: TimeSession/Edit/5
        public async Task<IActionResult> Edit(long? id)
            {
                if (id == null || _context.TimeSession == null)
                {
                    return NotFound();
                }

                var timeSession = await _context.TimeSession.FindAsync(id);
                if (timeSession == null)
                {
                    return NotFound();
                }
                return View(timeSession);
        }

        // POST: TimeSession/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,StartTime,EndTime,UserTimeZoneId")] TimeSession timeSession)
        {
            if (id != timeSession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeSessionExists(timeSession.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(timeSession);
        }

        // GET: TimeSession/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TimeSession == null)
            {
                return NotFound();
            }

            var timeSession = await _context.TimeSession
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeSession == null)
            {
                return NotFound();
            }

            return View(timeSession);
        }

        // POST: TimeSession/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TimeSession == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TimeSession'  is null.");
            }
            var timeSession = await _context.TimeSession.FindAsync(id);
            if (timeSession != null)
            {
                _context.TimeSession.Remove(timeSession);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeSessionExists(long id)
        {
            return _context.TimeSession.Any(e => e.Id == id);
        }
     }
}
