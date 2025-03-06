using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_App.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByUsernameAsync(string username);
        Task<bool> ValidatePasswordAsync(ApplicationUser user, string password);
    }
}
