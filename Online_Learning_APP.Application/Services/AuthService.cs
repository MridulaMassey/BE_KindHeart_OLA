using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
//using Application.DTOs;
//using Application.Interfaces;
using Online_Learning_App.Domain.Entities;
//using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUserRepository userRepository, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AuthenticateUserAsync(LoginDTO loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDto.UserName);
            if (user == null) return false;

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.RememberMe, false);
            return result.Succeeded;
        }
    }
}