using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using mvc_todo.Models;
using System;
using Flogging.Web.Filters;
using Flogging.Web;
using System.Configuration;
using System.Data.SqlClient;
using Flogging.Data;
using Dapper;
using System.Data;

namespace mvc_todo.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoDbContext _db = new ToDoDbContext();

        [TrackUsage(Constants.ProductName, Constants.LayerName, "View Todos")]
        public ActionResult Index()
        {
            //var idParam = new SqlParameter("@Id", "efbadid");
            //var itemParam = new SqlParameter("@Item", "Entity Framework updated todo");
            //_db.Database.SqlQuery<int>("UpdateToDoWithError @Id, @Item", idParam, itemParam).ToList();

            var connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (var db = new SqlConnection(connStr.ConnectionString))
            {
                db.Open();

                //var p = new DynamicParameters();
                //p.Add("@Id", "badid");
                //p.Add("@Item", "Dapper-looking todo");
                db.DapperProcNonQuery("UpdateTodoWithError", new { Id = "badid", Item = "Dapper-looking todo" });
                //db.Execute("UpdateTodoWithError", p, commandType: CommandType.StoredProcedure);

                //    //var sp = new Sproc(db, "UpdateTodoWithError");
                //    //sp.SetParam("@Id", "hello there!");
                //    //sp.SetParam("@Item", "here is some new todo text");
                //    //sp.ExecNonQuery();
            }

                Helper.LogWebDiagnostic(Constants.ProductName, Constants.LayerName, "hello from todo index");
            //return View(_db.ToDoItems.Where(a=> a.Id == 1).Include("BADSTUFF").ToList());
            
            return View(_db.ToDoItems.ToList());

        }

        public ActionResult Create()
        {
            return View();
        }

        [TrackUsage(Constants.ProductName, Constants.LayerName, "Create Todo")]        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToDoItem toDoItem)
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

        [TrackUsage(Constants.ProductName, Constants.LayerName, "Update Todo")]
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

        [TrackUsage(Constants.ProductName, Constants.LayerName, "Remove Todo")]
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