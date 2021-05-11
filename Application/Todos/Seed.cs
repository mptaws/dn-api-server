using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;
using System.Collections.Generic;
using System.Linq;

namespace Application.Todos
{
    public class Seed
    {
        public class Command : IRequest
        {
            public Todo Todo { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
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

                await _context.Todos.AddRangeAsync(Todos);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}