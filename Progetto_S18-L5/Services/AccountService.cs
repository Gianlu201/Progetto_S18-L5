using System.Data;
using Progetto_S18_L5.Data;
using Progetto_S18_L5.Models;

namespace Progetto_S18_L5.Services
{
    public class AccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUserRole(ApplicationUser user, ApplicationRole role)
        {
            try
            {
                _context.UserRoles.Add(
                    new ApplicationUserRole { UserId = user.Id, RoleId = role.Id }
                );

                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
