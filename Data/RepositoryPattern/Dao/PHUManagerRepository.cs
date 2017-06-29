using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryPattern.Dao
{
    public class PHUManagerRepository : Repository<PhuManager>
    {
        public PHUManagerRepository(DbContext context) : base(context)
        {
        }
    }
}
