namespace Persistence
{
    using System;

    public class Post : Entity
    {
        public Post()
            : base()
        {
        }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public User OwnerUser { get; set; }

        public Guid OwnerUserId { get; set; }

        public User WriterUser { get; set; }

        public Guid WriterUserId { get; set; }
    }
}