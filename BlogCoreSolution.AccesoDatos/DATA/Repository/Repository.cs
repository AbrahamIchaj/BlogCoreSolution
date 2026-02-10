using BlogCoreSolution.AccesoDatos.DATA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlogCoreSolution.AccesoDatos.DATA.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly DbContext Context;
        internal DbSet<T> dbset;

        public Repository(DbContext context)
        {
            Context = context;
            this.dbset = Context.Set<T>();
        }

        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public T Get(int id)
        {
            return dbset.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // se incluye propiedades de navegación

            if (includeProperties != null)
            {
                // Se divide la cadena de propiedares por comas y se itera sobre ellas
                foreach (var includeProperty in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                    query = query.Include(includeProperty); 
                }
            }

            // Se aplica el ordenamiento si se proporciona
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            //Si no se proporciona ordenamiento, simplemente se convierte la consulta en una lista
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            // Se crea una consulta IQueryable a partir del DbSet del contexto
            IQueryable<T> query = dbset;


            //Se aplica el filtro si se proporciona
            if(filter != null)
            {
                query = query.Where(filter);
            }

            // Se incluyen propiedades de navegación si se proporcionan
            if (includeProperties != null)
            {
                // Se divide la cadena de propiedades por comas y se itera sobre ellas
                foreach (var includeProperty in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            // Si no se proporciona ordenamiento, simplemente se convierte la consulta en una lista

            return query.FirstOrDefault();



        }

        public void Remove(int id)
        {
            T entityToRemove = dbset.Find(id);
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

 
    }
}
