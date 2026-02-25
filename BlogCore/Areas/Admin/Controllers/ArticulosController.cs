using BlogCoreSolution.AccesoDatos.DATA.Repository.IRepository;
using BlogCoreSolution.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ArticulosController : Controller
    {

        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticulosController(IContenedorTrabajo contenedorTrabajo,
            IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticuloVM artiVM = new ArticuloVM()
            {
                articulo = new BlogCoreSolution.Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategoria()
            };

            return View(artiVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM artiVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (artiVM.articulo.Id == 0 && archivos.Count > 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    artiVM.articulo.UrlImagen = $"imagenes/articulos/{nombreArchivo}{extension}";

                    _contenedorTrabajo.Articulo.Add(artiVM.articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }

            }

            artiVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategoria();
            return View(artiVM);
        }



        #region Llamadas a la API
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria") });
        }

        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{
        //    var objFromDb = _contenedorTrabajo.Categoria.Get(id);
        //    if (objFromDb == null)
        //    {
        //        return Json(new { success = false, message = "Error borrando categoria" });
        //    }
        //    _contenedorTrabajo.Categoria.Remove(objFromDb);
        //    _contenedorTrabajo.Save();
        //    return Json(new { success = true, message = "Categoria borrada correctamente" });
        //}

        #endregion
    }
}
