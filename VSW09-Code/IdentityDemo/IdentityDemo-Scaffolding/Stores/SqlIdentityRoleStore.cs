using IdentityDemo_Scaffolding.Data;
using IdentityDemo_Scaffolding.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityDemo_Scaffolding.Stores
{
    public class SqlIdentityRoleStore : RoleStore<SqlIdentityRole, SqlIdentityDbContext, Guid, IdentityUserRole<Guid>, IdentityRoleClaim<Guid>>
    {
        private readonly SqlIdentityDbContext _context;
        public SqlIdentityRoleStore(SqlIdentityDbContext context) : base(context)
        {
            _context = context;
        }


        public IQueryable<IdentityRoleClaim<Guid>> RoleClaims => Enumerable.Empty<IdentityRoleClaim<Guid>>().AsQueryable();
        public override IQueryable<SqlIdentityRole> Roles => _context.Roles.FromSqlRaw(@"SELECT RoleId as Id, RoleName as Name, UPPER(RoleName) as NormalizedName, convert(nvarchar(50),NewID()) as ConcurrencyStamp from aspnet_Roles");

    }
}
