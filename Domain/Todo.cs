using System;

namespace Domain
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public bool completed { get; set; }
    }
}