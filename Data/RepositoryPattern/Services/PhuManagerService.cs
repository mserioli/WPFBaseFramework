using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.RepositoryPattern.Dao;

namespace Data.RepositoryPattern.Services
{
    public class PhuManagerService : Service<PhuManager>
    {

        private Repository<PhuManager> _phuManagerRepository;

        public PhuManagerService(Repository<PhuManager> _phuManagerRepository) : base(_phuManagerRepository)
        {
            this._phuManagerRepository = _phuManagerRepository;
        }
    }
}
