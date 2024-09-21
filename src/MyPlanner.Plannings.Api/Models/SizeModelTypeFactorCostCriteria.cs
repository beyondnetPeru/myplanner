namespace MyPlanner.Plannings.Api.Models
{
    public class SizeModelTypeFactorCostCriteria
    {
        public string Factor { get; set; }
        public string SizeModelTypeSelected { get; set; }

        public SizeModelTypeFactorCostCriteria(string factor, string sizeModelTypeSelected)
        {
            Factor = factor;
            SizeModelTypeSelected = sizeModelTypeSelected;
        }
    }
}
