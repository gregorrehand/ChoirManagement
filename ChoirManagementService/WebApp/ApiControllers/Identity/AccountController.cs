using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Domain.App.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApp.ApiControllers.Identity
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IAppBLL _bll;

        /// <summary>
        /// AccountController
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="userManager"></param>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="bll"></param>
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
            ILogger<AccountController> logger, IConfiguration configuration, IAppBLL bll)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
            _bll = bll;
        }

        /// <summary>
        /// Login with email/password
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>JwtResponse</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.JwtResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Message), StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.v1.JwtResponse>> Login([FromBody] PublicApi.DTO.v1.Login dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            // TODO: wait a random time here to fool timing attacks
            if (appUser == null)
            {
                _logger.LogWarning("WebApi login failed. User {User} not found", dto.Email);
                return NotFound(new PublicApi.DTO.v1.Message("User/Password problem!"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, dto.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                var jwt = Extensions.Base.IdentityExtensions.GenerateJwt(
                    claimsPrincipal.Claims,
                    _configuration["JWT:Key"],                    
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Issuer"],
                    DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                    );
                _logger.LogInformation("WebApi login. User {User}", dto.Email);
                return Ok(new PublicApi.DTO.v1.JwtResponse()
                {
                    Token = jwt,
                    Firstname = appUser.FirstName,
                    Lastname = appUser.LastName,
                });
            }
            
            _logger.LogWarning("WebApi login failed. User {User} - bad password", dto.Email);
            return NotFound(new PublicApi.DTO.v1.Message("User/Password problem!"));
        }

        
        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>JwtResponse</returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] PublicApi.DTO.v1.Register dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser != null)
            {
                _logger.LogWarning(" User {User} already registered", dto.Email);
                return BadRequest(new PublicApi.DTO.v1.Message("User already registered"));
            }

            appUser = new Domain.App.Identity.AppUser()
            {
                Email = dto.Email,
                UserName = dto.Email,
                FirstName = dto.Firstname,
                LastName = dto.Lastname,
                VoiceGroupId = dto.VoiceGroupId
            };
            var result = await _userManager.CreateAsync(appUser, dto.Password);
            
            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} created a new account with password", appUser.Email);
                
                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {                
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                    var jwt = Extensions.Base.IdentityExtensions.GenerateJwt(
                        claimsPrincipal.Claims,
                        _configuration["JWT:Key"],                    
                        _configuration["JWT:Issuer"],
                        _configuration["JWT:Issuer"],
                        DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                    );
                    _logger.LogInformation("WebApi login. User {User}", dto.Email);
                    await _userManager.AddToRoleAsync(user, "user");
                    return Ok(new PublicApi.DTO.v1.JwtResponse()
                    {
                        Token = jwt,
                        Firstname = appUser.FirstName,
                        Lastname = appUser.LastName,
                    });
                    
                }
                else
                {
                    _logger.LogInformation("User {Email} not found after creation", appUser.Email);
                    return BadRequest(new PublicApi.DTO.v1.Message("User not found after creation!"));
                }
            }
            
            var errors = result.Errors.Select(error => error.Description).ToList();
            return BadRequest(new PublicApi.DTO.v1.Message() {Messages = errors});
        }
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "admin,manager")]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.AppUser>>> GetUsers()
        {
            var users = await _bll.AppUsers.GetAllAsync();

            var dtoUsers = new List<PublicApi.DTO.v1.AppUser>();
            foreach (var user in users)
            {
                dtoUsers.Add(new PublicApi.DTO.v1.AppUser
                {
                    Id = user.Id,
                    FirstLastName = user.FirstLastName,
                    VoiceGroupName = user.VoiceGroup!.Name
                });
            }

            return Ok(dtoUsers);
        }

    }
}