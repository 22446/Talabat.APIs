using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.Core.Entities;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
   
    public class accountsController : ApiEntityBase
    {
        private readonly IMapper mapper;
        private readonly ITokenServices tokenServices;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public accountsController(IMapper mapper,ITokenServices tokenServices,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this.mapper = mapper;
            this.tokenServices = tokenServices;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExsist(model.Email).Result.Value)
                return BadRequest(new ApiRespones(401));
            var User = new AppUser()
            {
                DisplayName=model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber
            };
            var Result= await userManager.CreateAsync(User, model.Password);
            if (Result.Succeeded)
            {
                var returnedUser = new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token =await tokenServices.GenerateToken(User)
                };
                return Ok(returnedUser);
            }
            else
                return BadRequest(new ApiRespones(400));

        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>>Login(LoginDto Model)
        {
            var User = await userManager.FindByEmailAsync(Model.Email);
            if(User is not null)
            {
                var Result = await signInManager.CheckPasswordSignInAsync(User,Model.Password,false);
                if(Result.Succeeded)
                {
                    var returnedUser = new UserDto()
                    {
                        DisplayName = User.DisplayName,
                        Email = User.Email,
                        Token = await tokenServices.GenerateToken(User)
                    };
                    return Ok(returnedUser);
                }
                else
                {
                    return Unauthorized(new ApiRespones(401));

                }

            }
            else
                return Unauthorized(new ApiRespones(401));

        }
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(Email);
         

            var ReturnedUser = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenServices.GenerateToken(user)
            };
            return Ok(ReturnedUser);
         
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetAddress()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(a=>a.address).FirstOrDefaultAsync(a=>a.Email==Email);
            var MappedAddress = mapper.Map<Address, AddressDto>(user.address);
            return Ok(MappedAddress);
        }
        [Authorize]
        [HttpPut("Address")]
        public  async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto addressDto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(a => a.address).FirstOrDefaultAsync(a => a.Email == Email);
            var MappedAddress = mapper.Map<AddressDto, Address>(addressDto);
            MappedAddress.Id= user.address.Id;
            user.address= MappedAddress;
            var result =await userManager.UpdateAsync(user);
            return Ok(addressDto);


        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExsist(string email)
        {
            var User = await userManager.FindByEmailAsync(email);
            if (User is not null) return true;
            else
               return false;
            

                   
        }
    }
}
