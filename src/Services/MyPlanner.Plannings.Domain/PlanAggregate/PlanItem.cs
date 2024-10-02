namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class PlanItemProps : IProps
    {
        public IdValueObject Id { get; private set; }
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

        public PlanItemProps(IdValueObject id, IdValueObject productId, IdValueObject planCategoryId,
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

        public PlanItemProps(IdValueObject id, IdValueObject productId, IdValueObject planCategoryId,
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

        public static PlanItem Create(IdValueObject id, IdValueObject productId, IdValueObject planCategoryId,
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

        public static PlanItem Load(string id, string productId, string planCategoryId,
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
            GetProps().SizeModelTypeItemId.SetValue(sizeModelTypeItemId.GetValue());
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


        public void ChangeBallParkCosts(BallParkCost ballParkCost, UserId userId)
        {
            GetProps().BallParkCosts.SetValue(ballParkCost.GetValue());
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

        public void Delete(UserId userId)
        {
            if (GetPropsCopy().Status != PlanItemStatus.Inactive)
            {
                AddBrokenRule("Plan Item", "Plan Item is not inactive.");
                return;
            }

            GetProps().Status = PlanItemStatus.Deleted;
            GetProps().Audit.Update(userId.GetValue());
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


