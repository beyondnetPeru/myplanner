namespace MyPlanner.Catalog.Api.Companies
{
    public class CompanyServices
    {
        public CompanyServices(IMediator mediator)
        {
            Mediator = mediator;
        }

        public IMediator Mediator { get; }
    }
}
