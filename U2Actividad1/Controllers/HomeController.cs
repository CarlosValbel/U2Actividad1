using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using U2Actividad1.Models.ViewModels;

namespace U2Actividad1.Controllers
{
    public class HomeController : Controller
    {
        U2Actividad1.Models.Entities.AnimalesContext context = new();
        [Route("/")]
        [Route("/Principal")]
        public IActionResult Index()
        {
            var clases = context.Clases.OrderBy(x => x.Nombre);
            return View(clases);
        }
        [Route("/{id}")]
        public IActionResult Clase(string Id)
        {
            var especies = context.Especies
                .Include(x => x.IdClaseNavigation)
                .Where(x => x.IdClaseNavigation.Nombre == Id)
                .OrderBy(x => x.Especie)
                .Select(x => new ClaseViewModel { Id = x.Id, Nombre = x.Especie, Clase = x.IdClaseNavigation.Nombre });

            if (especies.Count() > 0)
            {
                return View(especies);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [Route("/{clase}/{id}")]
        public IActionResult Especie(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("Index");
            }

            id = id.Replace("-", " ");
            var especie = context.Especies.Include(x => x.IdClaseNavigation)
                .FirstOrDefault(x => x.Especie == id);

            if (especie == null)
                return RedirectToAction("Index");
            return View(especie);
        }
    }
}
