namespace MyPlanner.Catalog.Api.Companies
{
    public class ENDPOINT
    {
        public const string Tag = "Companies";
        public class CREATE
        {
            public const string Name = "CreateCompany";
            public const string Summary = "Create Company";
            public const string Description = "Create a new company.";
        }

        public class GET
        {
            public const string Name = "GetCompany";
            public const string Summary = "Get Company";
            public const string Description = "Get a company by id.";
        }

        public class LIST
        {
            public const string Name = "ListCompanies";
            public const string Summary = "List Companies";
            public const string Description = "List all companies.";
        }

        public class UPDATE
        {
            public const string Name = "UpdateCompany";
            public const string Summary = "Update Company";
            public const string Description = "Update a company by id.";
        }

        public class DELETE
        {
            public const string Name = "DeleteCompany";
            public const string Summary = "Delete Company";
            public const string Description = "Delete a company by id.";
        }
    }
}
