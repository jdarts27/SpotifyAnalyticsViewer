using FinalProjectSpotifyVisualizer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectSpotifyVisualizer.Services
{
    public interface ISpotifyTopTracks
    {
        Task<IEnumerable<TopTracks>> GetNewTopTracks(string playlistId, int limit, string accessToken);
    }
}
