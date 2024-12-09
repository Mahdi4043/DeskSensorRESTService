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
            // Check if a desk with the same name already exists in the context using LINQ
            var existingDesk = _context.Desk.FirstOrDefault(d => d.Name == desk.Name);
            if (existingDesk != null)
            {
                UpdateOccupied(desk); // Call the method if a desk with the same name is found
            }

            desk.Id = 0; // Ensure the ID is reset for a new entry
            _context.Add(desk);
            _context.SaveChanges();
            return desk;
        }

        public List<Desk> Get()
        {
            List<Desk> query = _context.Desk.ToList();
            return query; 
        }

        public Desk? GetById(int id)
        {
            if(id == 0)
            {
                throw new ArgumentException("Id cannot be 0.");  
            }

            return _context.Desk.FirstOrDefault(m => m.Id == id);
        }

        public Desk Delete(int id)
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

        public Desk Update(int id, Desk updatedDesk)
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

        public Desk? UpdateOccupied(Desk updatedDesk)
        {
            var desk = _context.Desk.Find(updatedDesk);
            if (desk != null)
            {
                desk.Occupied = updatedDesk.Occupied;
                _context.SaveChanges();
            }
            return desk;
        }
    }
}
