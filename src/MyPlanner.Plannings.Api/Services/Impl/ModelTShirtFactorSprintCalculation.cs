using BeyondNet.Ddd;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain;

namespace MyPlanner.Plannings.Api.Services.Impl
{
    public class ModelTShirtFactorSprintCalculation : IFactorModelSizeCalculation
    {

        private void Guard(string sizeModelTypeFactor,
                           string sizeModelTypeValueSelected,
                           double profileAvgRate)
        {
            ArgumentNullException.ThrowIfNull(sizeModelTypeFactor, nameof(sizeModelTypeFactor));
            ArgumentNullException.ThrowIfNull(profileAvgRate, nameof(profileAvgRate));
            ArgumentNullException.ThrowIfNull(sizeModelTypeValueSelected, nameof(sizeModelTypeValueSelected));


            if (sizeModelTypeFactor != "tshirt")
                throw new DomainException("SizeModelType is not TShirt");
        }

        private int GetSprintCount(TShirtSizeEnum tShirtSelected)
        {
            var sprintCount = 0;

            if (tShirtSelected == TShirtSizeEnum.S)
            {
                sprintCount = 1;
            }

            if (tShirtSelected == TShirtSizeEnum.M)
            {
                sprintCount = 2;
            }

            if (tShirtSelected == TShirtSizeEnum.L)
            {
                sprintCount = 3;
            }

            if (tShirtSelected == TShirtSizeEnum.XL)
            {
                sprintCount = 4;
            }

            if (tShirtSelected == TShirtSizeEnum.XXL)
            {
                sprintCount = 5;
            }

            return sprintCount;
        }

        public double Calculate(string sizeModelTypeFactor, string sizeModelTypeValueSelected, double profileAvgRate)
        {
            Guard(sizeModelTypeFactor, sizeModelTypeValueSelected, profileAvgRate);

            var tShirtSelected = Enumeration.FromDisplayName<TShirtSizeEnum>(sizeModelTypeValueSelected);

            var sprintCount = GetSprintCount(tShirtSelected);

            return sprintCount * profileAvgRate;
        }
    }
}
