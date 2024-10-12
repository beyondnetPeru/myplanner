using static MyPlanner.Plannings.Domain.SizeModelTypes.SizeModelTypeItem;

namespace MyPlanner.Plannings.Domain.SizeModelTypes
{
    public class SizeModelTypeProps : IProps
    {
        public IdValueObject Id { get; private set; }
        public SizeModelTypeCode Code { get; private set; }
        public Name Name { get; private set; }
        public ICollection<SizeModelTypeItem> Items { get; private set; } = new List<SizeModelTypeItem>();
        public SizeModelTypeStatus Status { get; set; }

        public SizeModelTypeProps(IdValueObject id, SizeModelTypeCode code, Name name)
        {
            Id = id;
            Code = code;
            Name = name;
            Status = SizeModelTypeStatus.Active;
        }

        public SizeModelTypeProps(IdValueObject id, SizeModelTypeCode code, Name name, SizeModelTypeStatus status)
        {
            Id = id;
            Code = code;
            Name = name;
            Status = status;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class SizeModelType : Entity<SizeModelType, SizeModelTypeProps>
    {
        private SizeModelType(SizeModelTypeProps props) : base(props)
        {
        }

        public static SizeModelType Create(IdValueObject id, SizeModelTypeCode code, Name name)
        {
            return new SizeModelType(new SizeModelTypeProps(id, code, name));
        }

        public static SizeModelType Create(SizeModelTypeProps props)
        {
            return new SizeModelType(props);
        }

        public static SizeModelType Load(string id, string code, string name, int status)
        {
            var props = new SizeModelTypeProps(IdValueObject.Create(id),
                                               SizeModelTypeCode.Create(code),
                                               Name.Create(name),
                                               Enumeration.FromValue<SizeModelTypeStatus>(status));

            return Create(props);

        }

        public void ChangeCode(SizeModelTypeCode value)
        {
            Props.Code.SetValue(value.GetValue());
        }

        public void ChangeName(Name value)
        {
            Props.Name.SetValue(value.GetValue());
        }

        public void AddItem(SizeModelTypeItem item)
        {
            if (Props.Items.Any(x => x.GetPropsCopy().Code == item.GetPropsCopy().Code
            && x.GetPropsCopy().Status == SizeModelTypeItemStatus.Active))
            {
                AddBrokenRule("Item", "Item already exists in the list");
                return;
            }

            Props.Items.Add(item);
        }

        public void DeleteItem(SizeModelTypeItem item)
        {
            if (Props.Status == SizeModelTypeStatus.Active)
            {
                AddBrokenRule("Status", "Size Model Type is Active. Cannot remove Item");
                return;
            }

            if (!Props.Items.Any(x => x.GetPropsCopy().Code == item.GetPropsCopy().Code))
            {
                AddBrokenRule("Item", "Item does not exist in the list");
                return;
            }

            foreach (var item1 in Props.Items)
            {
                item1.Props.Status = SizeModelTypeItemStatus.Delete;
            };
        }

        public void Deactivate()
        {
            if (GetPropsCopy().Status == SizeModelTypeStatus.Inactive)
            {
                AddBrokenRule("Status", "Size Model Type is already inactive");
                return;
            }

            if (GetPropsCopy().Items.Any(x => x.GetPropsCopy().Status == SizeModelTypeItemStatus.Active))
            {
                AddBrokenRule("Status", "Size Model Type has active Items");
                return;
            }

            Props.Status = SizeModelTypeStatus.Inactive;

        }

        public void Activate()
        {
            if (GetPropsCopy().Status == SizeModelTypeStatus.Active)
            {
                AddBrokenRule("Status", "Size Model Type is already active");
                return;
            }

            Props.Status = SizeModelTypeStatus.Active;
        }

        public void Delete()
        {
            if (GetPropsCopy().Status == SizeModelTypeStatus.Active)
            {
                AddBrokenRule("Status", "Size Model Type is already Active. Cannot be deleted");
                return;
            }

            if (GetPropsCopy().Items.Any(x => x.GetPropsCopy().Status == SizeModelTypeItemStatus.Active))
            {
                AddBrokenRule("Status", "Size Model Type has active Items. Cannot be deleted");
                return;
            }

            foreach (var item in Props.Items)
            {
                item.Props.Status= SizeModelTypeItemStatus.Delete;
            }


            Props.Status = SizeModelTypeStatus.Delete;
        }

    }

    public class SizeModelTypeStatus : Enumeration
    {
        public static SizeModelTypeStatus Delete = new SizeModelTypeStatus(-1, nameof(Delete).ToLowerInvariant());
        public static SizeModelTypeStatus Active = new SizeModelTypeStatus(1, nameof(Active).ToLowerInvariant());
        public static SizeModelTypeStatus Inactive = new SizeModelTypeStatus(0, nameof(Inactive).ToLowerInvariant());


        public SizeModelTypeStatus(int id, string name) : base(id, name)
        {
        }
    }
}
