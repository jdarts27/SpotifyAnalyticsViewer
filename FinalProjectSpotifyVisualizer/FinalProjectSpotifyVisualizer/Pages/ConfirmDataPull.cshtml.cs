using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using FinalProjectSpotifyVisualizer.Models;
using FinalProjectSpotifyVisualizer.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace FinalProjectSpotifyVisualizer.Pages
{
    public class ConfirmDataPullModel : PageModel
    {
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IConfiguration _configuration;
        private readonly ISpotifyTopTracks _spotifyTopTracks;

        public ConfirmDataPullModel(
            ISpotifyAccountService spotifyAccountService,
            IConfiguration configuration,
            ISpotifyTopTracks spotifyTopTracks)
        {
            _spotifyAccountService = spotifyAccountService;
            _configuration = configuration;
            _spotifyTopTracks = spotifyTopTracks;
        }
        [BindProperty(SupportsGet = true)]
        public IEnumerable<TopTracks> NewTracks { get; set; }

        [BindProperty(SupportsGet = true)] 
        public DataFormModel Checked { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Limit { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var newTracks = await GetTopTracks("37i9dQZEVXbMDoHDwVN2tF", Limit);

            if(newTracks == null)
            {
                return NotFound();
            }

            NewTracks = newTracks;

            return Page();
        }

        private async Task<IEnumerable<TopTracks>> GetTopTracks(string playlistId, int limit)
        {
            try
            {
                var token = await _spotifyAccountService.GetToken(
                    _configuration["Spotify:ClientId"],
                    _configuration["Spotify:ClientSecret"]);

                if (limit <= 0 || limit >= 50)
                {
                    limit = 10;
                }

                var newReleases = await _spotifyTopTracks.GetNewTopTracks(playlistId, limit, token);
                return newReleases;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
