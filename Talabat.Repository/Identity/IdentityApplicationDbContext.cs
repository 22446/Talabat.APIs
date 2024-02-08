using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Identity
{
    public class IdentityApplicationDbContext : IdentityDbContext<AppUser>
    {
        public IdentityApplicationDbContext(DbContextOptions<IdentityApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Address> addresses { get; set; }
    }
}
