
namespace MyPlanner.Plannings.Domain.Test.Bootstrapper
{
    public class Criteria
    {
        public string Factor { get; set; }
        public string SizeModelTypeSelected { get; set; }

        public Criteria(string factor, string sizeModelTypeSelected)
        {
            Factor = factor;
            SizeModelTypeSelected = sizeModelTypeSelected;
        }
    }
}
