using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021010.Web.Controllers
{
   
    public class TestController : Controller
    {
        [HttpGet]
        public ActionResult Input()
        {
            Person p = new Person()
            {
                Birthday = new DateTime(1990, 11, 28)
            };
            return View(p);
        }

        [HttpPost]
        public ActionResult Input(Person p)// (string name, Datetime Birthday, float salary)
        {
            var data = new
            {
                Name = p.Name,
                Birthday = string.Format("{0:MM/dd/yyyy}", p.Birthday),
                Salary = p.Salary
            };

            return Json(p, JsonRequestBehavior.AllowGet);
        }

        public string TestDate( DateTime value)
        {
            DateTime d = value;
            return string.Format("{0:dd/MM/yyyy}", d);
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public float Salary { get; set; }
    }
}