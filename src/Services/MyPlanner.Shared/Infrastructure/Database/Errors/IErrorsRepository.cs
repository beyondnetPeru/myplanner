namespace MyPlanner.Shared.Infrastructure.Database.Errors
{
    public interface IErrorsRepository
    {
        Task Create(Error error);
    }
}
