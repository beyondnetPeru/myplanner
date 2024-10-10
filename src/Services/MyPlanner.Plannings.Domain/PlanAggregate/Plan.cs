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
            GetProps().Name.SetValue(name.GetValue());
            GetProps().Audit.Update(userId.GetValue());
        }

        public void ChangeOwner(Owner owner, UserId userId)
        {
            GetProps().Owner.SetValue(owner.GetValue());
            GetProps().Audit.Update(userId.GetValue());
        }

        public void AddCategory(PlanCategory category, UserId userId)
        {
            if (GetProps().Categories.Contains(category))
            {
                AddBrokenRule("Category", "Category already exists in the plan.");
                return;
            }

            GetProps().Categories.Add(category);
            GetProps().Audit.Update(userId.GetValue());
        }

        public void ChangeSizeModelTypeId(IdValueObject sizeModelTypeId, UserId userId)
        {
            GetProps().SizeModelTypeId.SetValue(sizeModelTypeId.GetValue());
            GetProps().Audit.Update(userId.GetValue());
        }

        public void RemoveCategory(PlanCategory category, UserId userId)
        {
            if (!GetPropsCopy().Categories.Contains(category))
            {
                AddBrokenRule("Category", "Category does not exist in the plan.");
                return;
            }

            GetProps().Categories.Remove(category);
            GetProps().Audit.Update(userId.GetValue());
        }

        public void AddPlanItem(PlanItem planItem, UserId userId)
        {
            if (GetPropsCopy().Items.Where(x => x.GetPropsCopy().Id.GetValue() == planItem.GetPropsCopy().Id.GetValue()).Any())
            {
                AddBrokenRule("Plan Item", "Plan Item exists in the plan.");
                return;
            }

            GetProps().Items.Add(planItem);
            GetProps().Audit.Update(userId.GetValue());
        }

        public void RemovePlanItem(PlanItem planItem, UserId userId)
        {
            if (!GetPropsCopy().Items.Where(x => x.GetPropsCopy().Id.GetValue() == planItem.GetPropsCopy().Id.GetValue()).Any())
            {
                AddBrokenRule("Plan Item", "Plan Item does not exist in the plan.");
                return;
            }

            GetProps().Items.Remove(planItem);
            GetProps().Audit.Update(userId.GetValue());
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

            GetProps().Items.ToList().ForEach(x => x.Draft(userId));
            GetProps().Status = PlanStatus.Draft;
            GetProps().Audit.Update(userId.GetValue());
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

            GetProps().Items.ToList().ForEach(x => x.Activate(userId));
            GetProps().Status = PlanStatus.Active;
            GetProps().Audit.Update(userId.GetValue());
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

            GetProps().Items.ToList().ForEach(x => x.Deactivate(userId));
            GetProps().Status = PlanStatus.Inactive;
            GetProps().Audit.Update(userId.GetValue());
        }

        public void Close(UserId userId)
        {
            if (GetPropsCopy().Status == PlanStatus.Closed)
            {
                AddBrokenRule("Plan Status", "Plan is already closed.");
                return;
            }

            GetProps().Items.ToList().ForEach(x => x.Close(userId));
            GetProps().Status = PlanStatus.Closed;
            GetProps().Audit.Update(userId.GetValue());
        }

        public void Delete(string PlanId, UserId userId)
        {
            if (GetPropsCopy().Status != PlanStatus.Inactive)
            {
                AddBrokenRule("Plan Status", "Plan is not in Inactive status.");
                return;
            }

            GetPropsCopy().Items.ToList().ForEach(x => x.Delete(userId));
            GetProps().Status = PlanStatus.Deleted;
            GetProps().Audit.Update(userId.GetValue());
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