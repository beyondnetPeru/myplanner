﻿


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeById
{
    public class GetSizeModelTypeByIdQuery : IQuery<ResultSet>
    {
        public GetSizeModelTypeByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
