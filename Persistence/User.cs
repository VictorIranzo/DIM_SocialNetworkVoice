namespace Persistence
{
    using System.Collections.Generic;

    public class User : Entity
    {
        public User()
            : base()
        {
        }

        public string Name { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public byte[] Photo { get; set; }

        public List<Post> OwnerPosts { get; set; }

        public List<Post> WriterPosts { get; set; }
    }
}