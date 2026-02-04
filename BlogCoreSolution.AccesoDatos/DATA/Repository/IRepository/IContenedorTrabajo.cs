using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.AccesoDatos.DATA.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        ICategoriaRepository Categoria { get; }

        void Save(); 
    }

   
}
