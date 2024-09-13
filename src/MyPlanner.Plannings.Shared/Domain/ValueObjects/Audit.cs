using BeyondNet.Ddd;
using MyPlanner.Plannings.Shared.Helpers;


namespace MyPlanner.Plannings.Shared.Domain.ValueObjects
{
    public struct AuditProps
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string TimeSpan { get; set; }

        override public string ToString()
        {
            return $"CreatedBy: {CreatedBy}, CreatedAt: {CreatedAt}, UpdatedBy: {UpdatedBy}, UpdatedAt: {UpdatedAt}, TimeSpan: {TimeSpan}";
        }
    }

    public class Audit : ValueObject<AuditProps>
    {
        private Audit(AuditProps value) : base(value)
        {
        }

        public static Audit Create(string createdBy)
        {
            return new Audit(new AuditProps
            {
                CreatedBy = createdBy,
                CreatedAt = DateTime.Today.ToUniversalTime(),
                TimeSpan = TimeStampHelper.GetTimeStamp(),
            });
        }

        public static Audit Load(AuditProps props)
        {
            return new Audit(props);
        }

        public void Update(string updatedBy)
        {
            this.SetValue(new AuditProps
            {
                CreatedBy = this.Value.CreatedBy,
                CreatedAt = this.Value.CreatedAt,
                UpdatedBy = updatedBy,
                UpdatedAt = DateTime.Today.ToUniversalTime(),
                TimeSpan = TimeStampHelper.GetTimeStamp(),
            });
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.CreatedBy;
            yield return Value.CreatedAt;
            yield return Value.UpdatedBy!;
            yield return Value.UpdatedAt!;
            yield return Value.TimeSpan;
        }
    }
}
