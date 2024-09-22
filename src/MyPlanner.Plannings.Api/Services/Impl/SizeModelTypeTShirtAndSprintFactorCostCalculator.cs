using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain;

namespace MyPlanner.Plannings.Api.Services.Impl
{
    public class SizeModelTypeTShirtAndSprintFactorCostCalculator : ISizeModelTypeFactorCostCalculator
    {
        public double Calculate(FactorsEnum factor, string sizeModelTypeItemValueSelected, int quantity, double profileAvgRate)
        {
            Guard(factor, sizeModelTypeItemValueSelected, profileAvgRate);

            var sprintCount = GetSprintCount(sizeModelTypeItemValueSelected);

            return sprintCount * (quantity * profileAvgRate);
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

            if (tShirtSelected == "xs")
            {
                sprintCount = 1;
            }

            if (tShirtSelected == "s")
            {
                sprintCount = 2;
            }

            if (tShirtSelected == "m")
            {
                sprintCount = 3;
            }

            if (tShirtSelected == "sm")
            {
                sprintCount = 4;
            }

            if (tShirtSelected == "l")
            {
                sprintCount = 5;
            }


            if (tShirtSelected == "xl")
            {
                sprintCount = 6;
            }

            return sprintCount;
        }

    }
}
