namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class PlanCategoryProps : IProps
    {
        public IdValueObject Id { get; set; }
        public Name Name { get; private set; }

        public PlanCategoryProps(IdValueObject id, Name name)
        {
            Id = id;
            Name = name;
        }

        public object Clone()
        {
            return this.MemberwiseClone(); 
        }
    }
    public class PlanCategory : Entity<PlanCategory, PlanCategoryProps>
    {
        private PlanCategory(PlanCategoryProps props) : base(props)
        {
        }

        public static PlanCategory Create(IdValueObject id, Name name)
        {
            return new PlanCategory(new PlanCategoryProps(id, name));
        }

        public void ChangeName(Name name)
        {
            GetProps().Name.SetValue(name.GetValue());
        }
    }
}
