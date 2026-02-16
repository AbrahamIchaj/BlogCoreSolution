using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogCoreSolution.AccesoDatos.DATA
{
    public class ApplicationDbContext : IdentityDbContext 
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Articulo> Articulos { get; set; }
    }
}
