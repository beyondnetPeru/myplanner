﻿namespace MyPlanner.Shared.Domain.ValueObjects
{
    public class Owner : StringValueObject
    {
        protected Owner(string value) : base(value)
        {
        }

        public static Owner Create(string value)
        {
            return new Owner(value);
        }

        public static new Owner DefaultValue => new Owner(string.Empty);
    }
}
