using Microsoft.AspNetCore.Mvc;
using Premisson.Northwind.Data.Acces.Concreate.EntityFramework;
using Premisson.Northwind.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Premisson.Northwind.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        NorthwindContext db = new NorthwindContext();

        [HttpGet]
        public ActionResult Login()
        {
            return Ok();
        }
        [HttpPost]
        public ActionResult Login(User u )
        {
            string e = u.Email;
            string p = u.Password;

            var kontrol = db.Users.FirstOrDefault(x => x.Email == e && x.Password == p);
            if (kontrol != null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View("Login");
            }


        }
    }
}
