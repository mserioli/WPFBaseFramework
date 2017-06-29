using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryPattern.Services
{
    public interface IPHUManagerService<T> where T : PhuManager
    {
        
        List<T> findAll(string PHUID, string status);
    }
}
