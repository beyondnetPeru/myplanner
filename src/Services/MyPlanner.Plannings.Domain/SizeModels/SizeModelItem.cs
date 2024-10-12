using MyPlanner.Plannings.Domain.SizeModelTypes;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public class SizeModelItemProps : IProps
    {
        public IdValueObject Id { get; private set; }
        public IdValueObject SizeModelId { get; private set; }// T-Shirt
        public IdValueObject SizeModelTypeItemId { get; private set; } // X, XS, S, M, L, XL, XXL, XXXL
        public SizeModelTypeItemCode SizeModelTypeItemCode { get; private set; } // X, XS, S, M, L, XL, XXL, XXXL
        public FactorsEnum FactorSelected { get; set; }
        public SizeModelProfile Profile { get; private set; } // SM - $3000, SRE - $5000
        public SizeModelTypeQuantity Quantity { get; private set; } //1, 2, ..., 30
        public SizeModelTotalCost TotalCost { get; set; } = SizeModelTotalCost.DefaultValue(); // 0.00
        public SizeModelItemIsStandard IsStandard { get; set; }
        public Audit Audit { get; private set; }
        public SizeModelItemStatus Status { get; set; }

        public SizeModelItemProps(IdValueObject id,
                                 IdValueObject sizeModelId,
                                 IdValueObject sizeModelTypeItemId,
                                 SizeModelTypeItemCode sizeModelTypeItemCode,
                                 FactorsEnum factorSelected,
                                 SizeModelProfile profile,
                                 SizeModelTypeQuantity quantity,
                                 SizeModelTotalCost totalCost,
                                 SizeModelItemIsStandard isStandard,
                                 UserId userId)
        {
            Id = id;
            SizeModelId = sizeModelId;
            SizeModelTypeItemId = sizeModelTypeItemId;
            SizeModelTypeItemCode = sizeModelTypeItemCode;
            FactorSelected = factorSelected;
            Profile = profile;
            Quantity = quantity;
            TotalCost = totalCost;
            IsStandard = isStandard;
            Audit = Audit.Create(userId.GetValue());
            Status = SizeModelItemStatus.Active;
        }

        public SizeModelItemProps(IdValueObject id,
                                 IdValueObject sizeModelId,
                                 IdValueObject sizeModelTypeItemId,
                                 SizeModelTypeItemCode sizeModelTypeItemCode,
                                 FactorsEnum factorSelected,
                                 SizeModelProfile profile,
                                 SizeModelTypeQuantity quantity,
                                 SizeModelTotalCost totalCost,
                                 SizeModelItemIsStandard isStandard,
                                 Audit audit,
                                 SizeModelItemStatus status)
        {
            Id = id;
            SizeModelId = sizeModelId;
            SizeModelTypeItemId = sizeModelTypeItemId;
            SizeModelTypeItemCode = sizeModelTypeItemCode;
            FactorSelected = factorSelected;
            Profile = profile;
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
                                 IdValueObject sizeModelId,
                                 IdValueObject sizeModelTypeItemId,
                                 SizeModelTypeItemCode sizeModelTypeItemCode,
                                 FactorsEnum factorSelected,
                                 SizeModelProfile profile,
                                 SizeModelTypeQuantity quantity,
                                 SizeModelTotalCost totalCost,
                                 SizeModelItemIsStandard isStandard,
                                 UserId userId)
        {
            var props = new SizeModelItemProps(id, sizeModelId, sizeModelTypeItemId, sizeModelTypeItemCode, factorSelected, profile, quantity, totalCost, isStandard, userId);

            return new SizeModelItem(props);
        }

        public static SizeModelItem Load(SizeModelItemProps sizeModelItem)
        {
            var props = new SizeModelItemProps(
                sizeModelItem.Id,
                sizeModelItem.SizeModelId,
                sizeModelItem.SizeModelTypeItemId,
                sizeModelItem.SizeModelTypeItemCode,
                sizeModelItem.FactorSelected,
                sizeModelItem.Profile,
                sizeModelItem.Quantity,
                sizeModelItem.TotalCost,
                sizeModelItem.IsStandard,
                sizeModelItem.Audit,
                sizeModelItem.Status
                );

            return new SizeModelItem(props);
        }

        public void ChangeSizeModelTypeItem(IdValueObject sizeModelTypeItemId, SizeModelTypeItemCode sizeModelTypeItemCode, UserId userId)
        {
            Props.SizeModelTypeItemId.SetValue(sizeModelTypeItemId.GetValue());
            Props.SizeModelTypeItemCode.SetValue(sizeModelTypeItemCode.GetValue());
            Props.Audit.Update(userId.GetValue());
        }

        public void ChangeFactorSelected(FactorsEnum factorSelected, UserId userId, double totalCost)
        {
            Props.FactorSelected = factorSelected;
            Props.TotalCost.SetValue(totalCost);
            Props.Audit.Update(userId.GetValue());
        }

        public void ChangeQuantity(int quantity, double totalCost, UserId userId)
        {
            Props.Quantity.SetValue(quantity);
            Props.TotalCost.SetValue(totalCost);
            Props.Audit.Update(userId.GetValue());
        }

        public void ChangeTotalCost(double totalCost, UserId userId)
        {
            Props.TotalCost.SetValue(totalCost);
            Props.Audit.Update(userId.GetValue());
        }

        public void ChangeIsStandard(bool isStandard, UserId userId)
        {
            Props.IsStandard.SetValue(isStandard);
            Props.Audit.Update(userId.GetValue());
        }

        public void Activate(UserId userId)
        {
            if (GetPropsCopy().Status == SizeModelItemStatus.Active)
            {
                AddBrokenRule("Size", "SizeModelItem is already Active.");
                return;
            }

            Props.Status = SizeModelItemStatus.Active;
            Props.Audit.Update(userId.GetValue());
        }

        public void Deactivate(UserId userId)
        {
            if (GetPropsCopy().Status == SizeModelItemStatus.Deactivated)
            {
                AddBrokenRule("Size", "SizeModelItem is already Inactive.");
                return;
            }

            Props.Status = SizeModelItemStatus.Deactivated;
            Props.Audit.Update(userId.GetValue());
        }

        public void Delete(UserId userId)
        {
            if (GetPropsCopy().Status == SizeModelItemStatus.Deleted)
            {
                AddBrokenRule("Size", "SizeModelItem is already Deleted.");
                return;
            }

            Props.Status = SizeModelItemStatus.Deleted;
            Props.Audit.Update(userId.GetValue());
        }

    }

    public class SizeModelItemStatus : Enumeration
    {
        public static SizeModelItemStatus Deleted = new SizeModelItemStatus(0, nameof(Deleted).ToLowerInvariant());
        public static SizeModelItemStatus Active = new SizeModelItemStatus(1, nameof(Active).ToLowerInvariant());
        public static SizeModelItemStatus Deactivated = new SizeModelItemStatus(2, nameof(Deactivated).ToLowerInvariant());

        public SizeModelItemStatus(int id, string name)
            : base(id, name)
        {
        }
    }
}
