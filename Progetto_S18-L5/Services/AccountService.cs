using System.Data;
using Microsoft.AspNetCore.Identity;
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

        public async Task<bool> EditEmployeeAsync(
            EditEmployeeViewModel editEmployee,
            ApplicationUser oldEmp
        )
        {
            try
            {
                // utente che ha lo stesso id di editEmployee e vado ad includere il suo ruolo
                var employee = _context.Users.FirstOrDefault(u =>
                    u.Id == editEmployee.Id
                    && u.ApplicationUserRole != null
                    && u.ApplicationUserRole.Count > 0
                );

                //var employee = await _context.Users.FindAsync(editEmployee.Id);

                var userRole = _context.UserRoles.FirstOrDefault(ur =>
                    ur.UserId == editEmployee.Id
                    && ur.RoleId == oldEmp.ApplicationUserRole.FirstOrDefault().RoleId
                );

                if (employee == null)
                {
                    return false;
                }

                if (userRole == null)
                {
                    return false;
                }

                employee.FirstName = editEmployee.FirstName;
                employee.LastName = editEmployee.LastName;
                employee.Email = editEmployee.Email;
                employee.NormalizedEmail = editEmployee.Email.ToUpper();
                employee.UserName = editEmployee.Email;
                employee.NormalizedUserName = editEmployee.Email.ToUpper();

                if (userRole.RoleId != editEmployee.RoleId)
                {
                    _context.UserRoles.Remove(userRole);

                    _context.UserRoles.Add(
                        new ApplicationUserRole
                        {
                            UserId = employee.Id,
                            RoleId = editEmployee.RoleId,
                        }
                    );
                }

                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
