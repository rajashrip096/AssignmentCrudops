using AssignmentCrudops.DAL;
using AssignmentCrudops.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentCrudops.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration configuration;
        UserDAL userdal;
        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
            userdal = new UserDAL(configuration);
        }
        // GET: UserController
        public ActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                User res = userdal.GetUserByEmail(user);
                if (res != null && res.Password == user.Password)
                {
                    HttpContext.Session.SetString("email", user.Email);
                    return RedirectToAction("MyDashboard", "Dashboard");
                }

                else
                    return View();
            }
            catch (Exception ex)
            {
                return View();
            }

        }
        public ActionResult Index()
        {
            var user = userdal.GetAllUser();
            return View(user);
        }
        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            var user = userdal.GetUserById(id);
            return View(user);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User objuser)
        {
            try
            {
                int res = userdal.AddUser(objuser);
                if (res == 1)
                    return RedirectToAction(nameof(Index));
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            var user = userdal.GetUserById(id);
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            try
            {
                int res = userdal.UpdateUser(user);
                if (res == 1)
                    return RedirectToAction(nameof(Index));
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            var user = userdal.GetUserById(id);
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int res = userdal.DeleteUser(id);
                if (res == 1)
                    return RedirectToAction(nameof(Index));
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }
    }
}