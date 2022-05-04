using System.Threading.Tasks;

namespace FinalProjectSpotifyVisualizer.Services
{
    public interface ISpotifyAccountService
    {
        Task<string> GetToken(string clientId, string clientSecret);
    }
}
