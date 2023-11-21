using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicApp.Data;
using MusicApp.Models;

namespace MusicApp.Pages.Albums
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public DeleteModel(ApplicationDbContext database)
        {
            this.database = database;
        }
        public Album Album { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Album = await database.Album.FindAsync(id);

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var album = await database.Album.FindAsync(id);


            database.Album.Remove(album);
            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
