using BeyondNet.Ddd;
using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.ValueObjects;
using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public class SizeModelProps : IProps
    {
        public IdValueObject Id { get; private set; }
        public IdValueObject SizeModelTypeId { get; private set; } // TShirt
        public SizeModelTypeCode SizeModelTypeCode { get; private set; } // TShirt
        public Name Name { get; private set; } // TShirt size for Squads
        public ICollection<SizeModelItem> Items { get; private set; }
        public Audit Audit { get; set; }
        public SizeModelStatus Status { get; private set; } = SizeModelStatus.Active;

        public SizeModelProps(IdValueObject id, IdValueObject sizeModelTypeId, SizeModelTypeCode sizeModelTypeCode, Name name, UserId userId)
        {
            Id = id;
            SizeModelTypeId = sizeModelTypeId;
            SizeModelTypeCode = sizeModelTypeCode;
            Name = name;
            Items = new List<SizeModelItem>();
            Audit = Audit.Create(userId.GetValue());
            Status = SizeModelStatus.Active;
        }

        public SizeModelProps(IdValueObject id, IdValueObject sizeModelTypeId, SizeModelTypeCode sizeModelTypeCode, Name name, Audit audit, SizeModelStatus status)
        {
            Id = id;
            SizeModelTypeId = sizeModelTypeId;
            SizeModelTypeCode = sizeModelTypeCode;
            Name = name;
            Items = new List<SizeModelItem>();
            Audit = audit;
            Status = status;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class SizeModel : Entity<SizeModel, SizeModelProps>
    {
        private SizeModel(SizeModelProps props) : base(props)
        {
        }

        public static SizeModel Create(IdValueObject id, IdValueObject sizeModelTypeId, SizeModelTypeCode sizeModelTypeCode, Name name, UserId userId)
        {
            return new SizeModel(new SizeModelProps(id, sizeModelTypeId, sizeModelTypeCode, name, userId));
        }

        public static SizeModel Create(IdValueObject id, IdValueObject sizeModelTypeId, SizeModelTypeCode sizeModelTypeCode, Name name, Audit audit, SizeModelStatus status)
        {
            return new SizeModel(new SizeModelProps(id, sizeModelTypeId, sizeModelTypeCode, name, audit, status));
        }

        public static SizeModel Load(SizeModelProps sizeModelProps)
        {

            return Create(sizeModelProps.Id,
                          sizeModelProps.SizeModelTypeId,
                          sizeModelProps.SizeModelTypeCode,
                          sizeModelProps.Name,
                          sizeModelProps.Audit,
                          sizeModelProps.Status);
        }

        public void AddItem(SizeModelItem sizeModelItem, UserId userId)
        {
            GetProps().Items.Add(sizeModelItem);
            GetProps().Audit.Update(userId.GetValue());
        }

        public void RemoveItem(SizeModelItem sizeModelItem, UserId userId)
        {
            if (!GetPropsCopy().Items.Any(x => x.GetPropsCopy().Id == sizeModelItem.GetPropsCopy().Id))
            {
                AddBrokenRule("SizeModel", "SizeModelItem does not exist");
                return;
            }

            GetProps().Items.Remove(sizeModelItem);
            GetProps().Audit.Update(userId.GetValue());
        }

        public void ChangeName(Name name, UserId userId)
        {
            GetProps().Name.SetValue(name.GetValue());
            GetProps().Audit.Update(userId.GetValue());
        }

        public void Activate(UserId userId)
        {
            if (GetProps().Status == SizeModelStatus.Active)
            {
                AddBrokenRule("SizeModel", "SizeModel is already active");
                return;
            }

            GetProps().Status.SetValue<SizeModelStatus>(SizeModelStatus.Active.Id);
            GetProps().Audit.Update(userId.GetValue());
        }

        public void Deactivate(UserId userId)
        {
            var count = GetProps().Items.Where(x => x.GetPropsCopy().Status == SizeModelItemStatus.Active).Count();

            if (count > 0)
            {
                AddBrokenRule("SizeModel", "SizeModel has active SizeModelItems");
                return;
            }

            GetProps().Status.SetValue<SizeModelStatus>(SizeModelStatus.Deactivated.Id);
            GetProps().Audit.Update(userId.GetValue());
        }

        public void Delete(UserId userId)
        {
            if (GetProps().Status == SizeModelStatus.Deleted)
            {
                AddBrokenRule("SizeModel", "SizeModel is already deleted");
                return;
            }

            foreach (var item in GetPropsCopy().Items)
            {
                if (item.GetPropsCopy().Status == SizeModelItemStatus.Active)
                {
                    AddBrokenRule("SizeModel", "SizeModel has active SizeModelItems");
                    return;
                }
            }

            GetProps().Status.SetValue<SizeModelStatus>(SizeModelStatus.Deleted.Id);
            GetProps().Audit.Update(userId.GetValue());
        }
    }

    public class SizeModelStatus : Enumeration
    {
        public static SizeModelStatus Deleted = new SizeModelStatus(0, "Deleted");
        public static SizeModelStatus Active = new SizeModelStatus(1, "Active");
        public static SizeModelStatus Deactivated = new SizeModelStatus(2, "Deactivated");

        public SizeModelStatus(int id, string name) : base(id, name)
        {
        }
    }
}
