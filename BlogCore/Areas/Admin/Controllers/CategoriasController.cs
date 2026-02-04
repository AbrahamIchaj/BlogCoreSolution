using BlogCoreSolution.AccesoDatos.DATA.Repository;
using BlogCoreSolution.AccesoDatos.DATA.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

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


        //#region Llamadas a la API
        //public IActionResult GetAll()
        //{
        //    return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
        //}

        //#endregion
    }
}
