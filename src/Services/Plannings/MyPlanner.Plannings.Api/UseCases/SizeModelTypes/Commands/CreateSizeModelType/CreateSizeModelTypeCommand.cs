﻿



namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.CreateSizeModelType
{
    public class CreateSizeModelTypeCommand : ICommand<ResultSet>
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<CreateSizeModelTypeItemsRequest> Items { get; private set; }
        public string UserId { get; private set; }

        public CreateSizeModelTypeCommand(string code,
                                          string name,
                                          string description,
                                          ICollection<CreateSizeModelTypeItemsRequest> items,
                                          string userId)
        {
            Code = code;
            Name = name;
            Items = items;
            Description = description;
            UserId = userId;
        }
    }

    public class CreateSizeModelTypeItemsRequest
    {
        public CreateSizeModelTypeItemsRequest(string code, string name, string userId)
        {
            Code = code;
            Name = name;
            UserId = userId;
        }

        public string Code { get; }
        public string Name { get; }
        public string UserId { get; }
    }
}
