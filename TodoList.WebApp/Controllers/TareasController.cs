using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.WebApp.Models;

namespace TodoList.WebApp.Controllers
{
    public class TareasController : Controller
    {
        // GET: TareasDetails
        public ActionResult Index()
        {
            List<TareasModel> tareas = new List<TareasModel>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7138/api/");
                var responseTask = client.GetAsync("Tarea/Get-Tareas");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<List<TareasModel>>();
                    readTask.Wait();
                    tareas = readTask.Result;
                }
                else
                {
                    tareas = Enumerable.Empty<TareasModel>().ToList();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View();
        }

        // GET: TareasDetails/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: TareasDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TareasDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TareasDetails/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TareasDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TareasDetails/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TareasDetails/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
