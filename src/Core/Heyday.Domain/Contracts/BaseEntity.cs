using MassTransit;

namespace Heyday.Domain.Contracts
{
    public abstract class BaseEntity
    {
        public Guid id { get; private set; }

        protected BaseEntity()
        {
            id = NewId.NextGuid();
        }
    }
}
