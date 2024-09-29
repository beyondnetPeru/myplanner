namespace MyPlanner.Shared.Cqrs.Interfaces
{
    public interface ICommand : ICommand<Unit>
    {
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
