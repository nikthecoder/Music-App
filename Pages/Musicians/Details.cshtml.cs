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
    public class DetailsModel : PageModel
    { 
        private readonly ApplicationDbContext database;

    public DetailsModel(ApplicationDbContext database)
    {
        this.database = database;
    }

    public Musician Musician { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Musician = await database.Musician.Include(m => m.Band).FirstOrDefaultAsync(a => a.ID == id);

        if (Musician == null)
        {
            return NotFound();
        }

        return Page();
    }
}
}
