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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public IndexModel(ApplicationDbContext database)
        {
            this.database = database;
        }
        public IList<Musician> Musician { get; set; }
/*        public Band Band { get; set; }
        public IList<Album> Albums { get; set; }*/
        public async Task OnGetAsync()
        {
            Musician = await database.Musician.Include(m => m.Band).ToListAsync();
        }
    }
}
