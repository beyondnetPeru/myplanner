
namespace MyPlanner.Shared.CQRS
{
    public interface IQuery<out T> : IRequest<T>
       where T : notnull
    {
    }
}
