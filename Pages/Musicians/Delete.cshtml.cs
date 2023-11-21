using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicApp.Data;
using MusicApp.Models;

namespace MusicApp.Pages.Musicians
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public DeleteModel(ApplicationDbContext database)
        {
            this.database = database;
        }
        public Musician Musician { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Musician = await database.Musician.FindAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var musician = await database.Musician.FindAsync(id);


            database.Musician.Remove(musician);
            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
