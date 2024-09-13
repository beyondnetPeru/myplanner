using BeyondNet.Ddd;
using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.ValueObjects;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class PlanItemProps : IProps
    {
        public IdValueObject Id { get; private set; }
        public BusinessFeature BusinessFeature { get; private set; }
        public TechnicalDefinition TechnicalDefinition { get; private set; } = TechnicalDefinition.DefaultValue();
        public ComponentsImpacted ComponentsImpacted { get; private set; } = ComponentsImpacted.DefaultValue();
        public TechnicalDependencies TechnicalDependencies { get; private set; } = TechnicalDependencies.DefaultValue();
        public SizeModelTypeFactor SizeModelTypeFactor { get; private set; }
        public SizeModelTypeValueSelected SizeModelTypeValueSelected { get; set; }
        public BallParkCost BallParkCost { get; private set; }
        public BallParkDependenciesCost BallParkDependenciesCost { get; private set; } = BallParkDependenciesCost.DefaultValue();
        public BallParkTotalCost BallParkTotalCost { get; private set; } = BallParkTotalCost.DefaultValue();
        public KeyAssumptions KeyAssumptions { get; private set; } = KeyAssumptions.DefaultValue();
        public Audit Audit { get; private set; }
        public PlanItemStatus Status { get; set; } = PlanItemStatus.Draft;

        public PlanItemProps(IdValueObject id, SizeModelTypeFactor sizeModelTypeFactor, SizeModelTypeValueSelected sizeModelTypeValueSelected, BusinessFeature businessFeature, TechnicalDefinition technicalDefinition, UserId userId)
        {
            Id = id;
            SizeModelTypeFactor = sizeModelTypeFactor;
            SizeModelTypeValueSelected = sizeModelTypeValueSelected;
            BusinessFeature = businessFeature;
            TechnicalDefinition = technicalDefinition;
            Audit = Audit.Create(userId.GetValue());
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class PlanItem : Entity<PlanItem, PlanItemProps>
    {
        private PlanItem(PlanItemProps props) : base(props)
        {
        }

        public static PlanItem Create(IdValueObject id, SizeModelTypeFactor sizeModelTypeFactor, SizeModelTypeValueSelected sizeModelTypeValueSelected, BusinessFeature businessFeature, TechnicalDefinition technicalDefinition, UserId userId)
        {
            return new PlanItem(new PlanItemProps(id, sizeModelTypeFactor, sizeModelTypeValueSelected, businessFeature, technicalDefinition, userId));
        }

        public void ChangeSizeModelTypeValueSelected(SizeModelTypeValueSelected sizeModelTypeValueSelected, UserId userId)
        {
            GetProps().SizeModelTypeValueSelected = sizeModelTypeValueSelected;
            GetProps().Audit.Update(userId.GetValue());
        }

        public void ChangeTechnicalDefinition(TechnicalDefinition technicalDefinition, UserId userId)
        {
            GetProps().TechnicalDefinition.SetValue(technicalDefinition.GetValue());
            GetProps().Audit.Update(userId.GetValue());

        }

        public void ChangeComponentsImpacted(ComponentsImpacted componentsImpacted, UserId userId)
        {
            GetProps().ComponentsImpacted.SetValue(componentsImpacted.GetValue());
            GetProps().Audit.Update(userId.GetValue());
        }

        public void ChangeTechnicalDependencies(TechnicalDependencies technicalDependencies, UserId userId)
        {
            GetProps().TechnicalDependencies.SetValue(technicalDependencies.GetValue());
            GetProps().Audit.Update(userId.GetValue());
        }


        public void ChangeBallParkCost(BallParkCost ballParkCost, UserId userId)
        {
            GetProps().BallParkCost.SetValue(ballParkCost.GetValue());
            GetProps().Audit.Update(userId.GetValue());
        }

        public void ChangeBallParkDependenciesCost(BallParkDependenciesCost ballParkDependenciesCost, UserId userId)
        {
            GetProps().BallParkDependenciesCost.SetValue(ballParkDependenciesCost.GetValue());
            GetProps().Audit.Update(userId.GetValue());
        }

        public void ChangeKeyAssumptions(KeyAssumptions keyAssumptions, UserId userId)
        {
            GetProps().KeyAssumptions.SetValue(keyAssumptions.GetValue());
            GetProps().Audit.Update(userId.GetValue());
        }

        public void Draft(UserId userId)
        {
            if (GetPropsCopy().Status == PlanItemStatus.Draft)
            {
                AddBrokenRule("Plan Item", "Plan Item is already in draft status.");
                return;
            }

            if (GetPropsCopy().Status == PlanItemStatus.Closed)
            {
                AddBrokenRule("Plan Item", "Plan Item is closed and cannot be changed.");
                return;
            }

            GetProps().Status = PlanItemStatus.Draft;
            GetProps().Audit.Update(userId.GetValue());
        }

        public void Activate(UserId userId)
        {
            if (GetPropsCopy().Status == PlanItemStatus.Active)
            {
                AddBrokenRule("Plan Item", "Plan Item is already in active status.");
                return;
            }

            if (GetPropsCopy().Status == PlanItemStatus.Closed)
            {
                AddBrokenRule("Plan Item", "Plan Item is closed and cannot be changed.");
                return;
            }

            GetProps().Status = PlanItemStatus.Active;
            GetProps().Audit.Update(userId.GetValue());
        }

        public void Deactivate(UserId userId)
        {
            if (GetPropsCopy().Status == PlanItemStatus.Inactive)
            {
                AddBrokenRule("Plan Item", "Plan Item is already in inactive status.");
                return;
            }

            if (GetPropsCopy().Status == PlanItemStatus.Closed)
            {
                AddBrokenRule("Plan Item", "Plan Item is closed and cannot be changed.");
                return;
            }

            GetProps().Status = PlanItemStatus.Inactive;
            GetProps().Audit.Update(userId.GetValue());
        }

        public void Close(UserId userId)
        {
            if (GetPropsCopy().Status == PlanItemStatus.Closed)
            {
                AddBrokenRule("Plan Item", "Plan Item is already closed.");
                return;
            }

            GetProps().Status = PlanItemStatus.Closed;
            GetProps().Audit.Update(userId.GetValue());
        }

    }

    public class PlanItemStatus : Enumeration
    {
        public static PlanItemStatus Draft = new PlanItemStatus(1, "Draft");
        public static PlanItemStatus Active = new PlanItemStatus(2, "Active");
        public static PlanItemStatus Inactive = new PlanItemStatus(3, "Inactive");
        public static PlanItemStatus Closed = new PlanItemStatus(4, "Closed");

        public PlanItemStatus(int id, string name) : base(id, name)
        {
        }
    }

}


