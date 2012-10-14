using System;

namespace Coffee.News.Domain
{
    public class ShortNews
    {
        public Guid NewsId { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
