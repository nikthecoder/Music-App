using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicApp.Data;
using MusicApp.Models;

namespace MusicApp.Pages.Bands
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public CreateModel(ApplicationDbContext database)
        {
            this.database = database;

        }

        public Band Band { get; set; }

        private void CreateEmptyBand()
        {
            Band = new Band
            {

            };
        }

        public void OnGet()
        {
            CreateEmptyBand();
        }

        public async Task<IActionResult> OnPostAsync(Band band)
        {
            CreateEmptyBand();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Band.Name = band.Name;
            Band.Genre = band.Genre;
            Band.YearFormed = band.YearFormed;

            await database.Band.AddAsync(Band);
            await database.SaveChangesAsync();
            return RedirectToPage("./Details", new { id = Band.ID });
        }
    }
}
