using BlogCoreSolution.AccesoDatos.DATA.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using BlogCoreSolution.Models;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                //Logica para guardar en la BD
                _contenedorTrabajo.Categoria.Add(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));

            }
            return View();
        }


        #region Llamadas a la API
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
        }

        #endregion
    }
}
