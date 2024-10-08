namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class PlanCategoryProps : IProps
    {
        public IdValueObject Id { get; set; }
        public IdValueObject PlanId { get; set; }
        public Name Name { get; private set; }

        public PlanCategoryProps(IdValueObject id, IdValueObject planId, Name name)
        {
            Id = id;
            PlanId = planId;
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

        public static PlanCategory Create(IdValueObject id, IdValueObject planId, Name name)
        {
            return new PlanCategory(new PlanCategoryProps(id, planId, name));
        }

        public void ChangeName(Name name)
        {
            GetProps().Name.SetValue(name.GetValue());
        }
    }   
}
