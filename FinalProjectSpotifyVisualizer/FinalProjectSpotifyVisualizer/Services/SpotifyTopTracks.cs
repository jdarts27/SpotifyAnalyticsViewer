using FinalProjectSpotifyVisualizer.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Linq;

namespace FinalProjectSpotifyVisualizer.Services
{
    public class SpotifyTopTracks : ISpotifyTopTracks
    {
        private readonly HttpClient _httpClient;
        public SpotifyTopTracks(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<IEnumerable<TopTracks>> GetNewTopTracks( string playlistId, int limit, string accessToken)
        {
            playlistId = "37i9dQZEVXbMDoHDwVN2tF";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"playlists/{playlistId}/tracks?playlist_id={playlistId}&limit={limit}");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<GetTopTracks>(responseStream);

            return responseObject?.playlistItems.Select(i => new TopTracks
            {
                Track = i.track.name,
                Artist = string.Join(",", i.track.artists.Select(i => i.name)),
                Album = i.track.album.name,
                ImageUrl = i.track.album.images.FirstOrDefault().url,
                AlbumLink = i.track.album.external_urls.spotify,
                ArtistLink = string.Join(",", i.track.artists.Select(i => i.external_urls4))
            });
        }
    }
}
