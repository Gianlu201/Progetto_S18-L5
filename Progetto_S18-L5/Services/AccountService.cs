using System.Data;
using Progetto_S18_L5.Data;
using Progetto_S18_L5.Models;
using Progetto_S18_L5.ViewModels;

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

        public async Task<bool> EditClientAsync(EditClientViewModel editClient)
        {
            try
            {
                var client = await _context.Users.FindAsync(editClient.Id);

                if (client == null)
                {
                    return false;
                }

                client.FirstName = editClient.FirstName;
                client.LastName = editClient.LastName;
                client.Email = editClient.Email;
                client.NormalizedEmail = editClient.Email.ToUpper();
                client.UserName = editClient.Email;
                client.NormalizedUserName = editClient.Email.ToUpper();

                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
