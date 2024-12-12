using DeskSensorRESTService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeskSensorRESTServiceTests.Models
{
    [TestClass()]
    [DoNotParallelize]
    public class DeskRepoDbTests
    {
        private const bool useDatabase = true;
        private static IDesk _repo;
        // https://learn.microsoft.com/en-us/dotnet/core/testing/order-unit-tests?pivots=mstest

     
        [TestInitialize]
        public void Init()
        {

            if (useDatabase)
            {
                var optionsBuilder = new DbContextOptionsBuilder<DeskDbContext>();
                // https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets
                optionsBuilder.UseSqlServer("Data Source=mssql12.unoeuro.com;Initial Catalog=auden_dk_db_eksamen;User ID=auden_dk;Password=5pFwR4c9bfEDGe3Bdymh;TrustServerCertificate = True");

                DeskDbContext _dbContext = new(optionsBuilder.Options);
              
                // clean database table: remove all rows
                _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Desk");
                _repo = new DeskRepoDb(_dbContext);
            }
            else
            {
                //_repo = new DeskRepo();
            }
        }

        [TestMethod, Priority(1)]
        [DoNotParallelize]
        public void AddTest()
        {
            var deskFalse = _repo.Add(new Desk { Name = "", Occupied = true });
            var deskTrue = _repo.Add(new Desk { Name = "TestTrue", Occupied = true });

            //allowed
            Assert.IsTrue(_repo.Get().Contains(deskTrue));
            Assert.AreEqual("TestTrue", deskTrue.Name);
            Assert.AreEqual(true, deskTrue.Occupied);

            //not allowed
            Assert.IsFalse(_repo.Get().Contains(deskFalse));
            Assert.IsNull(ArgumentOutOfRangeException();
        }

        [TestMethod, Priority(2)]
        public void GetByIdTest()
        {
            Desk m = _repo.Add(new Desk { Name = "Desk1", Occupied = true });
            Desk? desk = _repo.GetById(m.Id);

            // allowed
            Assert.IsNotNull(desk);
            Assert.AreEqual("Desk1", desk.Name);
            Assert.AreEqual(true, desk.Occupied);

            //not allowed
            Assert.IsNull(_repo.GetById(-1));
            Assert.IsTrue
        }
        [TestMethod, Priority(3)]
        [DoNotParallelize]
        public void RemoveTest()
        {
            Desk m = _repo.Add(new Desk { Name = "Desk1", Occupied = true });
            Desk? desk = _repo.Delete(m.Id);
            Assert.IsNotNull(desk);
            Assert.AreEqual("Desk1", desk.Name);

            Desk? desk2 = _repo.Delete(m.Id);
            Assert.IsNull(desk2);
        }

        [TestMethod, Priority(4)]
        [DoNotParallelize]
        public void UpdateTest()
        {
            Desk m = _repo.Add(new Desk { Name = "Desk1", Occupied = true });
            Desk? desk = _repo.Update(m.Id, new Desk { Name = "Desk1", Occupied = false });
            Assert.IsNotNull(desk);
            Desk? desk2 = _repo.GetById(m.Id);
            Assert.AreEqual(false, desk.Occupied);
        }
    }
}