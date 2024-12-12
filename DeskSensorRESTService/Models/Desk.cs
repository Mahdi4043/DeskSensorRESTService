using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace DeskSensorRESTService.Models
{
    public class Desk 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool Occupied { get; set; }


        public void validateName()
        {
            if (Name == null || Name.Length == 0)
            {
                throw new ArgumentOutOfRangeException( "Desk name cannot be empty.");
            }
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Occupied: {Occupied}";
        }
    }
}
