using FinalProjectSpotifyVisualizer.Models;
using FinalProjectSpotifyVisualizer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectSpotifyVisualizer.Pages
{
    public class PrintAlbumsModel : PageModel
    {
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IConfiguration _configuration;
        private readonly ISpotifyService _spotifyService;

        public PrintAlbumsModel(
            ISpotifyAccountService spotifyAccountService,
            IConfiguration configuration,
            ISpotifyService spotifyService)
        {
            _spotifyAccountService = spotifyAccountService;
            _configuration = configuration;
            _spotifyService = spotifyService;
        }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<Release> NewReleases { get; set; }

        [BindProperty(SupportsGet = true)]
        public ReleasesInputModel InputInfo { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var newReleases = await GetReleases(InputInfo.CountryCode, InputInfo.Limit);

            if(newReleases == null)
            {
                return NotFound();
            }
            NewReleases = newReleases;

            return Page();
        }

        private async Task<IEnumerable<Release>> GetReleases(string countryCode, int limit)
        {
            try
            {
                var token = await _spotifyAccountService.GetToken(
                    _configuration["Spotify:ClientId"],
                    _configuration["Spotify:ClientSecret"]);

                if(countryCode == null)
                {
                    countryCode = "US";
                }
                if(limit <= 0)
                {
                    limit = 10;
                }

                var newReleases = await _spotifyService.GetNewReleases(countryCode, limit, token);
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
