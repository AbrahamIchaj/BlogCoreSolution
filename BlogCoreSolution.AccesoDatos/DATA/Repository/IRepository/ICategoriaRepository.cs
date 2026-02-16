using BlogCoreSolution.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.AccesoDatos.DATA.Repository.IRepository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
           void Update(Categoria categoria);
    }
}
