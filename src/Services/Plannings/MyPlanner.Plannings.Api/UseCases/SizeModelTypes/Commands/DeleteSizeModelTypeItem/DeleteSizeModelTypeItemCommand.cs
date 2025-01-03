﻿


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeleteSizeModelTypeItem
{
    public class DeleteSizeModelTypeItemCommand : ICommand<ResultSet>
    {
        public string SizeModelTypeId { get; set; }
        public string SizeModelTypeItemId { get; set; }
        public string UserId { get; set; }

        public DeleteSizeModelTypeItemCommand(string sizeModelTypeId, string sizeModelTypeItemId, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            SizeModelTypeItemId = sizeModelTypeItemId;
            UserId = userId;
        }

    }
}
