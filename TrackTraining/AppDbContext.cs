using Microsoft.AspNet.Identity.EntityFramework;


namespace TrackTraining
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
            : base("MyConnection", throwIfV1Schema: false)
        {
        }
    }
}