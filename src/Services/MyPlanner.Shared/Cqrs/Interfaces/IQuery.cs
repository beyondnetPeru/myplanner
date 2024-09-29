namespace MyPlanner.Shared.Cqrs.Interfaces
{
    public interface IQuery<out T> : IRequest<T>
       where T : notnull
    {
    }
}
