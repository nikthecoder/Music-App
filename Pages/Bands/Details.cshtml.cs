using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MusicApp.Data;
using MusicApp.Models;

namespace MusicApp.Pages.Bands
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public DetailsModel(ApplicationDbContext database)
        {
            this.database = database;
        }

        public Band Band { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Band = await database.Band.FirstOrDefaultAsync(b => b.ID == id);

            if (Band == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
