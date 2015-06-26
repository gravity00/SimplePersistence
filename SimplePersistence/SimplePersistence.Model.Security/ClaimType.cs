namespace SimplePersistence.Model.Security
{
    public class ClaimType : Entity<string>, IClaimType
    {
        public virtual string Description { get; set; }
    }
}