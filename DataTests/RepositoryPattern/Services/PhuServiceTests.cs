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
    public class PhuServiceTests
    {
        private PHURepository _phuRepository;
        private PhuService _phuService;
        private WPFBaseFrameworkEntities _dbContext;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbContext = new WPFBaseFrameworkEntities();
            _phuRepository = new PHURepository(_dbContext);
            _phuService = new PhuService(_phuRepository);
        }

        /*
        [TestMethod()]
        public void PhuServiceTest()
        {
            Assert.Fail();
        }
        */

        [TestMethod()]
        public async void FindAll()
        {
            var phus = await _phuService.FindAll();
            Assert.AreEqual(phus.Count(), 1);
        }

    }
}