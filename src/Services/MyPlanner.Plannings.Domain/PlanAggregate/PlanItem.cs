namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class PlanItemProps : IProps
    {
        public IdValueObject Id { get; private set; }
        public IdValueObject PlanId { get; private set; }
        public IdValueObject ProductId { get; private set; }
        public IdValueObject PlanCategoryId { get; private set; }
        public BusinessFeature BusinessFeature { get; private set; }
        public TechnicalDefinition TechnicalDefinition { get; private set; } = TechnicalDefinition.DefaultValue();
        public ComponentsImpacted ComponentsImpacted { get; private set; } = ComponentsImpacted.DefaultValue();
        public TechnicalDependencies TechnicalDependencies { get; private set; } = TechnicalDependencies.DefaultValue();
        public IdValueObject SizeModelTypeItemId { get; private set; }
        public BallParkCost BallParkCosts { get; private set; }
        public KeyAssumptions KeyAssumptions { get; private set; } = KeyAssumptions.DefaultValue();
        public Audit Audit { get; private set; }
        public PlanItemStatus Status { get; set; } = PlanItemStatus.Draft;
        public UserId UserId { get; private set; }

        public PlanItemProps(IdValueObject id, 
                             IdValueObject planId,   
                             IdValueObject productId, 
                             IdValueObject planCategoryId,
                             BusinessFeature businessFeature,
                             TechnicalDefinition technicalDefinition,
                             ComponentsImpacted componentsImpacted,
                             TechnicalDependencies technicalDependencies,
                             IdValueObject sizeModelTypeItemId,
                             BallParkCost ballParkCost,
                             KeyAssumptions keyAssumptions,
                             UserId userId)
        {
            Id = id;
            PlanId = planId;
            ProductId = productId;
            PlanCategoryId = planCategoryId;
            BusinessFeature = businessFeature;
            TechnicalDefinition = technicalDefinition;
            ComponentsImpacted = componentsImpacted;
            TechnicalDependencies = technicalDependencies;
            SizeModelTypeItemId = sizeModelTypeItemId;
            BallParkCosts = ballParkCost;
            KeyAssumptions = keyAssumptions;
            UserId = userId;
            Audit = Audit.Create(userId.GetValue());
            Status = PlanItemStatus.Draft;            
        }

        public PlanItemProps(IdValueObject id,
                             IdValueObject planId,   
                             IdValueObject productId, 
                             IdValueObject planCategoryId,
                             BusinessFeature businessFeature,
                             TechnicalDefinition technicalDefinition,
                             ComponentsImpacted componentsImpacted,
                             TechnicalDependencies technicalDependencies,
                             IdValueObject sizeModelTypeItemId,
                             BallParkCost ballParkCost,
                             KeyAssumptions keyAssumptions,                             
                             Audit audit,
                             PlanItemStatus status,
                             UserId userId)
        {
            Id = id;
            PlanId = planId;
            ProductId = productId;
            PlanCategoryId = planCategoryId;
            BusinessFeature = businessFeature;
            TechnicalDefinition = technicalDefinition;
            ComponentsImpacted = componentsImpacted;
            TechnicalDependencies = technicalDependencies;
            SizeModelTypeItemId = sizeModelTypeItemId;
            BallParkCosts = ballParkCost;
            KeyAssumptions = keyAssumptions;
            Audit = audit;
            Status = status;
            UserId = userId;
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

        public static PlanItem Create(IdValueObject id, IdValueObject planId, IdValueObject productId, IdValueObject planCategoryId,
                             BusinessFeature businessFeature,
                             TechnicalDefinition technicalDefinition,
                             ComponentsImpacted componentsImpacted,
                             TechnicalDependencies technicalDependencies,
                             IdValueObject sizeModelTypeItemId,
                             BallParkCost ballParkCost,
                             KeyAssumptions keyAssumptions,
                             UserId userId)
        {
            return new PlanItem(new PlanItemProps(id, 
                planId,
                productId,
                planCategoryId,
                businessFeature,
                technicalDefinition,
                componentsImpacted,
                technicalDependencies,
                sizeModelTypeItemId,
                ballParkCost,
                keyAssumptions,
                userId));
        }

        public static PlanItem Load(string id, string planId, string productId, string planCategoryId,
                             BusinessFeature businessFeature,
                             TechnicalDefinition technicalDefinition,
                             ComponentsImpacted componentsImpacted,
                             TechnicalDependencies technicalDependencies,
                             string sizeModelTypeItemId,
                             BallParkCost ballParkCost,
                             KeyAssumptions keyAssumptions,
                             Audit audit,
                             int status,
                             string userId)
        {
            return new PlanItem(new PlanItemProps(IdValueObject.Create(id),
                IdValueObject.Create(planId),
                IdValueObject.Create(productId),
                IdValueObject.Create(planCategoryId),
                businessFeature,
                technicalDefinition,
                componentsImpacted,
                technicalDependencies,
                IdValueObject.Create(sizeModelTypeItemId),
                ballParkCost,
                keyAssumptions,
                audit,
                Enumeration.FromValue<PlanItemStatus>(status),
                UserId.Create(userId)));
        }

        public void ChangeSizeModelTypeItemId(IdValueObject sizeModelTypeItemId, UserId userId)
        {
            Props.SizeModelTypeItemId.SetValue(sizeModelTypeItemId.GetValue());
            Props.Audit.Update(userId.GetValue());
        }

        public void ChangeTechnicalDefinition(TechnicalDefinition technicalDefinition, UserId userId)
        {
            Props.TechnicalDefinition.SetValue(technicalDefinition.GetValue());
            Props.Audit.Update(userId.GetValue());

        }

        public void ChangeComponentsImpacted(ComponentsImpacted componentsImpacted, UserId userId)
        {
            Props.ComponentsImpacted.SetValue(componentsImpacted.GetValue());
            Props.Audit.Update(userId.GetValue());
        }

        public void ChangeTechnicalDependencies(TechnicalDependencies technicalDependencies, UserId userId)
        {
            Props.TechnicalDependencies.SetValue(technicalDependencies.GetValue());
            Props.Audit.Update(userId.GetValue());
        }


        public void ChangeBallParkCosts(BallParkCost ballParkCost, UserId userId)
        {
            Props.BallParkCosts.SetValue(ballParkCost.GetValue());
            Props.Audit.Update(userId.GetValue());
        }

        public void ChangeKeyAssumptions(KeyAssumptions keyAssumptions, UserId userId)
        {
            Props.KeyAssumptions.SetValue(keyAssumptions.GetValue());
            Props.Audit.Update(userId.GetValue());
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

            Props.Status = PlanItemStatus.Draft;
            Props.Audit.Update(userId.GetValue());
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

            Props.Status = PlanItemStatus.Active;
            Props.Audit.Update(userId.GetValue());
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

            Props.Status = PlanItemStatus.Inactive;
            Props.Audit.Update(userId.GetValue());
        }

        public void Close(UserId userId)
        {
            if (GetPropsCopy().Status == PlanItemStatus.Closed)
            {
                AddBrokenRule("Plan Item", "Plan Item is already closed.");
                return;
            }

            Props.Status = PlanItemStatus.Closed;
            Props.Audit.Update(userId.GetValue());
        }

        public void Delete(UserId userId)
        {
            if (GetPropsCopy().Status != PlanItemStatus.Inactive)
            {
                AddBrokenRule("Plan Item", "Plan Item is not inactive.");
                return;
            }

            Props.Status = PlanItemStatus.Deleted;
            Props.Audit.Update(userId.GetValue());
        }
    }

    public class PlanItemStatus : Enumeration
    {
        public static PlanItemStatus Deleted = new PlanItemStatus(0, "Deleted");
        public static PlanItemStatus Draft = new PlanItemStatus(1, "Draft");
        public static PlanItemStatus Active = new PlanItemStatus(2, "Active");
        public static PlanItemStatus Inactive = new PlanItemStatus(3, "Inactive");
        public static PlanItemStatus Closed = new PlanItemStatus(4, "Closed");

        public PlanItemStatus(int id, string name) : base(id, name)
        {
        }
    }

}


