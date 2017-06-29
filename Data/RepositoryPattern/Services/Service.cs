using Data.RepositoryPattern.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryPattern.Services
{
    public class Service<T> : IService<T> where T : class
    {
        internal Repository<T> _repository;

        public Service()
        {

        }
        public Service(Repository<T> _repository)
        {
            this._repository = _repository;
        }

        public virtual void Create(T entity)
        {
            _repository.Create(entity);
        }

        public virtual void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public virtual void Delete(object PK)
        {
            _repository.Delete(PK);
        }

        public async virtual Task<IEnumerable<T>> FindAll()
        {
            return await _repository.FindAll();
        }

        public async virtual Task<T> FindByPK(object PK)
        {
            return await _repository.FindByPK(PK);
        }

        public virtual void Update(T entity)
        {
            _repository.Update(entity);
        }
    }
}
