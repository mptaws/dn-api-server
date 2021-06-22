using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Todos.Any()) return;

            var Todos = new List<Todo>
            {
                new Todo
                {
                    Title = "Past Todo",
                    completed = false,
                    Description = "Description of the Past Todo Task"
                },
                new Todo
                {
                    Title = "Present Todo",
                    completed = true,
                    Description = "Description of the Present Todo Task"
                },
                new Todo
                {
                    Title = "Future Todo",
                    completed = false,
                    Description = "Description of the Future Todo Task"
                }
            };

            await context.Todos.AddRangeAsync(Todos);
            await context.SaveChangesAsync();
        }
    }
}