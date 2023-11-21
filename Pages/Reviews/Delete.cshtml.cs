using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MusicApp.Data;
using MusicApp.Models;

namespace MusicApp.Pages.Reviews
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext database;
        public readonly AccessControl accessControl;

        public DeleteModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }
        public Review Review{ get; set; }
        public Album Album { get; set; }
        public double TotalRating { get; set; }
        public IList<Review> ReviewList { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            Review = await database.Review.Include(r => r.Album).SingleOrDefaultAsync(r => r.ID == id);
            if (!accessControl.UserCanAccess(Review))
            {
                return Forbid();
            }
            if (Review == null)
            {
                return NotFound();
            }
            
            Album = Review.Album;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            Review = await database.Review.Include(r => r.Album).SingleOrDefaultAsync(r => r.ID == id);
            Album = Review.Album;

            if (!accessControl.UserCanAccess(Review))
            {
                return Forbid();
            }

            database.Review.Remove(Review);
            await database.SaveChangesAsync();

            ReviewList = await database.Review.Where(r => r.Album == Review.Album).ToListAsync();
            if(ReviewList.Count != 0)
            {
                foreach (var reviewItem in ReviewList)
                {
                    TotalRating += reviewItem.Rating;
                }
                Review.Album.AverageRating = Math.Round(TotalRating / ReviewList.Count, 1);
            }
            else
            {
                Review.Album.AverageRating = 0;
            }


            await database.SaveChangesAsync();
            return RedirectToPage("/albums/Details", new { id = Album.ID });
        }
    }
}
