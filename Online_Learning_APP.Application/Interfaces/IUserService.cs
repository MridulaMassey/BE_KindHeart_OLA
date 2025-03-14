using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_APP.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(string username, string email, string password, string roleName,string firstName,string lastName);
        Task<ApplicationUser> GetProfileAsync(string username);


    }
}
