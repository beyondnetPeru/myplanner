namespace MyPlanner.Plannings.Api.Models
{
    public class SizeModelTypeFactorCostCriteria
    {
        public string Factor { get; set; }
        public string SizeModelTypeCodeSelected { get; set; }

        public SizeModelTypeFactorCostCriteria(string factor, string sizeModelTypeCodeSelected)
        {
            Factor = factor;
            SizeModelTypeCodeSelected = sizeModelTypeCodeSelected;
        }
    }
}
