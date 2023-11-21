using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MusicApp.Data;
using MusicApp.Models;

namespace MusicApp.Pages.Albums
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext database;


        public CreateModel(ApplicationDbContext database)
        {
            this.database = database;

        }
        [BindProperty]
        public string SelectedBand { get; set; }
        [BindProperty]
        public Album Album { get; set; }
        public List<string> BandNames { get; set; }

        public async Task OnGetAsync()
        {
            BandNames = await database.Band.Select(b => b.Name).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(Album album)
        {
            BandNames = await database.Band.Select(b => b.Name).ToListAsync();
            Band band = database.Band.Where(b => b.Name == SelectedBand).First();


 
            Album.Name = album.Name;
            Album.ReleaseYear = album.ReleaseYear;
            Album.Band = band;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await database.Album.AddAsync(Album);
            await database.SaveChangesAsync();
            return RedirectToPage("./Details", new { id = Album.ID });
        }
    }
}
