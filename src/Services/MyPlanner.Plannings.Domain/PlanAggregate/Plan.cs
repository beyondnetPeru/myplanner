namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public class PlanProps : IProps
    {
        public IdValueObject Id { get; private set; }
        public ICollection<PlanCategory> Categories { get; private set; }
        public Name Name { get; private set; }
        public Owner Owner { get; private set; }
        public IdValueObject SizeModelTypeId { get; private set; }
        public ICollection<PlanItem> Items{ get; private set; }
        public Audit Audit { get; private set; }
        public PlanStatus Status { get; set; } = PlanStatus.Draft;

        public PlanProps(IdValueObject id, IdValueObject sizeModelTypeId, Name name, Owner owner, UserId userId)
        {
            Id = id;
            Categories = new List<PlanCategory>();
            SizeModelTypeId = sizeModelTypeId;            
            Name = name;
            Owner = owner;
            Items = new List<PlanItem>();
            Audit = Audit.Create(userId.GetValue());
            Status = PlanStatus.Draft;
        }

        public PlanProps(IdValueObject id, IdValueObject sizeModelTypeId, ICollection<PlanCategory> categories, Name name, Owner owner, ICollection<PlanItem> planItems, Audit audit, PlanStatus status)
        {
            Id = id;
            Categories = categories;
            SizeModelTypeId = sizeModelTypeId;
            Name = name;
            Owner = owner;
            Items = planItems;
            Audit = audit;
            Status = status;
        }


        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class Plan : Entity<Plan, PlanProps>
    {
        private Plan(PlanProps props) : base(props)
        {
        }

        public static Plan Create(IdValueObject id, IdValueObject sizeModelTypeId, Name name, Owner owner, UserId userId)
        {
            return new Plan(new PlanProps(id, sizeModelTypeId, name, owner, userId));
        }

        public static Plan Load(PlanProps props)
        {
            return new Plan(new PlanProps(props.Id, props.SizeModelTypeId, props.Categories, props.Name, props.Owner, props.Items, props.Audit, props.Status));
        }

        public string GetCategoryIdByName(string name)
        {
            var id = this.GetPropsCopy().Categories.FirstOrDefault(c => c.GetPropsCopy().Name.GetValue() == name)?.GetPropsCopy().Id.GetValue();

            if (string.IsNullOrEmpty(id))
            {
                AddBrokenRule("Category", "Category does not exist in the plan.");
                return string.Empty;
            }

            return id;
        }

        public void ChangeName(Name name, UserId userId)
        {
            Props.Name.SetValue(name.GetValue());
            Props.Audit.Update(userId.GetValue());
        }

        public void ChangeOwner(Owner owner, UserId userId)
        {
            Props.Owner.SetValue(owner.GetValue());
            Props.Audit.Update(userId.GetValue());
        }

        public void AddCategory(PlanCategory category, UserId userId)
        {
            if (Props.Categories.Any(x => x.GetPropsCopy().Name.GetValue().ToLowerInvariant() == category.GetPropsCopy().Name.GetValue().ToLowerInvariant()))
            {
                AddBrokenRule("Category", "Category already exists in the plan.");
                return;
            }

            Props.Categories.Add(category);
            Props.Audit.Update(userId.GetValue());
        }

        public void ChangeSizeModelTypeId(IdValueObject sizeModelTypeId, UserId userId)
        {
            Props.SizeModelTypeId.SetValue(sizeModelTypeId.GetValue());
            Props.Audit.Update(userId.GetValue());
        }

        public void RemoveCategory(PlanCategory category, UserId userId)
        {
            if (!Props.Categories.Any(x => x.GetPropsCopy().Name.GetValue().ToLowerInvariant() == category.GetPropsCopy().Name.GetValue().ToLowerInvariant()))
            {
                AddBrokenRule("Category", "Category does not exist in the plan.");
                return;
            }

            Props.Categories.Remove(category);
            Props.Audit.Update(userId.GetValue());
        }

        public void AddPlanItem(PlanItem planItem, UserId userId)
        {
            Props.Items.Add(planItem);
            Props.Audit.Update(userId.GetValue());
        }

        public void RemovePlanItem(PlanItem planItem, UserId userId)
        {        

            Props.Items.Remove(planItem);
            Props.Audit.Update(userId.GetValue());
        }

        public void Draft(UserId userId)
        {
            if (GetPropsCopy().Status == PlanStatus.Draft)
            {
                AddBrokenRule("Plan Status", "Plan is already in Draft status.");
                return;
            }

            if (GetPropsCopy().Status == PlanStatus.Closed)
            {
                AddBrokenRule("Plan Status", "Plan is closed and cannot be changed.");
                return;
            }

            Props.Items.ToList().ForEach(x => x.Draft(userId));
            Props.Status = PlanStatus.Draft;
            Props.Audit.Update(userId.GetValue());
        }

        public void Activate(UserId userId)
        {
            if (GetPropsCopy().Status == PlanStatus.Active)
            {
                AddBrokenRule("Plan Status", "Plan is already in Active status.");
                return;
            }

            if (GetPropsCopy().Status == PlanStatus.Closed)
            {
                AddBrokenRule("Plan Status", "Plan is closed and cannot be changed.");
                return;
            }

            Props.Items.ToList().ForEach(x => x.Activate(userId));
            Props.Status = PlanStatus.Active;
            Props.Audit.Update(userId.GetValue());
        }

        public void Deactivate(UserId userId)
        {
            if (GetPropsCopy().Status == PlanStatus.Inactive)
            {
                AddBrokenRule("Plan Status", "Plan is already in Inactive status.");
                return;
            }

            if (GetPropsCopy().Status == PlanStatus.Closed)
            {
                AddBrokenRule("Plan Status", "Plan is closed and cannot be changed.");
                return;
            }

            Props.Items.ToList().ForEach(x => x.Deactivate(userId));
            Props.Status = PlanStatus.Inactive;
            Props.Audit.Update(userId.GetValue());
        }

        public void Close(UserId userId)
        {
            if (GetPropsCopy().Status == PlanStatus.Closed)
            {
                AddBrokenRule("Plan Status", "Plan is already closed.");
                return;
            }

            Props.Items.ToList().ForEach(x => x.Close(userId));
            Props.Status = PlanStatus.Closed;
            Props.Audit.Update(userId.GetValue());
        }

        public void Delete(string PlanId, UserId userId)
        {
            if (GetPropsCopy().Status != PlanStatus.Inactive)
            {
                AddBrokenRule("Plan Status", "Plan is not in Inactive status.");
                return;
            }

            GetPropsCopy().Items.ToList().ForEach(x => x.Delete(userId));
            Props.Status = PlanStatus.Deleted;
            Props.Audit.Update(userId.GetValue());
        }
    }
}

public class PlanStatus : Enumeration
{
    public static PlanStatus Deleted = new PlanStatus(0, "Deleted");
    public static PlanStatus Draft = new PlanStatus(1, "Draft");
    public static PlanStatus Active = new PlanStatus(2, "Active");
    public static PlanStatus Inactive = new PlanStatus(3, "Inactive");
    public static PlanStatus Closed = new PlanStatus(4, "Closed");

    public PlanStatus(int id, string name) : base(id, name)
    {
    }
}