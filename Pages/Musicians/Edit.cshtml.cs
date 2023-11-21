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
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public EditModel(ApplicationDbContext database)
        {
            this.database = database;
        }

        [BindProperty]
        public string SelectedBand { get; set; }
        public Musician Musician { get; set; }

        public List<string> BandNames { get; set; }

        private async Task LoadMusician(int id)
        {
            Musician = await database.Musician.Include(m => m.Band).SingleAsync(m => m.ID == id);
        }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            await LoadMusician(id);
            BandNames = await database.Band.Select(b => b.Name).ToListAsync();


            return Page();
        }

        // The "contact" parameter is the Contact submitted through the HTML form, while the "Contact" variable is the Contact in the database.
        // In this method, we transfer the data we need from the "form contact" to the "database contact" and then save it.
        // Note that the variable names (Contact/contact) need to match because they are both connected to the "name" attributes in the HTML form, although uppercase/lowercase differences don't matter.
        public async Task<IActionResult> OnPostAsync(int id, Musician musician)
        {
            Band band = database.Band.Where(b => b.Name == SelectedBand).First();
            await LoadMusician(id);


            if (!ModelState.IsValid)
            {
                return Page();
            }


            // Transfer the properties from the insecure (user-provided) Contact object to the "real" Contact object, from the database.
            // Note that this is the step that ensures the user cannot set the FreeCalls variable.
            Musician.Name = musician.Name;
            Musician.BirthDay = musician.BirthDay;
            Musician.Country = musician.Country;
            Musician.Band = band;

            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
