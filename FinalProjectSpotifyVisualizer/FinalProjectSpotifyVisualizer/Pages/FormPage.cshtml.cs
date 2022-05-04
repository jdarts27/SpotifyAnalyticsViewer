using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using FinalProjectSpotifyVisualizer.Models;
using System.Collections.Generic;

namespace FinalProjectSpotifyVisualizer.Pages
{
    public class FormPageModel : PageModel
    {
        [BindProperty]
        public DataFormModel Checked { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            Dictionary<string, string> dictChecked = new Dictionary<string, string> { { "passedObject", JsonConvert.SerializeObject(Checked) } } ;

            return RedirectToPage("/ConfirmDataPull", dictChecked);
        }
    }
}
