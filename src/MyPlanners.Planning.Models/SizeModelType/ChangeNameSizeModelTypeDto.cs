﻿namespace MyPlanner.Plannings.Models.SizeModelType
{
    public class ChangeNameSizeModelTypeDto : AbstractUserDto
    {
        public ChangeNameSizeModelTypeDto(string name, string userId) : base(userId)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
