using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TennisTournament.Data
{
    public class TennisDBContext : IdentityDbContext
    {
        public TennisDBContext(DbContextOptions<TennisDBContext> options)
            : base(options)
        {
        }
    }
}