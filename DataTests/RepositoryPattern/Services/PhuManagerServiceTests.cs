using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.RepositoryPattern.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.RepositoryPattern.Dao;

namespace Data.RepositoryPattern.Services.Tests
{
    
    [TestClass()]
    public class PhuManagerServiceTests
    {

        private PHUManagerRepository _phuManagerRepository;
        private PhuManagerService _phuManagerService;
        private WPFBaseFrameworkEntities _dbContext;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbContext = new WPFBaseFrameworkEntities();
            _phuManagerRepository = new PHUManagerRepository(_dbContext);
            _phuManagerService = new PhuManagerService(_phuManagerRepository);
        }

        /*
        [TestMethod()]
        public void PhuManagerServiceTest()
        {
            
        }
        */

        [TestMethod()]
        public async void FindAllTest()
        {
            IEnumerable<PhuManager> list =  await _phuManagerService.FindAll();
            Console.WriteLine(list.Count());
        }
    }
}