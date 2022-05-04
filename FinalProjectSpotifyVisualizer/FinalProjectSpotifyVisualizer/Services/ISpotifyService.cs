using FinalProjectSpotifyVisualizer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectSpotifyVisualizer.Services
{
    public interface ISpotifyService
    {
        Task<IEnumerable<Release>> GetNewReleases(string countryCode, int limit, string accessToken);
    }
}
