﻿using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.Services.Impl
{
    public class FactorCostCalculatorDefault : IFactorCostCalculator
    {
        public double Calculate(FactorsEnum factor, string sizeModelTypeItemValueSelected, int quantity, double profileAvgRate)
        {
            return 0.00;
        }
    }
}
