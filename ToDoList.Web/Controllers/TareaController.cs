using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Web.Models.Tarea;
using ToDoList.Web.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoList.Web.Controllers
{
    public class TareaController : Controller
    {
        private readonly TareaService _TareaService;

        public TareaController()
        {
            _TareaService = new TareaService();
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var tasks = await _TareaService.GetTasksAsync();
                return View(tasks);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(new List<TareaModel>());
            }
        }

        public async Task<IActionResult> Details(string nombre)
        {
            try
            {
                var task = await _TareaService.GetTaskByTitleAsync(nombre);
                return View(task);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(new TareaModel());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TareaModel model)
        {
            try
            {
                var result = await _TareaService.CreateTaskAsync(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = result.Message;
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(string nombre)
        {
            try
            {
                var task = await _TareaService.GetTaskByTitleAsync(nombre);
                return View(task);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(new TareaModel());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TareaModel model)
        {
            try
            {
                var result = await _TareaService.UpdateTaskAsync(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = result.Message;
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _TareaService.DeleteTaskAsync(id);
                if (!result.Success)
                {
                    ViewBag.Message = result.Message;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}