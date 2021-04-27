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
                    Title = "Past Todo 1",
                    completed = false
                },
                new Todo
                {
                    Title = "Past Todo 2",
                    completed = true
                },
                new Todo
                {
                    Title = "Future Todo 1",
                    completed = false
                }
            };

            await context.Todos.AddRangeAsync(Todos);
            await context.SaveChangesAsync();
        }
    }
}