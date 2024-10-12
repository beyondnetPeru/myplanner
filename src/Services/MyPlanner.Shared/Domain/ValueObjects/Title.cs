namespace MyPlanner.Projects.Domain.ValueObjects
{
    public class Title : StringValueObject
    {
        public Title(string value) : base(value)
        {

        }

        public static Title Create(string value)
        {
            return new Title(value);
        }

    }
}
