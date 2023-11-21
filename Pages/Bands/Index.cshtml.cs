using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicApp.Data;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Pages.Bands
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public IndexModel(ApplicationDbContext database)
        {
            this.database = database;
        }
        public IList<Band> Bands { get; set; }
        public IList<Album> Albums { get; set; } 
        public double AverageBandScore { get; set; }
        [FromQuery]
        public string SearchTerm { get; set; }
        private const string NameColumn = "Name (A-Z)";
        private const string GenreColumn = "Genre";
        private const string ScoreColumn = "Rating";
        private readonly string[] SortColumns = { NameColumn, GenreColumn, ScoreColumn };
        [FromQuery]
        public string SortColumn { get; set; }
        public SelectList SortColumnList { get; set; }

        public IList<Review> ReviewList { get; set; }
        public double TotalRating { get; set; }
        public double BandScore { get; set; }


        public async Task OnGetAsync()
        {
            int albumCounter = 0;
            int numberOfReviews = 0;
            Bands = await database.Band.Include(b => b.Albums).ThenInclude(a => a.Reviews).ToListAsync();
            foreach (var band in Bands)
            {
                albumCounter = 0;
                numberOfReviews = 0;
                BandScore = 0;
                //if we create a new band that band wont have any albums so we skip calculating its score
                if (band.Albums.Count == 0)
                {
                }
                else
                {
                    
                    foreach (var album in band.Albums)
                    {
                        TotalRating = 0;
                        ReviewList = await database.Review.Where(r => r.Album == album).ToListAsync();
                        if (ReviewList.Count == 0)
                        {
                        }
                        else
                        {
                            numberOfReviews = ReviewList.Count;
                            foreach (var reviewItem in ReviewList)
                            {
                                TotalRating += reviewItem.Rating;
                            }
                            album.AverageRating = Math.Round(TotalRating / ReviewList.Count, 1);
                            BandScore += (double)album.AverageRating;
                            albumCounter++;
                        }
                        if (numberOfReviews == 0)
                        {
                        }
                        else
                        {
                            
                            band.AverageRating = Math.Round(BandScore / albumCounter, 1);
                            await database.SaveChangesAsync();
                        }
                    }
                    
                }
            }

            SortColumnList = new SelectList(SortColumns);

            var query = database.Band.Include(b => b.Albums).AsNoTracking();

            if (SearchTerm != null)
            {
                query = query.Where(b =>
                    b.Name.ToLower().Contains(SearchTerm.ToLower()) ||
                    b.Genre.ToLower().Contains(SearchTerm.ToLower())
                );
            }

            if (SortColumn != null)
            {
                if (SortColumn == NameColumn)
                {
                    query = query.OrderBy(b => b.Name);
                }
                else if (SortColumn == GenreColumn)
                {
                    query = query.OrderBy(b => b.Genre);
                }
                else if (SortColumn == ScoreColumn)
                {
                    query = query.OrderByDescending(b => b.AverageRating);
                }
            }

            Bands = await query.ToListAsync();

            //foreach (var album in Albums)
            //{
            //    TotalRating = 0;
            //    Album = album;
            //    ReviewList = await database.Review.Where(r => r.Album == album).ToListAsync();
            //    foreach (var reviewItem in ReviewList)
            //    {
            //        TotalRating += reviewItem.Rating;
            //    }
            //    Album.AverageRating = Math.Round(TotalRating / ReviewList.Count, 1);

            //}
            ////after the averageRating has been calculate we update the database again
            await database.SaveChangesAsync();
        }
    }
}
