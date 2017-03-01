using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mvc_todo.Models;
using System;

namespace mvc_todo.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoDbContext _db = new ToDoDbContext();

        public ActionResult Index()
        {
            return View(_db.ToDoItems.Where(a=> a.Id == 1).Include("BADSTUFF").ToList());
        }
        
        public ActionResult Create()
        {
            return View();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Item,Completed")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                _db.ToDoItems.Add(toDoItem);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toDoItem);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var toDoItem = _db.ToDoItems.Find(id);
            if (toDoItem == null)
                throw new Exception($"No To-Do found with item number [{id}]!");

            return View(toDoItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Item,Completed")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(toDoItem).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDoItem);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var toDoItem = _db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var toDoItem = _db.ToDoItems.Find(id);
            if (toDoItem != null)
            {
                _db.ToDoItems.Remove(toDoItem);
                _db.SaveChanges();
            }                
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}