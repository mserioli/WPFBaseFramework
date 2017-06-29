using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.RepositoryPattern.Dao;
using System.Data.Entity;

namespace Data.RepositoryPattern.Services
{
    public class PhuService : Service<PHU>
    {

        //private readonly Repository<PHU> _phuRepository;
        private readonly DbContext _dbContext;
        public PhuService() 
        {
            this._dbContext = new WPFBaseFrameworkEntities();
            this._repository = new PHURepository(_dbContext);
           
            
        }

        public PhuService(Repository<PHU> _repository) : base(_repository)
        {
            //this._repository = _phuRepository;
        }
    }
}
