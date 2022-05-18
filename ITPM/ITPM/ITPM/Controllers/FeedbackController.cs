using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITPM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using ClosedXML.Excel;
using System.IO;
using ITPM.Report;
using System.Data.SqlClient;

namespace ITPM.Controllers
{
    public class FeedbackController : Controller
    {
        SqlCommand command;
        private static String connectionstring = "workstation id=Project.mssql.somee.com;packet size=4096;user id=donkavi2_SQLLogin_1;pwd=12345678;data source=Project.mssql.somee.com;persist security info=False;initial catalog=Project";
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataReader reader;
        //List<Customer> TestList = new List<Customer>();
        //Customer test = null;

        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FeedbackController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> FeedIn(String Feedsearch)
        {
            ViewData["Getfeeddetails"] = Feedsearch;

            var feedquery = from x in _db.FeedbackTable select x;
            if (!String.IsNullOrEmpty(Feedsearch))
            {
                feedquery = feedquery.Where(x => x.FName.Contains(Feedsearch) || x.LName.Contains(Feedsearch) || x.FeedType.Contains(Feedsearch));
            }
            return View(await feedquery.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Index(String Feedsearch)
        {
            ViewData["Getitemdetails"] = Feedsearch;

            var feedquery = from x in _db.FeedbackTable select x;
            if (!String.IsNullOrEmpty(Feedsearch))
            {
                feedquery = feedquery.Where(x => x.FName.Contains(Feedsearch) || x.LName.Contains(Feedsearch) || x.FeedType.Contains(Feedsearch));
            }

            return View(await feedquery.AsNoTracking().ToListAsync());
        }

        //GET: Create
        public IActionResult CreateFeed()
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
        public async Task<IActionResult> CreateFeed([Bind("FeedID,FName,LName,FeedType,FeedDes,St_01,St_02,Rate")] FeedbackClass feedbackClass)
        {
            if (ModelState.IsValid)
            {
                ////Save Image to wwwroot/image/items
                //string wwwRootPath = _hostEnvironment.WebRootPath;
                //string fileName = Path.GetFileNameWithoutExtension(feedbackClass.ImageFile.FileName);
                //string extension = Path.GetExtension(feedbackClass.ImageFile.FileName);
                //feedbackClass.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                //string path = Path.Combine(wwwRootPath + "/Images/Items", fileName);
                //using (var fileStream = new FileStream(path, FileMode.Create))
                //{
                //    await feedbackClass.ImageFile.CopyToAsync(fileStream);
                //}
                //Insert record
                _db.Add(feedbackClass);
                await _db.SaveChangesAsync();
                //TempData["save"] = "This Product has been added";
                return RedirectToAction(nameof(Index));
            }
            return View(feedbackClass);
        }

        //GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            //if (HttpContext.Session.GetString("Adminsession") == null)
            //{
            //    return View("sessionerror2");
            //}
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var feedbackclass = await _db.FeedbackTable
            //     .FirstOrDefaultAsync(m => m.FeedID == id);
            //if (feedbackclass == null)
            //{
            //    return NotFound();
            //}
            //string img = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("Adminsession_img"));
            //if (String.IsNullOrEmpty(img))
            //{
            //    img = "default.png";
            //}
            //ViewBag.url = img;

            //return View(feedbackclass);

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var getfeeddetails = await _db.FeedbackTable.FindAsync(id);
            return View(getfeeddetails);
        }

        //Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("FeedIn");
            }
            var getFeeddetails = await _db.FeedbackTable.FindAsync(id);
            return View(getFeeddetails);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(FeedbackClass nc)
        {
            if (ModelState.IsValid)
            {
                _db.Update(nc);
                await _db.SaveChangesAsync();
                TempData["edit"] = "This Feedback has been Updated";
                return RedirectToAction("FeedIn");
            }
            return View(nc);
        }

        //Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("FeedIn");
            }
            var getFeeddetails = await _db.FeedbackTable.FindAsync(id);
            return View(getFeeddetails);
        }
        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {

            var getFeeddetails = await _db.FeedbackTable.FindAsync(id);
            _db.FeedbackTable.Remove(getFeeddetails);
            await _db.SaveChangesAsync();
            TempData["delete"] = "This Feedback has been deleted";
            return RedirectToAction("FeedIn");
        }

        //Generate PDF
        public IActionResult FeedbackDetails(int param)
        {
            List<FeedbackClass> ob = new List<FeedbackClass>();

            ob = _db.FeedbackTable.ToList();

            FeedReport cr = new FeedReport(_hostEnvironment);
            return File(cr.Report(ob), "application/pdf");
        }

        //Generate Excel file
        public ActionResult PrintProductExcel()
        {
            List<FeedbackClass> ob = new List<FeedbackClass>();
            ob = _db.FeedbackTable.ToList(); using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Product List");
                var currentrow = 1;
                #region Header
                worksheet.Cell(currentrow, 1).Value = "First Name";
                worksheet.Cell(currentrow, 2).Value = "Last Name";
                worksheet.Cell(currentrow, 3).Value = "Feedback Type";
                worksheet.Cell(currentrow, 4).Value = "Feedback Comment";
                worksheet.Cell(currentrow, 5).Value = "St_01";
                worksheet.Cell(currentrow, 6).Value = "St_02";
                worksheet.Cell(currentrow, 7).Value = "Rate";
                
                #endregion
                #region body 
                foreach (var cus in ob)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = cus.FName;
                    worksheet.Cell(currentrow, 2).Value = cus.LName;
                    worksheet.Cell(currentrow, 3).Value = cus.FeedType;
                    worksheet.Cell(currentrow, 4).Value = cus.FeedDes;
                    worksheet.Cell(currentrow, 5).Value = cus.St_01;
                    worksheet.Cell(currentrow, 6).Value = cus.St_02;
                    worksheet.Cell(currentrow, 7).Value = cus.Rate;
                    
                }
                #endregion
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray(); return File(
                    content,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Feedback Details.xlsx"
                    );
                }
            }
        }

    }

    
}
