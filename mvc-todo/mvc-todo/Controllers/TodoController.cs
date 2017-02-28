using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mvc_todo.Models;

namespace mvc_todo.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoDbContext _db = new ToDoDbContext();

        // GET: ToDoItems
        public ActionResult Index()
        {
            return View(_db.ToDoItems.ToList());
        }

        // GET: ToDoItems/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var toDoItem = _db.ToDoItems.Find(id);
        //    if (toDoItem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(toDoItem);
        //}

        // GET: ToDoItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoItems/Create
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

        // GET: ToDoItems/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: ToDoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: ToDoItems/Delete/5
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

        // POST: ToDoItems/Delete/5
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