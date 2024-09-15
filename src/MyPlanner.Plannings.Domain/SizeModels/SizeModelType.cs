using BeyondNet.Ddd;
using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.ValueObjects;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public class SizeModelTypeProps : IProps
    {
        public IdValueObject Id { get; private set; }
        public SizeModelTypeCode Code { get; private set; }
        public Name Name { get; private set; }
        public ICollection<SizeModelTypeFactor> Factors { get; private set; } = new List<SizeModelTypeFactor>();
        public SizeModelTypeStatus Status { get; private set; }

        public SizeModelTypeProps(IdValueObject id, SizeModelTypeCode code, Name name)
        {
            Id = id;
            Code = code;
            Name = name;
            Factors = new List<SizeModelTypeFactor>();
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
            return new SizeModelTypeProps(Id, Code, Name, Status);
        }
    }

    public class SizeModelType : Entity<SizeModelType, SizeModelTypeProps>
    {
        private SizeModelType(SizeModelTypeProps props) : base(props)
        {
        }

        public static SizeModelType Create(IdValueObject id, SizeModelTypeCode code, Name value)
        {
            return new SizeModelType(new SizeModelTypeProps(id, code, value));
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

            return SizeModelType.Create(props);

        }

        public void ChangeCode(SizeModelTypeCode value)
        {
            GetProps().Code.SetValue(value.GetValue());
        }

        public void ChangeName(Name value)
        {
            GetProps().Name.SetValue(value.GetValue());
        }

        public void AddFactor(SizeModelTypeFactor factor)
        {
            if (GetProps().Factors.Contains(factor))
            {
                AddBrokenRule("Factor", "Factor already exists in the list");
                return;
            }

            GetProps().Factors.Add(factor);
        }

        public void RemoveFactor(SizeModelTypeFactor factor)
        {
            if (!GetProps().Factors.Contains(factor))
            {
                AddBrokenRule("Factor", "Factor does not exist in the list");
                return;
            }

            GetProps().Factors.Remove(factor);
        }

        public void Deactivate()
        {
            if (GetPropsCopy().Status == SizeModelTypeStatus.Inactive)
            {
                AddBrokenRule("Status", "Size Model Type is already inactive");
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
    }

    public class SizeModelTypeStatus : Enumeration
    {

        public static SizeModelTypeStatus Active = new SizeModelTypeStatus(1, nameof(Active).ToLowerInvariant());
        public static SizeModelTypeStatus Inactive = new SizeModelTypeStatus(0, nameof(Inactive).ToLowerInvariant());

        public SizeModelTypeStatus(int id, string name) : base(id, name)
        {
        }
    }
}
