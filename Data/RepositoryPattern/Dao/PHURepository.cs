using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryPattern.Dao
{
    public class PHURepository : Repository<PHU>
    {
        public PHURepository(DbContext context) : base(context)
        {
        }
    }
}
