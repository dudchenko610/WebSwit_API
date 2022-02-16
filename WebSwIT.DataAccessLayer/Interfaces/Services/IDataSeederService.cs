using System.Threading.Tasks;

namespace WebSwIT.DataAccessLayer.Interfaces.Services
{
    public interface IDataSeederService
    {
        Task SeedDataAsync();
    }
}
