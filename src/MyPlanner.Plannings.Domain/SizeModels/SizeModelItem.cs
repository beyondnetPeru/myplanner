using BeyondNet.Ddd;
using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.ValueObjects;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public class SizeModelItemProps : IProps
    {
        public IdValueObject Id { get; private set; }
        public SizeModel SizeModel { get; private set; }
        public SizeModelProfile Profile { get; private set; } // SM, SRE, FE, BE, DB, QA, PM
        public SizeModelTypeFactor SizeModelTypeFactor { get; private set; } // Enum: Percentage, Flat
        public SizeModelTypeValueSelected ProfileValueSelected { get; private set; } // 0, 0.00, %
        public SizeModelTypeQuantity Quantity { get; private set; } //1, 2, ..., 30
        public SizeModelTotalCost TotalCost { get; set; } = SizeModelTotalCost.DefaultValue(); // 0.00
        public Audit Audit { get; private set; }
        public SizeModelItemStatus Status { get; private set; }

        public SizeModelItemProps(IdValueObject id,
                                 SizeModel sizeModel,
                                 SizeModelProfile profile,
                                 SizeModelTypeFactor sizeModelTypeFactor,
                                 SizeModelTypeValueSelected valueSelected,
                                 SizeModelTypeQuantity quantity,
                                 SizeModelTotalCost totalCost,
                                 UserId userId)
        {
            Id = id;
            SizeModel = sizeModel;
            Profile = profile;
            SizeModelTypeFactor = sizeModelTypeFactor;
            ProfileValueSelected = valueSelected;
            Quantity = quantity;
            TotalCost = totalCost;
            Audit = Audit.Create(userId.GetValue());
            Status = SizeModelItemStatus.Active;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class SizeModelItem : Entity<SizeModelItem, SizeModelItemProps>
    {
        private SizeModelItem(SizeModelItemProps props) : base(props)
        {

        }

        public static SizeModelItem Create(IdValueObject id,
                                           SizeModel sizeModel,
                                           SizeModelProfile profile,
                                           SizeModelTypeFactor sizeModelTypeFactor,
                                           SizeModelTypeValueSelected valueSelected,
                                           SizeModelTypeQuantity quantity, SizeModelTotalCost totalCost, UserId userId)
        {
            var props = new SizeModelItemProps(id, sizeModel, profile, sizeModelTypeFactor, valueSelected, quantity, totalCost, userId);

            return new SizeModelItem(props);
        }

        public void SetQuantity(int quantity, UserId userId)
        {
            GetProps().Quantity.SetValue(quantity);
            GetProps().Audit.Update(userId.GetValue());
        }

        public void SetTotalCost(double totalCost, UserId userId)
        {
            GetProps().TotalCost.SetValue(totalCost);
            GetProps().Audit.Update(userId.GetValue());
        }

        public void Activate(UserId userId)
        {
            if (GetPropsCopy().Status == SizeModelItemStatus.Active)
            {
                AddBrokenRule("Size", "SizeModelItem is already Active.");
                return;
            }

            GetProps().Status.SetValue<SizeModelItemStatus>(SizeModelItemStatus.Active.Id);
            GetProps().Audit.Update(userId.GetValue());

        }

        public void Deactivate(UserId userId)
        {
            if (GetPropsCopy().Status == SizeModelItemStatus.Inactive)
            {
                AddBrokenRule("Size", "SizeModelItem is already Inactive.");
                return;
            }

            GetProps().Status.SetValue<SizeModelItemStatus>(SizeModelItemStatus.Inactive.Id);
            GetProps().Audit.Update(userId.GetValue());
        }

    }

    public class SizeModelItemStatus : Enumeration
    {
        public static SizeModelItemStatus Active = new SizeModelItemStatus(1, nameof(Active).ToLowerInvariant());
        public static SizeModelItemStatus Inactive = new SizeModelItemStatus(0, nameof(Inactive).ToLowerInvariant());

        public SizeModelItemStatus(int id, string name)
            : base(id, name)
        {
        }
    }


}
