using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCoreSolution.AccesoDatos.DATA.Repository.IRepository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        IEnumerable<SelectListItem> GetListaCategoria();
        void Update(Categoria categoria);
    }
}
