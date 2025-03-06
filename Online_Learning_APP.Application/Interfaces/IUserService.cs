using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(string username, string email, string password, string roleName);
    }
}
