using ITPM.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ITPM.Controllers
{
    public class HomeController : Controller
    {
        SqlCommand command;
        private static String connectionstring = "workstation id=Project.mssql.somee.com;packet size=4096;user id=donkavi2_SQLLogin_1;pwd=12345678;data source=Project.mssql.somee.com;persist security info=False;initial catalog=Project";
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlDataReader reader;
        //List<Customer> TestList = new List<Customer>();
        //Customer test = null;

        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _hostEnvironment;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _db = db;
            //this._hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var displaydata = _db.coaches.ToList();
            var feedbacks = _db.FeedbackTable.ToList();
            ViewBag.feeds = displaydata;
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

        //Get items details action method
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = _db.coaches.FirstOrDefault(c => c.CoachID == id);
            if (item == null)
            {
                return NotFound();
            }

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
                action4 = "logout";

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

            ViewItemDetailsModel viewItemDetailsModel = new ViewItemDetailsModel();
            viewItemDetailsModel.CoachID = item.CoachID;
            viewItemDetailsModel.Cname = item.Cname;
            viewItemDetailsModel.CAge = item.CAge;
            viewItemDetailsModel.contactnumber = item.contactnumber;
            viewItemDetailsModel.Email = item.Email;
            viewItemDetailsModel.Description = item.Description;
            viewItemDetailsModel.Rate = item.Rate;
            viewItemDetailsModel.ImageName = item.ImageName;
            return View(viewItemDetailsModel);
        }
    }
}
