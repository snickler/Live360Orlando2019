using IdentityDemo_Scaffolding.Data;
using IdentityDemo_Scaffolding.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityDemo_Scaffolding.Stores
{
    public class SqlIdentityUserStore : IUserStore<SqlIdentityUser>, IUserPasswordStore<SqlIdentityUser>, IUserEmailStore<SqlIdentityUser>, IUserPhoneNumberStore<SqlIdentityUser>, IUserRoleStore<SqlIdentityUser>, IUserTwoFactorStore<SqlIdentityUser>, IUserAuthenticatorKeyStore<SqlIdentityUser>, IUserAuthenticationTokenStore<SqlIdentityUser>, IQueryableUserStore<SqlIdentityUser>
    {
        private readonly SqlIdentityDbContext _context;
        private readonly MFATokenDbContext _mfaTokenContext;
        private const string AuthenticatorStoreLoginProvider = "[SqlIdentityAuthenticatorStore]";
        private const string AuthenticatorKeyTokenName = "AuthenticatorKey";
        private const string RecoveryCodeTokenName = "RecoveryCodes";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">The SqlIdentityDbContext</param>
        /// <param name="tokenRepository">The TokenRepository</param>
        public SqlIdentityUserStore(SqlIdentityDbContext context, MFATokenDbContext mfaTokenContext)
        {
            _context = context;
            _mfaTokenContext = mfaTokenContext;
            
        }


        private bool alreadyDisposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
        protected virtual void Dispose(bool isDisposing)
        {
            if (alreadyDisposed)
                return;

            alreadyDisposed = true;
        }

        public IQueryable<SqlIdentityUser> Users => _context.Users;

        public IQueryable<IdentityRole<Guid>> Roles => _context.Roles.FromSqlRaw(@"SELECT RoleId as Id, RoleName as Name, UPPER(RoleName) as NormalizedName, convert(nvarchar(50),NewID()) as ConcurrencyStamp from aspnet_Roles");

        public IQueryable<IdentityUserRole<Guid>> UserRoles => _context.UserRoles.FromSqlRaw("SELECT UserId, RoleId FROM aspnet_UsersInRoles");

        public Task<IdentityResult> CreateAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<SqlIdentityUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await Users.FirstOrDefaultAsync(x => x.NormalizedEmail == normalizedEmail);
        }

        public async Task<SqlIdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var query = Users.Where(x => x.Id.ToString() == userId);

            return await query.Take(1).FirstOrDefaultAsync();
        }

        public async Task<SqlIdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var normalized = normalizedUserName.ToLower();
            var user = Users.Where(x => x.UserName == normalized).Take(1);
            var ret = user.FirstOrDefault();
            return await Task.FromResult(ret);

        }

        public Task<string> GetEmailAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task<string> GetNormalizedEmailAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<string> GetNormalizedUserNameAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetPhoneNumberAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task<string> GetUserIdAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetEmailAsync(SqlIdentityUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(SqlIdentityUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(SqlIdentityUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(SqlIdentityUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(SqlIdentityUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetPhoneNumberAsync(SqlIdentityUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetPhoneNumberConfirmedAsync(SqlIdentityUser user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(SqlIdentityUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public Task AddToRoleAsync(SqlIdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(SqlIdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userId = user.Id;
            return await
                UserRoles
                .Join(Roles,
                        userRole => userRole.RoleId,
                        role => role.Id,
                        (userRole, role) => new { UserRole = userRole, Role = role })
                .Where(x => x.UserRole.UserId == userId).Select(x => x.Role.Name).ToListAsync(cancellationToken);
        }

        public Task<bool> IsInRoleAsync(SqlIdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return
                UserRoles
                .Join(Roles,
                      userRole => userRole.RoleId,
                      role => role.Id,
                      (userRole, role) => new { UserRole = userRole, Role = role })
                .AnyAsync(x => x.UserRole.UserId == user.Id && x.Role.Name == roleName);
        }

        public async Task<IList<SqlIdentityUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await
                     UserRoles
                     .Join(Users,
                           userRole => userRole.RoleId,
                           user => user.Id,
                           (UserRole, User) => new { UserRole, User })
                     .Join(Roles,
                           userRole => userRole.UserRole.RoleId,
                           role => role.Id,
                           (UserRole2, Role) => new { UserRole2, Role })
                     .Where(x => x.Role.Name == roleName)
                     .Select(x => x.UserRole2.User)
                     .ToListAsync();

        }

        public Task SetAuthenticatorKeyAsync(SqlIdentityUser user, string key, CancellationToken cancellationToken)
        {
            return SetTokenAsync(user, AuthenticatorStoreLoginProvider, AuthenticatorKeyTokenName, key, cancellationToken);
        }

        public Task<string> GetAuthenticatorKeyAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            return GetTokenAsync(user, AuthenticatorStoreLoginProvider, AuthenticatorKeyTokenName, cancellationToken);
        }

        public Task SetTwoFactorEnabledAsync(SqlIdentityUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = true;
            return Task.CompletedTask;
        }

        public async Task<bool> GetTwoFactorEnabledAsync(SqlIdentityUser user, CancellationToken cancellationToken)
        {
            return await _mfaTokenContext.UserTokens.AnyAsync(x => x.UserId == user.Id.ToString());
        }

        public async Task SetTokenAsync(SqlIdentityUser user, string loginProvider, string name, string value, CancellationToken cancellationToken)
        {
            var tokenEntity = await _mfaTokenContext.UserTokens.SingleOrDefaultAsync(
            l =>
                l.Name == name && l.LoginProvider == loginProvider &&
                l.UserId == user.Id.ToString());
            if (tokenEntity != null)
            {
                tokenEntity.Value = value;
                _mfaTokenContext.Update(tokenEntity);
            }
            else
            {
                _mfaTokenContext.UserTokens.Add(new UserToken
                {
                    UserId = user.Id.ToString(),
                    LoginProvider = loginProvider,
                    Name = name,
                    Value = value
                });
            }
            await _mfaTokenContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveTokenAsync(SqlIdentityUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            var tokenEntity = await _mfaTokenContext.UserTokens.SingleOrDefaultAsync(
    l =>
        l.Name == name && l.LoginProvider == loginProvider &&
        l.UserId == user.Id.ToString());
            if (tokenEntity != null)
            {
                _mfaTokenContext.UserTokens.Remove(tokenEntity);
            }
            await _mfaTokenContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<string> GetTokenAsync(SqlIdentityUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            var tokenEntity = await _mfaTokenContext.UserTokens.SingleOrDefaultAsync(
            l =>
                l.Name == name && l.LoginProvider == loginProvider &&
                l.UserId == user.Id.ToString());

            return tokenEntity?.Value;
        }
    }
}
