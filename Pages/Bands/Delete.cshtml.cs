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
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public DeleteModel(ApplicationDbContext database)
        {
            this.database = database;
        }
        public Band Band { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Band = await database.Band.FindAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var band = await database.Band.FindAsync(id);


            database.Band.Remove(band);
            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
