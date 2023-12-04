using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcDreams.Data;
using MvcDreams.Models;
using System;
using System.Linq;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcDreamsContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcDreamsContext>>()))
        {
            // Look for any movies.
            if (context.Dream.Any())
            {
                return;   // DB has been seeded
            }
            context.Dream.AddRange(
                new Dream
                {
                    Name = "Snow-strung rivercrossing with coined waters",
                    UploadDate = DateTime.Parse("2023-1-12"),
                    ReadableBy = "Everyone",
                    Tag = "Long Time",
                    DreamText = "Once upon a time, I dreamt of a rivercrossing. The water scintilliated with a golden light. Upon closer inspection, this iridescenent light came from coins in the water."
                },
                new Dream
                {
                    Name = "Mountain hike race with sulphur-smelling hotspring",
                    UploadDate = DateTime.Parse("2023-12-23"),
                    ReadableBy = "Everyone",
                    Tag = "Exploration",
					DreamText = "I hiked on a mountain. It was snowy. It stank of sulphur. A crater, or vent of sorts, seemed to contain hot water. It reeked of sulphur, but it was nice."

				},
                new Dream
                {
                    Name = "PTSD war stabbing torture how many wrongs is a right",
                    UploadDate = DateTime.Parse("2023-7-6"),
                    ReadableBy = "Friends",
                    Tag = "Violence",
					DreamText = "20 percent hearing in my left ear at best, 80 in the right. I, an active combatant, stabbed a barely-active combatant. The smell was like a mixture of a morgue's cremation building and sweat. Not nice."

				},
                new Dream
                {
                    Name = "Orc Kowloon",
                    UploadDate = DateTime.Parse("2023-8-10"),
                    ReadableBy = "Private",
                    Tag = "Exploration",
					DreamText = "A cramped city rife with all manner of hoodlums and ne'er-do-wells."

				}
            );
            context.SaveChanges();
        }
    }
}