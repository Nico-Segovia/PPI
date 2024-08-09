using OrdenesInversionAPI.Models;
using System.Threading.Tasks;

namespace OrdenesInversionAPI.Services
{
    public interface IUserService
    {
        Task<Usuario> Authenticate(string username, string password, string scheme);
    }

}
