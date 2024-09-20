using BeyondNet.Ddd;
using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.ValueObjects;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;
using static MyPlanner.Plannings.Domain.SizeModels.SizeModelTypeItem;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public class SizeModelTypeItemProps : IProps
    {
        public IdValueObject Id { get; private set; }
        public SizeModelTypeItemCode Code { get; private set; }
        public Name Name { get; private set; }
        public SizeModelType SizeModelType { get; private set; }

        public SizeModelTypeItemStatus Status { get; private set; }

        public SizeModelTypeItemProps(IdValueObject id, SizeModelTypeItemCode code, Name name, SizeModelType sizeModelType)
        {
            Id = id;
            Code = code;
            Name = name;
            SizeModelType = sizeModelType;
            Status = SizeModelTypeItemStatus.Active;
        }

        public SizeModelTypeItemProps(IdValueObject id, SizeModelTypeItemCode code, Name name, SizeModelType sizeModelType, SizeModelTypeItemStatus sizeModelTypeFactorStatus)
        {
            Id = id;
            Code = code;
            Name = name;
            SizeModelType = sizeModelType;
            Status = sizeModelTypeFactorStatus;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class SizeModelTypeItem : Entity<SizeModelTypeItem, SizeModelTypeItemProps>
    {
        private SizeModelTypeItem(SizeModelTypeItemProps props) : base(props)
        {
        }

        public static SizeModelTypeItem Create(IdValueObject id, SizeModelTypeItemCode code, Name name, SizeModelType sizeModelType)
        {
            return new SizeModelTypeItem(new SizeModelTypeItemProps(id, code, name, sizeModelType));
        }

        public static SizeModelTypeItem Load(string id, string code, string name, SizeModelType sizeModelType, int status)
        {
            return new SizeModelTypeItem(new SizeModelTypeItemProps(IdValueObject.Create(id),
                SizeModelTypeItemCode.Create(code),
                Name.Create(name), 
                sizeModelType,
                Enumeration.FromValue<SizeModelTypeItemStatus>(status)));
        }

        public void ChangeName(Name name)
        {
            GetProps().Name.SetValue(name.GetValue());
        }

        public void ChangeCode(SizeModelTypeItemCode code)
        {
            GetProps().Code.SetValue(code.GetValue());
        }

        public void Activate()
        {
            if (GetProps().Status == SizeModelTypeItemStatus.Active)
            {
                AddBrokenRule("Status", "Size Model Type Factor is already active.");
                return;
            }

            GetProps().Status.SetValue<SizeModelItemStatus>(SizeModelTypeItemStatus.Active.Id);
        }

        public void Deactivate()
        {
            if (GetProps().Status == SizeModelTypeItemStatus.Inactive)
            {
                AddBrokenRule("Status", "Size Model Type Factor is already inactive.");
                return;
            }

            GetProps().Status.SetValue<SizeModelTypeItemStatus>(SizeModelTypeItemStatus.Inactive.Id);
        }

        public void Delete()
        {
            if (GetProps().Status == SizeModelTypeItemStatus.Delete)
            {
                AddBrokenRule("Status", "Size Model Type Factor is already deleted.");
                return;
            }

            if (GetProps().Status == SizeModelTypeItemStatus.Active)
            {
                AddBrokenRule("Status", "Size Model Type Factor is active. Cannot delete.");
                return;
            }

            GetProps().Status.SetValue<SizeModelTypeItemStatus>(SizeModelTypeItemStatus.Delete.Id);
        }

        public class SizeModelTypeItemStatus : Enumeration
        {
            public static SizeModelTypeItemStatus Active = new SizeModelTypeItemStatus(1, "Active");
            public static SizeModelTypeItemStatus Inactive = new SizeModelTypeItemStatus(0, "Inactive");
            public static SizeModelTypeItemStatus Delete = new SizeModelTypeItemStatus(-1, "Delete");

            public SizeModelTypeItemStatus(int id, string name) : base(id, name)
            { }
        }
    }
}
