using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain;

namespace MyPlanner.Plannings.Api.Services.Impl
{
    public class SizeModelTypeSprintFactorCostCalculator : ISizeModelTypeFactorCostCalculator
    {
        public double Calculate(FactorsEnum factor, string sizeModelTypeItemValueSelected, double profileAvgRate)
        {
            Guard(factor, sizeModelTypeItemValueSelected, profileAvgRate);

            var sprintCount = GetSprintCount(sizeModelTypeItemValueSelected);

            return sprintCount * profileAvgRate;
        }

        private void Guard(FactorsEnum factor,
                        string sizeModelTypeItemValueSelected,
                        double profileAvgRate)
        {
            ArgumentNullException.ThrowIfNull(factor, nameof(factor));
            ArgumentNullException.ThrowIfNull(profileAvgRate, nameof(profileAvgRate));
            ArgumentNullException.ThrowIfNull(sizeModelTypeItemValueSelected, nameof(sizeModelTypeItemValueSelected));


            if (factor != FactorsEnum.Sprints)
                throw new DomainException("Factor is not sprint.");
        }

        private int GetSprintCount(string sizeModelTypeItemValueSelected)
        {
            var sprintCount = 0;
            var tShirtSelected = sizeModelTypeItemValueSelected.ToLower().Trim();

            if (tShirtSelected == "S")
            {
                sprintCount = 1;
            }

            if (tShirtSelected == "M")
            {
                sprintCount = 2;
            }

            if (tShirtSelected == "L")
            {
                sprintCount = 3;
            }

            if (tShirtSelected == "XL")
            {
                sprintCount = 4;
            }

            if (tShirtSelected == "XXL")
            {
                sprintCount = 5;
            }

            return sprintCount;
        }

    }
}
