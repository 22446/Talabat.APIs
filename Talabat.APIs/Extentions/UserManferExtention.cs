using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities;

namespace Talabat.APIs.Extentions
{
    public static class UserManferExtention
    {
        public static async  Task<AppUser?> FindUserWithAddress(this UserManager<AppUser> _UserManeger,ClaimsPrincipal claims)
        {
            var Email =  claims.FindFirstValue(ClaimTypes.Email);
            var user =await  _UserManeger.Users.Include(a => a.address).FirstOrDefaultAsync(a => a.Email == Email);
            return  user;
        }
    }
}
