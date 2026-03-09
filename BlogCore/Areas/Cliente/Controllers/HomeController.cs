using BlogCore.Models;
using BlogCoreSolution.AccesoDatos.DATA.Repository;
using BlogCoreSolution.AccesoDatos.DATA.Repository.IRepository;
using BlogCoreSolution.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogCore.Areas.Cliente.Controllers
{

    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public HomeController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;


        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Sliders = _contenedorTrabajo.Slider.GetAll(),
                ListArticulos = _contenedorTrabajo.Articulo.GetAll()
            };

            ViewBag.IsHome = true;
            return View(homeVM);
        }

        [HttpGet]
        public IActionResult Detalle(int id)
        {
            var articuloDesdeBd = _contenedorTrabajo.Articulo.Get(id);
            return View(articuloDesdeBd);

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
