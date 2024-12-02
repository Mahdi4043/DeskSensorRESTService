using DeskSensorRESTService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskSensorRESTService.Models
{
    public class DeskRepoDb : IDesk
    {
        private readonly DeskDbContext _context;

        public DeskRepoDb(DeskDbContext dbContext)
        {
            _context = dbContext;
        }


        public Desk Add(Desk desk)
        {
            //if( desk.Name == _context.Desk.FirstOrDefault(m => m.Name == desk.Name)?.Name)
            //{
            //    throw new ArgumentException("Name already exists");
            //}
            desk.Id = 0;
            _context.Add(desk);
            _context.SaveChanges();
            return desk;
        }

        public List<Desk> Get()
        {
            //List<Movie> result = _context.Movies.ToList();
            List<Desk> query = _context.Desk.ToList();
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
            _context.SaveChanges();
            return exsisting;
        }

        public Desk? UpdateOccupied(int id, Desk updatedDesk)
        {
            var desk = _context.Desk.Find(id);
            if (desk != null)
            {
                desk.Occupied = updatedDesk.Occupied;
                _context.SaveChanges();
            }
            return desk;
        }
    }
}
