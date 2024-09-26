namespace MyPlanner.Shared.Application.Errors
{
    public interface IErrorsRepository
    {
        Task Create(Error error);
    }
}
