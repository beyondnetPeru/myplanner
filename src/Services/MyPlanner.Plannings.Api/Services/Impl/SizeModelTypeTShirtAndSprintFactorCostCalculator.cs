using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Shared.Domain;

namespace MyPlanner.Plannings.Api.Services.Impl
{
    public class SizeModelTypeTShirtAndSprintFactorCostCalculator : ISizeModelTypeFactorCostCalculator
    {
        public double Calculate(FactorsEnum factor, string sizeModelTypeItemCode, int quantity, double profileAvgRate)
        {
            Guard(factor, sizeModelTypeItemCode, profileAvgRate);

            var sprintCount = GetSprintCount(sizeModelTypeItemCode);

            return sprintCount * (quantity * profileAvgRate);
        }

        private void Guard(FactorsEnum factor,
                        string sizeModelTypeItemCode,
                        double profileAvgRate)
        {
            ArgumentNullException.ThrowIfNull(factor, nameof(factor));
            ArgumentNullException.ThrowIfNull(profileAvgRate, nameof(profileAvgRate));
            ArgumentNullException.ThrowIfNull(sizeModelTypeItemCode, nameof(sizeModelTypeItemCode));


            if (factor != FactorsEnum.Sprints)
                throw new DomainException("Factor is not sprint.");
        }

        private int GetSprintCount(string sizeModelTypeItemCode)
        {
            var sprintCount = 0;
            var tShirtSelected = sizeModelTypeItemCode.ToLower().Trim();

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
