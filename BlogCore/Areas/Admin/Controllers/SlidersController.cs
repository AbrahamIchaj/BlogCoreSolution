using BlogCoreSolution.AccesoDatos.DATA.Repository.IRepository;
using BlogCoreSolution.Models;
using BlogCoreSolution.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    
        [Area("Admin")]
        public class SlidersController : Controller
        {
           
            private readonly IContenedorTrabajo _contenedorTrabajo;
            private readonly IWebHostEnvironment _hostingEnvironment;


        public SlidersController(IContenedorTrabajo contenedorTrabajo,
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
            SliderVM slidVM = new SliderVM()
            {
                slider = new BlogCoreSolution.Models.Slider()
            };

            return View();
            }

        //CREAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SliderVM slidVM)
        {

            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos2 = HttpContext.Request.Form.Files;
                if (slidVM.slider.Id == 0 && archivos2.Count > 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes/slider");
                    var extension = Path.GetExtension(archivos2[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos2[0].CopyTo(fileStreams);
                    }

                    slidVM.slider.UrlImagen = $"/imagenes/slider/{nombreArchivo}{extension}";

                    _contenedorTrabajo.Slider.Add(slidVM.slider);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }

            }

            return View(slidVM);
        }

        //EDITAR

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            SliderVM slidVM = new SliderVM()
            {
                slider = new BlogCoreSolution.Models.Slider()

            }; 

            if (id != null)
            {
                slidVM.slider = _contenedorTrabajo.Slider.Get(id.GetValueOrDefault());
            }

            return View(slidVM);
        }

        //EDITAR IMG
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SliderVM slidVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var sliderDesdeBd = _contenedorTrabajo.Slider.Get(slidVM.slider.Id);
                if (sliderDesdeBd == null)
                {
                    return NotFound();
                }

                if (archivos.Count > 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes/slider");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = !string.IsNullOrWhiteSpace(sliderDesdeBd.UrlImagen)
                        ? Path.Combine(rutaPrincipal, sliderDesdeBd.UrlImagen.TrimStart('\\', '/'))
                        : null;

                    if (rutaImagen != null && System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    slidVM.slider.UrlImagen = $"/imagenes/slider/{nombreArchivo}{extension}";
                }
                else
                {
                    slidVM.slider.UrlImagen = sliderDesdeBd.UrlImagen;
                }

                _contenedorTrabajo.Slider.Update(slidVM.slider);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(slidVM);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Slider.Get(id);
            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, objFromDb.UrlImagen.TrimStart('\\', '/'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error borrando slider" });
            }
            _contenedorTrabajo.Slider.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Slider borrado correctamente" });
        }

        #region Llamadas a la API
        public IActionResult GetAll()
        {
            var data = _contenedorTrabajo.Slider.GetAll()
    .Select(s => new {
        s.Id,
        s.Nombre,
        Estado = s.Estado,
        urlImagen = string.IsNullOrWhiteSpace(s.UrlImagen)
            ? null
            : s.UrlImagen.Replace("\\", "/").TrimStart('/')
        });
            return Json(new { data });
        }
        
        
        
        #endregion


    }
}

