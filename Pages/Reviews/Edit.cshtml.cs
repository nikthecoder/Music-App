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
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public EditModel(ApplicationDbContext database)
        {
            this.database = database;
        }

        [BindProperty]
        public int RatingScore { get; set; }
        public Review Review { get; set; }
        public double TotalRating { get; set; }
        public IList<Review> ReviewList { get; set; }



        public async Task<IActionResult> OnGetAsync(int id)
        {
            Review = await database.Review.SingleAsync(m => m.ID == id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, Review review)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Review = await database.Review.Include(r => r.Album).SingleAsync(r => r.ID == id);
            ReviewList = await database.Review.Where(r => r.Album == Review.Album).ToListAsync();

            if (RatingScore == 0)
            {
                Review.Content = review.Content;
            }
            else
            {
                Review.Content = review.Content;
                Review.Rating = RatingScore;
            }
            foreach (var reviewItem in ReviewList)
            {
                TotalRating += reviewItem.Rating;
            }
            Review.Album.AverageRating = Math.Round(TotalRating / ReviewList.Count, 1);

            await database.SaveChangesAsync();
            return RedirectToPage("/albums/Details", new { id = Review.Album.ID });
        }
    }
}