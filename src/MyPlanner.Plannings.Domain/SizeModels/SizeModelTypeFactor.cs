using BeyondNet.Ddd;
using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.ValueObjects;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public class SizeModelTypeFactorProps : IProps
    {
        public IdValueObject Id { get; private set; }
        public SizeModelTypeFactorCode Code { get; private set; }
        public Name Name { get; private set; }
        public SizeModelType SizeModelType { get; private set; }

        public SizeModelTypeFactorStatus Status { get; private set; }

        public SizeModelTypeFactorProps(IdValueObject id, SizeModelTypeFactorCode code, Name name, SizeModelType sizeModelType)
        {
            Id = id;
            Code = code;
            Name = name;
            SizeModelType = sizeModelType;
            Status = SizeModelTypeFactorStatus.Active;
        }

        public SizeModelTypeFactorProps(IdValueObject id, SizeModelTypeFactorCode code, Name name, SizeModelType sizeModelType, SizeModelTypeFactorStatus sizeModelTypeFactorStatus)
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

    public class SizeModelTypeFactor : Entity<SizeModelTypeFactor, SizeModelTypeFactorProps>
    {
        private SizeModelTypeFactor(SizeModelTypeFactorProps props) : base(props)
        {
        }

        public static SizeModelTypeFactor Create(IdValueObject id, SizeModelTypeFactorCode code, Name name, SizeModelType sizeModelType)
        {
            return new SizeModelTypeFactor(new SizeModelTypeFactorProps(id, code, name, sizeModelType));
        }

        public static SizeModelTypeFactor Load(IdValueObject id, SizeModelTypeFactorCode code, Name name, SizeModelType sizeModelType, SizeModelTypeFactorStatus sizeModelTypeFactorStatus)
        {
            return new SizeModelTypeFactor(new SizeModelTypeFactorProps(id, code, name, sizeModelType, sizeModelTypeFactorStatus));
        }

        public void ChangeName(Name name)
        {
            GetProps().Name.SetValue(name.GetValue());
        }

        public void ChangeCode(SizeModelTypeFactorCode code)
        {
            GetProps().Code.SetValue(code.GetValue());
        }

        public void Activate()
        {
            if (GetProps().Status == SizeModelTypeFactorStatus.Active)
            {
                AddBrokenRule("Status", "Size Model Type Factor is already active.");
                return;
            }

            GetProps().Status.SetValue<SizeModelItemStatus>(SizeModelTypeFactorStatus.Active.Id);
        }

        public void Deactivate()
        {
            if (GetProps().Status == SizeModelTypeFactorStatus.Inactive)
            {
                AddBrokenRule("Status", "Size Model Type Factor is already inactive.");
                return;
            }

            GetProps().Status.SetValue<SizeModelTypeFactorStatus>(SizeModelTypeFactorStatus.Inactive.Id);
        }
    }

    public class SizeModelTypeFactorStatus : Enumeration
    {
        public static SizeModelTypeFactorStatus Active = new SizeModelTypeFactorStatus(1, "Active");
        public static SizeModelTypeFactorStatus Inactive = new SizeModelTypeFactorStatus(0, "Inactive");

        public SizeModelTypeFactorStatus(int id, string name) : base(id, name)
        { }
    }
}
