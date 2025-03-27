using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoList.WebAppp.Models;
using ToDoList.Web.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoList.Web.Controllers
{
    public class TareaVistaController : Controller
    {
        private readonly TareaService _TareaService;

        public TareaVistaController()
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
                return View(new List<TareaModelsView>());
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
                return View(new TareaModelsView());
            }
        }

        public async Task<ActionResult> Create(TareaModelsView model)
        {
            TareaModelsView modelInternal = new TareaModelsView();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7138/api/");
                var responce = await client.GetAsync("Set-Tarea");
                modelInternal = await responce.Content.ReadFromJsonAsync<TareaModelsView>();
                if (!responce.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }

                var entity = new ModelCreate()
                {
                    Nombre = modelInternal.Nombre,
                    Contenido = modelInternal.Contenido,
                    Estado = modelInternal.Estado,
                    idUsuario = modelInternal.idUsuario
                };

                return View(modelInternal);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ModelCreate model)
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
                return View(new TareaModelsView());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TareaModelsView model)
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