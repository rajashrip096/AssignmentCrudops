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
    public class TaskController : Controller
    {
        private readonly IConfiguration configuration;
        TaskDAL taskdal;
        public TaskController(IConfiguration configuration)
        {
            this.configuration = configuration;
            taskdal = new TaskDAL(configuration);
        }
        // GET: UserController
        
       
      
        public ActionResult Index()
        {
            var task = taskdal.GetAllTask();
            return View(task);
        }
        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            var task = taskdal.GetTaskById(id);
            return View(task);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tasks task)
        {
            try
            {
                int res = taskdal.AddTask(task);
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
            var task = taskdal.GetTaskById(id);
            return View(task);
         
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tasks t)
        {
            try
            {
                int res = taskdal.UpdateTask(t);
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
            var task = taskdal.GetTaskById(id);
            return View(task);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int res = taskdal.DeleteTask(id);
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
