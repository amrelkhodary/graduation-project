using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartJobAssistant.APIS.DTOS;
using SmartJobAssistant.APIS.Errors;
using SmartJobAssistant.Core.Models.Identity;
using SmartJobAssistant.Core.Services;
using SmartJobAssistant.Services;
using System.Security.Claims;


namespace SmartJobAssistant.APIS.Controllers
{
	public class AccountController:BaseApiController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;

		public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager,ITokenService tokenService)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
		}

		[HttpPost("Login")]
		public async Task<ActionResult<UserDTO>> Login(LoginDTO model )
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is null) return Unauthorized(new ApiResponse(401));
			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password,false);
			if(!result.Succeeded) return Unauthorized(new ApiResponse(401));
			return Ok( new UserDTO()
			{
				DisplayName=user.DisplayNamr,
				Email=user.Email,
				Token = await _tokenService.CreateTokenAsync(user, _userManager)

			});
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
		{
			var user = new AppUser()
			{
				DisplayNamr = model.DisplayName,
				Email = model.Email,
				UserName = model.Email.Split('@')[0],
				PhoneNumber = model.PhoneNumber,
			};
			var result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded) return BadRequest(new ApiResponse(400));
			return Ok(new UserDTO()
			{
				DisplayName = user.DisplayNamr,
				Email = user.Email,
				Token= await _tokenService.CreateTokenAsync(user,_userManager)

			});

		}
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet]
		public async Task<ActionResult<UserDTO>> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(email); // no valdation as the user is authorize
			return Ok(new UserDTO()
			{
				Email = user.Email,
				DisplayName = user.DisplayNamr,
				Token = await _tokenService.CreateTokenAsync(user, _userManager)

			});

		}
	}
}
