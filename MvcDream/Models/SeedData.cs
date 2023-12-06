using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcDream.Data;
using System;
using System.Linq;



namespace MvcDream.Models;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcDreamContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcDreamContext>>()))
        {
            // Look for any movies.
            if (context.DreamViewModel.Any())
            {
                return;   // DB has been seeded
            }
            context.DreamViewModel.AddRange(
                new DreamViewModel
                {
                    DreamName = "Nick Cave dream",
                    DreamText = "I had a dream Joe",
                    ReadableBy = "Private"
                },
                new DreamViewModel
                {
                    DreamName = "Dancing in the dark",
                    DreamText = "I dreamt I was dancing",
                    ReadableBy = "Everyone"
                },
                new DreamViewModel
                {
                    DreamName = "Doggie dog dog",
                    DreamText = "There was a dog and it was nice",
                    ReadableBy = "Everyone"
                },
                new DreamViewModel
                {
                    DreamName = "Do you love me",
                    DreamText = "I was watching a Nick Cave concert",
                    ReadableBy = "Friends"
                }
            );
            context.SaveChanges();
        }
    }
}

/*new DreamViewModel
                {
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M
                }*/
