namespace GR.Entities
{

    public interface IAggregateRoot: IAggregateRoot<long>, IEntity
    {

    }

    public interface IAggregateRoot<TKey> : IEntity<TKey>
    {
    }
}
