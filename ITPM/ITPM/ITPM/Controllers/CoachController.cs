using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ITPM.Models;
using ITPM.Report;
using System.Data.SqlClient;

namespace ITPM.Controllers
{
    public class CoachController : Controller
    {
        SqlCommand command;
        private static String connectionstring = "workstation id=Project.mssql.somee.com;packet size=4096;user id=donkavi2_SQLLogin_1;pwd=12345678;data source=Project.mssql.somee.com;persist security info=False;initial catalog=Project";
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataReader reader;
        //List<Customer> TestList = new List<Customer>();
        //Customer test = null;

        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _hostEnvironment;
        public CoachController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            var displaydata = _db.coaches.ToList();
            return View(displaydata);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string Coachsearch)
        {
           
            ViewData["Getoachdetails"] = Coachsearch;

            var orderquery = from x in _db.coaches select x;
            if (!String.IsNullOrEmpty(Coachsearch))
            {
                orderquery = orderquery.Where(x => x.Cname.Contains(Coachsearch));
            }
            return View(await orderquery.AsNoTracking().ToListAsync());
        }




        //GET: Create
        public IActionResult Create()
        {
            //if (HttpContext.Session.GetString("Adminsession") == null)
            //{
            //    return View("sessionerror2");
            //}
            //command = new SqlCommand("select productid from supplierlog", connection);
            //List<int> ob1 = new List<int>();
            //int ob;
            //connection.Open();
            //reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    ob = int.Parse(reader["productid"].ToString());
            //    ob1.Add(ob);
            //}
            //ViewBag.pid2 = ob1;
            //string img = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("Adminsession_img"));
            //if (String.IsNullOrEmpty(img))
            //{
            //    img = "default.png";
            //}
            //ViewBag.url = img;
            return View();
        }

        // POST: Create
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create([Bind("CoachID,Cname,CAge,contactnumber,Email,Description,Rate,ImageFile")] CoachClass nc)
        {
            if (ModelState.IsValid)
            {
                //Save Image to wwwroot/image/items
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(nc.ImageFile.FileName);
                string extension = Path.GetExtension(nc.ImageFile.FileName);
                nc.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Images/Coaches", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await nc.ImageFile.CopyToAsync(fileStream);
                }
                //Insert record
                _db.Add(nc);
                await _db.SaveChangesAsync(); 
                TempData["save"] = "This Product has been added";
                return RedirectToAction(nameof(Index));
            }
            return View(nc);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null)
            {
                return RedirectToAction("Index");
            }

            var getcoachdetails = await _db.coaches.FindAsync(id);
            return View(getcoachdetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CoachClass nc)
        {
            if(ModelState.IsValid)
            {
                _db.Update(nc);
                await _db.SaveChangesAsync();
                TempData["Edit"] = "Coach details has been updated";
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

            var getcoachdetails = await _db.coaches.FindAsync(id);
            return View(getcoachdetails);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var getcoachdetails = await _db.coaches.FindAsync(id);
            return View(getcoachdetails);
        }
         [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var getcoachdetails = await _db.coaches.FindAsync(id);
            _db.coaches.Remove(getcoachdetails);
            await _db.SaveChangesAsync();

            TempData["Delete"] = "This Coach has been deleted";
            return RedirectToAction("Index");
        }

        public IActionResult PrintReport(int param)
        {
            List<CoachClass> getcoachdetails = new List<CoachClass>();



            getcoachdetails = _db.coaches.ToList();



            CoachDetailsReport rpt = new CoachDetailsReport(_hostEnvironment);
            return File(rpt.Report(getcoachdetails), "application/pdf");
        }

    }
}
