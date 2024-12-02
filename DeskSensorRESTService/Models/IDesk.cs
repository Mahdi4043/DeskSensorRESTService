namespace DeskSensorRESTService.Models
{
    public interface IDesk
    {
        Desk Add(Desk desk);
        IEnumerable<Desk> Get();
        Desk? GetById(int id);
        Desk Delete(int id);
        Desk Update(int id, Desk desk);
    }
}
