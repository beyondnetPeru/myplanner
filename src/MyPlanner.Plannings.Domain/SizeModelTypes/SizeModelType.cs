using BeyondNet.Ddd;
using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.ValueObjects;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;
using static MyPlanner.Plannings.Domain.SizeModelTypes.SizeModelTypeItem;

namespace MyPlanner.Plannings.Domain.SizeModelTypes
{
    public class SizeModelTypeProps : IProps
    {
        public IdValueObject Id { get; private set; }
        public SizeModelTypeCode Code { get; private set; }
        public Name Name { get; private set; }
        public ICollection<SizeModelTypeItem> Items { get; private set; } = new List<SizeModelTypeItem>();
        public SizeModelTypeStatus Status { get; private set; }

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
            GetProps().Code.SetValue(value.GetValue());
        }

        public void ChangeName(Name value)
        {
            GetProps().Name.SetValue(value.GetValue());
        }

        public void AddItem(SizeModelTypeItem item)
        {
            if (GetProps().Items.Any(x => x.GetPropsCopy().Code == item.GetPropsCopy().Code
            && x.GetPropsCopy().Status == SizeModelTypeItemStatus.Active))
            {
                AddBrokenRule("Item", "Item already exists in the list");
                return;
            }

            GetProps().Items.Add(item);
        }

        public void DeleteItem(SizeModelTypeItem item)
        {
            if (GetProps().Status == SizeModelTypeStatus.Active)
            {
                AddBrokenRule("Status", "Size Model Type is Active. Cannot remove Item");
                return;
            }

            if (!GetProps().Items.Any(x => x.GetPropsCopy().Code == item.GetPropsCopy().Code))
            {
                AddBrokenRule("Item", "Item does not exist in the list");
                return;
            }

            GetProps().Items.Remove(item);
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

            GetProps().Status.SetValue<SizeModelTypeStatus>(SizeModelTypeStatus.Inactive.Id);

        }

        public void Activate()
        {
            if (GetPropsCopy().Status == SizeModelTypeStatus.Active)
            {
                AddBrokenRule("Status", "Size Model Type is already active");
                return;
            }

            GetProps().Status.SetValue<SizeModelTypeStatus>(SizeModelTypeStatus.Active.Id);
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

            foreach (var item in GetProps().Items)
            {
                item.GetProps().Status.SetValue<SizeModelTypeItemStatus>(SizeModelTypeItemStatus.Delete.Id);
            }


            GetProps().Status.SetValue<SizeModelTypeStatus>(SizeModelTypeStatus.Delete.Id);
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
