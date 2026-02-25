using BlogCoreSolution.AccesoDatos.DATA.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCoreSolution.AccesoDatos.DATA.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaCategoria()
        {
            return _db.Categorias.Select(c => new SelectListItem()
            {
                Text = c.Nombre,
                Value = c.Id.ToString()
            });

        }

        public void Update(Categoria categoria)
        {
            var objDesdeDb = _db.Categorias.FirstOrDefault(s => s.Id == categoria.Id);
            objDesdeDb.Nombre = categoria.Nombre;
            objDesdeDb.Orden = categoria.Orden;

            //_db.SaveChanges();
        }
    }
}
