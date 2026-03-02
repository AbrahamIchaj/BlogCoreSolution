using BlogCoreSolution.AccesoDatos.DATA.Repository.IRepository;
using BlogCoreSolution.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;

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
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes/articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    artiVM.articulo.UrlImagen = $"/imagenes/articulos/{nombreArchivo}{extension}";

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


  

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticuloVM artiVM = new ArticuloVM()
            {
                articulo = new BlogCoreSolution.Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategoria()
            }; 

            if (id != null)
            {
                artiVM.articulo = _contenedorTrabajo.Articulo.Get(id.GetValueOrDefault());
            }

            return View(artiVM);
        }

        //EDITAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM artiVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var articuloDesdeBd = _contenedorTrabajo.Articulo.Get(artiVM.articulo.Id);

                if (archivos.Count > 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes/articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, articuloDesdeBd.UrlImagen.TrimStart('\\', '/'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //Subimos el nuevo archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    artiVM.articulo.UrlImagen = $"/imagenes/articulos/{nombreArchivo}{extension}";
                }
                else
                {
                    artiVM.articulo.UrlImagen = articuloDesdeBd.UrlImagen;
                }

                _contenedorTrabajo.Articulo.Update(artiVM.articulo);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));

            }

            artiVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategoria();
            return View(artiVM);
        }



        //AREA
        #region Llamadas a la API
        public IActionResult GetAll()
        {
            var data = _contenedorTrabajo.Articulo
                .GetAll(includeProperties: "Categoria")
                .Select(a => new {
                    a.Id,
                    a.Nombre,
                    categoria = a.Categoria,
                    urlImagen = string.IsNullOrWhiteSpace(a.UrlImagen)
                        ? null
                        : a.UrlImagen.Replace("\\", "/").TrimStart('/')
                });
            return Json(new { data });
        }





        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Articulo.Get(id);
            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, objFromDb.UrlImagen.TrimStart('\\', '/'));
            
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }
            
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error borrando artículo" });
            }
            _contenedorTrabajo.Articulo.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Artículo borrada correctamente" });
        }

        #endregion
    }
}
