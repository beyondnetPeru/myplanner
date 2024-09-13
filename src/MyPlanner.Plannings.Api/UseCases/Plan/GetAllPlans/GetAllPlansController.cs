using Carter;
using MediatR;
using MyPlanner.Plannings.Shared.Application.Dtos;

namespace MyPlanner.Plannings.Api.UseCases.Plan.GetAllPlans
{
    public class GetAllPlansController : ICarterModule
    {
        private readonly IMediator mediator;

        public GetAllPlansController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/plans", async (int page = 1, int recordsPerPage = 10) =>
            {
                var pagination = new PaginationDto()
                {
                    Page = page,
                    RecordsPerPage = recordsPerPage
                };

                var query = new GetAllPlansQuery(pagination);

                var result = await mediator.Send(query);

                return result != null ? Results.Ok(result) : Results.NotFound();

            }).WithTags(Tags.Plan);
        }
    }
}
