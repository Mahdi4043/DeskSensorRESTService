using DeskSensorRESTService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayGroundLib
{
    public class DeskRepositoryDb : IDesk
    {
        private readonly DeskDbContext _context;
        public DeskRepositoryDb(DeskDbContext dbContext)
        {
            _context = dbContext;
        }

        public Desk Add(Desk desk)
        {

            desk.Id = 0;
            _context.Desk.Add(desk);
            _context.SaveChanges();
            return desk;
        }

        public IEnumerable<Desk> Get()
        {
            //List<Movie> result = _context.Movies.ToList();
            IEnumerable<Desk> query = _context.Desk.ToList();
            // Copy ToList()
            return query;
        }

        public Desk? GetById(int id)
        {
            return _context.Desk.FirstOrDefault(m => m.Id == id);
        }

        public Desk? Delete(int id)
        {
                Desk? desk = GetById(id);
            if (desk is null)
            {
                return null;
            }
            _context.Desk.Remove(desk);
            _context.SaveChanges();
            return desk;
        }

        public Desk? Update(int id, Desk updatedDesk)
        // https://www.learnentityframeworkcore.com/dbcontext/modifying-data
        {

            Desk? exsisting = GetById(id);
            if (exsisting == null)
            {
                return null;
            }
            exsisting.Name = updatedDesk.Name;
            exsisting.Occupied = updatedDesk.Occupied;
            return exsisting;
        }
    }
}
