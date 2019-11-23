namespace IdentityDemo_Scaffolding.Models
{
    public class AspNetRoleClaims
    {
        public int Id { get; set; }
        public virtual string UserId { get;set;}
        public virtual string ClaimType { get; set; }
        public virtual string ClaimValue { get; set; }
    }
}
