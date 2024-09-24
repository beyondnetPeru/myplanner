using BeyondNet.Ddd;
using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.ValueObjects;
using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public class SizeModelItemProps : IProps
    {
        public IdValueObject Id { get; private set; }
        public IdValueObject SizeModelId { get; set; }// T-Shirt
        public IdValueObject SizeModelTypeItemId { get; set; } // X, XS, S, M, L, XL, XXL, XXXL
        public FactorsEnum FactorSelected { get; private set; }
        public SizeModelProfile Profile { get; private set; } // SM - $3000, SRE - $5000
        public SizeModelTypeValueSelected SizeModelTypeSelected { get; private set; } // X, XS, S, M, L, XL, XXL, XXXL
        public SizeModelTypeQuantity Quantity { get; private set; } //1, 2, ..., 30
        public SizeModelTotalCost TotalCost { get; set; } = SizeModelTotalCost.DefaultValue(); // 0.00
        public SizeModelItemIsStandard IsStandard { get; set; }
        public Audit Audit { get; private set; }
        public SizeModelItemStatus Status { get; private set; }

        public SizeModelItemProps(IdValueObject id,
                                 IdValueObject sizeModelId,
                                 IdValueObject sizeModelTypeItemId,
                                 FactorsEnum factorSelected,
                                 SizeModelProfile profile,
                                 SizeModelTypeValueSelected sizeModelTypeSelected,
                                 SizeModelTypeQuantity quantity,
                                 SizeModelTotalCost totalCost,
                                 SizeModelItemIsStandard isStandard,
                                 UserId userId)
        {
            Id = id;
            SizeModelId = sizeModelId;
            SizeModelTypeItemId = sizeModelTypeItemId;
            FactorSelected = factorSelected;
            Profile = profile;
            SizeModelTypeSelected = sizeModelTypeSelected;
            Quantity = quantity;
            TotalCost = totalCost;
            IsStandard = isStandard;
            Audit = Audit.Create(userId.GetValue());
            Status = SizeModelItemStatus.Active;
        }

        public SizeModelItemProps(IdValueObject id,
                                 IdValueObject sizeModelId,
                                 IdValueObject sizeModelTypeItemId,
                                 FactorsEnum factorSelected,
                                 SizeModelProfile profile,
                                 SizeModelTypeValueSelected sizeModelTypeSelected,
                                 SizeModelTypeQuantity quantity,
                                 SizeModelTotalCost totalCost,
                                 SizeModelItemIsStandard isStandard,
                                 Audit audit,
                                 SizeModelItemStatus status)
        {
            Id = id;
            SizeModelId = sizeModelId;
            SizeModelTypeItemId = sizeModelTypeItemId;
            FactorSelected = factorSelected;
            Profile = profile;
            SizeModelTypeSelected = sizeModelTypeSelected;
            Quantity = quantity;
            TotalCost = totalCost;
            IsStandard = isStandard;
            Audit = audit;
            Status = status;
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
                                 SizeModelTypeItem sizeModelTypeItem,
                                 FactorsEnum factorSelected,
                                 SizeModelProfile profile,
                                 SizeModelTypeValueSelected sizeModelTypeSelected,
                                 SizeModelTypeQuantity quantity,
                                 SizeModelTotalCost totalCost,
                                 SizeModelItemIsStandard isStandard,
                                 UserId userId)
        {
            var props = new SizeModelItemProps(id, sizeModelId, sizeModelTypeItemId, factorSelected, profile, sizeModelTypeSelected, quantity, totalCost, isStandard, userId);

            return new SizeModelItem(props);
        }

        public static SizeModelItem Load(SizeModelItemProps sizeModelItem)
        {
            var props = new SizeModelItemProps(
                sizeModelItem.Id,
                sizeModelItem.SizeModel,
                sizeModelItem.SizeModelTypeItem,
                sizeModelItem.FactorSelected,
                sizeModelItem.Profile,
                sizeModelItem.SizeModelTypeSelected,
                sizeModelItem.Quantity,
                sizeModelItem.TotalCost,
                sizeModelItem.IsStandard,
                sizeModelItem.Audit,
                sizeModelItem.Status
                );

            return new SizeModelItem(props);
        }

        public void ChangeSizeModelTypeItem(SizeModelTypeItem sizeModelTypeItem, UserId userId)
        {
            GetProps().SizeModelTypeItem = sizeModelTypeItem;
            GetProps().SizeModelTypeSelected.SetValue(sizeModelTypeItem.GetPropsCopy().Code.GetValue());
            GetProps().Audit.Update(userId.GetValue());
        }

        public void ChangeFactorSelected(FactorsEnum factorSelected, UserId userId)
        {
            GetProps().FactorSelected.SetValue<FactorsEnum>(factorSelected.Id);
            GetProps().Audit.Update(userId.GetValue());
        }

        public void ChangeQuantity(int quantity, UserId userId)
        {
            GetProps().Quantity.SetValue(quantity);
            GetProps().Audit.Update(userId.GetValue());
        }

        public void ChangeTotalCost(double totalCost, UserId userId)
        {
            GetProps().TotalCost.SetValue(totalCost);
            GetProps().Audit.Update(userId.GetValue());
        }

        public void ChangeIsStandard(bool isStandard, UserId userId)
        {
            GetProps().IsStandard.SetValue(isStandard);
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

        public void Delete(UserId userId)
        {
            if (GetPropsCopy().Status == SizeModelItemStatus.Delete)
            {
                AddBrokenRule("Size", "SizeModelItem is already Deleted.");
                return;
            }

            GetProps().Status.SetValue<SizeModelItemStatus>(SizeModelItemStatus.Delete.Id);
            GetProps().Audit.Update(userId.GetValue());
        }

    }

    public class SizeModelItemStatus : Enumeration
    {
        public static SizeModelItemStatus Delete = new SizeModelItemStatus(-1, nameof(Delete).ToLowerInvariant());
        public static SizeModelItemStatus Active = new SizeModelItemStatus(1, nameof(Active).ToLowerInvariant());
        public static SizeModelItemStatus Inactive = new SizeModelItemStatus(0, nameof(Inactive).ToLowerInvariant());

        public SizeModelItemStatus(int id, string name)
            : base(id, name)
        {
        }
    }
}
