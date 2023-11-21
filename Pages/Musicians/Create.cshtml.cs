using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MusicApp.Data;
using MusicApp.Models;

namespace MusicApp.Pages.Musicians
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
        public Musician Musician{ get; set; }
        public List<string> BandNames { get; set; }

        public async Task OnGetAsync()
        {
            BandNames = await database.Band.Select(b => b.Name).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(Musician musician)
        {
            Band band = database.Band.Where(b => b.Name == SelectedBand).First();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Musician.Name = musician.Name;
            Musician.BirthDay = musician.BirthDay;
            Musician.Band = band;

            await database.Musician.AddAsync(Musician);
            await database.SaveChangesAsync();
            return RedirectToPage("./Details", new { id = Musician.ID });
        }
    }
}
