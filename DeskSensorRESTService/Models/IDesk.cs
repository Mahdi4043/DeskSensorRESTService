namespace DeskSensorRESTService.Models
{
    public interface IDesk 
    {
        Desk Add(Desk desk);
        List<Desk> Get();
        Desk? GetById(int id);
        Desk Delete(int id);
        Desk Update(int id, Desk desk);
    }
}
