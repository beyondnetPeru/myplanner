namespace MyPlanner.Plannings.Api.Boostrapper
{
    public class FactorCostCriteria
    {
        public string Factor { get; set; }
        public string SizeModelTypeCodeSelected { get; set; }

        public FactorCostCriteria(string factor, string sizeModelTypeCodeSelected)
        {
            Factor = factor;
            SizeModelTypeCodeSelected = sizeModelTypeCodeSelected;
        }
    }
}
