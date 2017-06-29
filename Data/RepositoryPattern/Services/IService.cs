using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryPattern.Services
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> FindAll();
        void Delete(object PK);
        void Delete(T entity);
        void Update(T entity);
        Task<T> FindByPK(object PK);
        void Create(T entity);
    }
}
