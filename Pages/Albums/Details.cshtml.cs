using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MusicApp.Data;
using MusicApp.Models;

namespace MusicApp.Pages.Albums
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext database;
        public readonly AccessControl accessControl;

        public DetailsModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        [BindProperty]
        public int RatingScore { get; set; }
        [BindProperty]
        public string AlbumReview { get; set; }
        public Album Album { get; set; }
        public IList<Review> ReviewList { get; set; }
        public Review Review { get; set; }
        public double TotalRating { get; set; }
        public IList<IdentityUser> UserList { get; set; }



        public async Task<IActionResult> OnGetAsync(int id)
        {
             var user = accessControl.LoggedInUserID;


            Album = await database.Album.Include(a => a.Band).FirstOrDefaultAsync(a => a.ID == id);
            ReviewList = await database.Review.Where(r => r.Album == Album).ToListAsync();
            UserList = await database.Users.ToListAsync();
            foreach(var review in ReviewList)
            {
                foreach (var userItem in UserList)
                {
                    if (review.UserID == userItem.Id)
                    {
                        review.User = userItem;
                    }
                }
            }

            if (Album == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {

            Album = await database.Album.Include(a => a.Band).FirstOrDefaultAsync(a => a.ID == id);
            var user = accessControl.LoggedInUserID;
            Review review = new()
            {
                Content = AlbumReview,
                Rating = RatingScore,
                Album = Album,
                UserID = user
            };


            //we add the review here and update the database in order for the data to be displayed correctly
            await database.Review.AddAsync(review);
            await database.SaveChangesAsync();
            ReviewList = await database.Review.Include(r => r.User).Where(r => r.Album == Album).ToListAsync();
            

            if (!ModelState.IsValid)
            {
                return Page();
            }


            foreach (var reviewItem in ReviewList)
            {
                TotalRating += reviewItem.Rating;
            }
            review.Album.AverageRating = Math.Round(TotalRating / ReviewList.Count, 1);
            //after the averageRating has been calculate we update the database again
            await database.SaveChangesAsync();
            return RedirectToPage("./Details", new { id = Album.ID });

        }
    }
}
