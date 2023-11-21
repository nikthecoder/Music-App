using Microsoft.AspNetCore.Identity;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext database, UserManager<IdentityUser> userManager)
        {
            if (database.Band.Any() || database.Album.Any() || database.Musician.Any() || database.Review.Any() || database.Users.Any())
            {
                return;
            }

            string bandsPath = @"Data/Bands.csv";
            string albumsPath = @"Data/Albums.csv";
            string musiciansPath = @"Data/Musicians.csv";
            string reviewsPath = @"Data/Reviews.csv";

            // If Bands.csv does not exist or has issues being read
            if (!File.Exists(bandsPath))
            {
                return;
            }

            string[] bandLines = File.ReadAllLines(bandsPath);
            foreach (string line in bandLines)
            {
                try
                {
                    // First, split the line on commas (CSV means "comma-separated values").
                    string[] parts = line.Split(',');

                    // Then create a product with its values set to the different parts of the line.
                    Band b = new Band
                    {
                        Name = parts[0],
                        YearFormed = Convert.ToInt32(parts[1]),
                        Genre = parts[2]
                    };
                    database.Band.Add(b);
                }
                catch
                {
                    return;
                }
            }

            database.SaveChanges();

            // If Albums.csv does not exist or has issues being read
            if (!File.Exists(albumsPath))
            {
                return;
            }

            string[] albumLines = File.ReadAllLines(albumsPath);
            foreach (string line in albumLines)
            {
                try
                {
                    // First, split the line on commas (CSV means "comma-separated values").
                    string[] parts = line.Split(',');

                    // Then create a product with its values set to the different parts of the line.
                    Album a = new Album
                    {
                        Name = parts[0],
                        ReleaseYear = Convert.ToInt32(parts[1]),
                        Band = (Band)database.Band.Where(b => b.Name == parts[2]).First()
                    };
                    database.Album.Add(a);
                }
                catch
                {
                    return;
                }
            }

            database.SaveChanges();

            // If Musicians.csv does not exist or has issues being read
            if (!File.Exists(musiciansPath))
            {
                return;
            }

            string[] musicianLines = File.ReadAllLines(musiciansPath);
            foreach (string line in musicianLines)
            {
                try
                {
                    // First, split the line on commas (CSV means "comma-separated values").
                    string[] parts = line.Split(',');

                    // Then create a product with its values set to the different parts of the line.
                    Musician m = new Musician
                    {
                        Name = parts[0],
                        BirthDay = Convert.ToDateTime(parts[1]),
                        Country = parts[2],
                        Band = (Band)database.Band.Where(b => b.Name == parts[3]).First()
                    };
                    database.Musician.Add(m);
                }
                catch
                {
                    return;
                }
            }

            database.SaveChanges();

            if (!database.Users.Any())
            {
                for (int i = 1; i <= 3; i++)
                {
                    var exampleEmail = "user" + i + "@example.com";
                    var exampleUser = new IdentityUser(exampleEmail);
                    exampleUser.Email = exampleEmail;
                    exampleUser.EmailConfirmed = true;
                    await userManager.CreateAsync(exampleUser, "Password" + i + "!");
                }
            }

            database.SaveChanges();

            // If Reviews.csv does not exist or has issues being read
            if (!File.Exists(reviewsPath))
            {
                return;
            }

            string[] reviewLines = File.ReadAllLines(reviewsPath);
            foreach (string line in reviewLines)
            {
                try
                {
                    // First, split the line on commas (CSV means "comma-separated values").
                    string[] parts = line.Split(',');

                    // Then create a product with its values set to the different parts of the line.
                    Review r = new Review
                    {
                        Content = parts[0],
                        Rating = int.Parse(parts[1]),
                        Album = (Album)database.Album.Where(a => a.Name == parts[2]).First(),
                        User = database.Users.Where(u => u.Email == parts[3]).First()
                    };
                    database.Review.Add(r);
                }
                catch
                {
                    return;
                }
            }

            database.SaveChanges();
        }
    }
}
