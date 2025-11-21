using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CMCS.Web.Models;

namespace CMCS.Web.Data
{
    public class CMCSContext : IdentityDbContext
    {
        public CMCSContext(DbContextOptions<CMCSContext> options)
            : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }
    }
}
