namespace Persistence
{
    using System;

    public class Entity
    {
        public Entity()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}