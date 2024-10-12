using static MyPlanner.Plannings.Domain.SizeModelTypes.SizeModelTypeItem;

namespace MyPlanner.Plannings.Domain.SizeModelTypes
{
    public class SizeModelTypeItemProps : IProps
    {
        public IdValueObject Id { get; private set; }
        public IdValueObject SizeModelTypeId { get; private set; }
        public SizeModelTypeItemCode Code { get; private set; }
        public Name Name { get; private set; }
        public SizeModelTypeItemStatus Status { get; set; }

        public SizeModelTypeItemProps(IdValueObject id, IdValueObject sizeModelTypeId, SizeModelTypeItemCode code, Name name, SizeModelType sizeModelType)
        {
            Id = id;
            SizeModelTypeId = sizeModelTypeId;
            Code = code;
            Name = name;            
            Status = SizeModelTypeItemStatus.Active;
        }

        public SizeModelTypeItemProps(IdValueObject id, IdValueObject sizeModelTypeId, SizeModelTypeItemCode code, Name name, SizeModelType sizeModelType, SizeModelTypeItemStatus sizeModelTypeFactorStatus)
        {
            Id = id;
            SizeModelTypeId = sizeModelTypeId;
            Code = code;
            Name = name;
            Status = sizeModelTypeFactorStatus;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class SizeModelTypeItem : Entity<SizeModelTypeItem, SizeModelTypeItemProps>
    {
        private SizeModelTypeItem(SizeModelTypeItemProps props) : base(props)
        {
        }

        public static SizeModelTypeItem Create(IdValueObject id, IdValueObject sizeModelTypeId,  SizeModelTypeItemCode code, Name name, SizeModelType sizeModelType)
        {
            return new SizeModelTypeItem(new SizeModelTypeItemProps(id, sizeModelTypeId, code, name, sizeModelType));
        }

        public static SizeModelTypeItem Load(string id, string sizeModelTypeId, string code, string name, SizeModelType sizeModelType, int status)
        {
            return new SizeModelTypeItem(new SizeModelTypeItemProps(IdValueObject.Create(id),
                IdValueObject.Create(sizeModelTypeId),
                SizeModelTypeItemCode.Create(code),
                Name.Create(name),
                sizeModelType,
                Enumeration.FromValue<SizeModelTypeItemStatus>(status)));
        }

        public void ChangeName(Name name)
        {
            Props.Name.SetValue(name.GetValue());
        }

        public void ChangeCode(SizeModelTypeItemCode code)
        {
            Props.Code.SetValue(code.GetValue());
        }

        public void Activate()
        {
            if (Props.Status == SizeModelTypeItemStatus.Active)
            {
                AddBrokenRule("Status", "Size Model Type Factor is already active.");
                return;
            }

            Props.Status = SizeModelTypeItemStatus.Active;
        }

        public void Deactivate()
        {
            if (Props.Status == SizeModelTypeItemStatus.Inactive)
            {
                AddBrokenRule("Status", "Size Model Type Factor is already inactive.");
                return;
            }

            Props.Status = SizeModelTypeItemStatus.Inactive;
        }

        public void Delete()
        {
            if (Props.Status == SizeModelTypeItemStatus.Delete)
            {
                AddBrokenRule("Status", "Size Model Type Factor is already deleted.");
                return;
            }

            if (Props.Status == SizeModelTypeItemStatus.Active)
            {
                AddBrokenRule("Status", "Size Model Type Factor is active. Cannot delete.");
                return;
            }

            Props.Status = SizeModelTypeItemStatus.Delete;
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
