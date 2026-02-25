using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCoreSolution.Models.ViewModels
{
    public class ArticuloVM
    {

        public Articulo articulo { get; set; }

        public IEnumerable<SelectListItem> ListaCategorias { get; set; }


    }
}
