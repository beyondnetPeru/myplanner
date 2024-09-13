namespace MyPlanner.Plannings.Shared.Application.Errors
{
    public interface IErrorsRepository
    {
        Task Create(Error error);
    }
}
