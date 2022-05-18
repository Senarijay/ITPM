using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITPM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using ITPM.Report;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;


namespace ITPM.Controllers

{
    public class ContController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ContController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _db = db;
        }
        public IActionResult Index()
        {
            var displaydata = _db.ContactTable.ToList();
            return View(displaydata);
        }
        public IActionResult Create()
        {

            int cid = 0;
            string action1, action2, icon, action3, action4 = null, action5, img = null;

            if (HttpContext.Session.GetString("customersession") != null)
            {

                cid = JsonConvert.DeserializeObject<int>(HttpContext.Session.GetString("customersession"));
                img = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("customersession_img"));
                if (String.IsNullOrEmpty(img))
                {
                    action5 = "default.png";
                }
                else
                {
                    action5 = img;
                }
                action1 = "PROFILE";
                action2 = "LOGOUT";
                icon = "fa-power-off";
                action3 = "userprofile";

            }
            else
            {
                action1 = "LOGIN";
                action2 = "SIGN UP";
                icon = "fa-user-plus";
                action3 = "Customerlogin";
                action4 = "Register";
                action5 = "plus.png";
            }
            ViewData["action3"] = action3;
            ViewData["action4"] = action4;
            ViewData["icon"] = icon;
            ViewData["action1"] = action1;
            ViewData["action2"] = action2;
            ViewData["cid"] = cid;
            ViewBag.img = action5;
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> Index(String Contsearch)
        {
            ViewData["Getcontdetails"] = Contsearch;

            var contactquery = from x in _db.ContactTable select x;
            if (!String.IsNullOrEmpty(Contsearch))
            {
                contactquery = contactquery.Where(x => x.Fname.Contains(Contsearch) || x.Lname.Contains(Contsearch) || x.Email.Contains(Contsearch) || x.Phonen.Contains(Contsearch) || x.Cmessage.Contains(Contsearch));
            }
            return View(await contactquery.AsNoTracking().ToListAsync());
        }

        [HttpPost]

        public async Task<IActionResult> Create(NewCont nec)
        {
            if (ModelState.IsValid)
            {
                _db.Add(nec);
                await _db.SaveChangesAsync();
                return RedirectToAction("Create");
            }
            return View(nec);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var getContdetails = await _db.ContactTable.FindAsync(id);
            return View(getContdetails);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(NewCont nc)
        {
            if (ModelState.IsValid)
            {
                _db.Update(nc);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nc);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var getContdetails = await _db.ContactTable.FindAsync(id);
            return View(getContdetails);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var getContdetails = await _db.ContactTable.FindAsync(id);
            return View(getContdetails);
        }
        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {

            var getContdetails = await _db.ContactTable.FindAsync(id);
            _db.ContactTable.Remove(getContdetails);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //public IActionResult ContactPartial()
        //{

        //    return PartialView("Owner/ContactDash");
        //}



        public ActionResult printcontact(int param)
        {
            List<NewCont> ob = new List<NewCont>();

            ob = _db.ContactTable.ToList();

            ContactReport cr = new ContactReport(_hostEnvironment);
            return File(cr.Report(ob), "application/pdf");
        }

        public IActionResult AboutUs()
        {
            int cid = 0;
            string action1, action2, icon, action3, action4 = null, action5, img = null;

            if (HttpContext.Session.GetString("customersession") != null)
            {

                cid = JsonConvert.DeserializeObject<int>(HttpContext.Session.GetString("customersession"));
                img = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("customersession_img"));
                if (String.IsNullOrEmpty(img))
                {
                    action5 = "default.png";
                }
                else
                {
                    action5 = img;
                }
                action1 = "PROFILE";
                action2 = "LOGOUT";
                icon = "fa-power-off";
                action3 = "userprofile";

            }
            else
            {
                action1 = "LOGIN";
                action2 = "SIGN UP";
                icon = "fa-user-plus";
                action3 = "Customerlogin";
                action4 = "Register";
                action5 = "plus.png";
            }
            ViewData["action3"] = action3;
            ViewData["action4"] = action4;
            ViewData["icon"] = icon;
            ViewData["action1"] = action1;
            ViewData["action2"] = action2;
            ViewData["cid"] = cid;
            ViewBag.img = action5;

            return View();
        }

    }


}
