using MyPlanner.Plannings.Infrastructure.Database;
using MyPlanner.Shared.Domain;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Infrastructure.Idempotency;

public class RequestManager : IRequestManager
{
    private readonly PlanningDbContext _context;

    public RequestManager(PlanningDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    public async Task<bool> ExistAsync(Guid id)
    {
        var request = await _context.
            FindAsync<ClientRequest>(id);

        return request != null;
    }

    public async Task CreateRequestForCommandAsync<T>(Guid id)
    {
        var exists = await ExistAsync(id);

        var request = exists ?
            throw new DomainException($"Request with {id} already exists") :
            new ClientRequest()
            {
                Id = id,
                Name = typeof(T).Name,
                Time = DateTime.UtcNow
            };

        _context.Add(request);

        await _context.SaveChangesAsync();
    }
}
