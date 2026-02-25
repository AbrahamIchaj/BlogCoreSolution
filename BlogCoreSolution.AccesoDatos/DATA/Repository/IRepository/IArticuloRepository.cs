using BlogCoreSolution.Models;

    namespace BlogCoreSolution.AccesoDatos.DATA.Repository.IRepository
{
    public interface IArticuloRepository : IRepository<Articulo>
    {
        void Update(Articulo articulo);
    }
}
