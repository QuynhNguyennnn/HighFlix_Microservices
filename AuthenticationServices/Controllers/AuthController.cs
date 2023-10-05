using APIS.DTOs.RequestDto;
using APIS.DTOs.ResponseDto;
using AuthenticationServices.Models;
using AuthenticationServices.Services.RoleServices;
using AuthenticationServices.Services.UserServices;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIS.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService userService = new UserService();
        private IRoleService roleService = new RoleService();
        public AuthResponse authResponse = new AuthResponse();
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public ActionResult<AuthResponse> Register(RegisterDto registerDto)
        {
            var usernameValidate = userService.GetUserByUsername(registerDto.Username);
            if (usernameValidate != null)
            {
                authResponse.Message = "Username already exists.";
                return BadRequest(authResponse);
            }
            else
            {
                if (registerDto.Password != null && registerDto.Password == registerDto.ConfirmPassword)
                {
                    var userMap = _mapper.Map<User>(registerDto);
                    var user = userService.Register(userMap);
                    var token = GeneratAccessToken(user.Username);
                    authResponse.AccessToken = token;
                    authResponse.Message = "Register successful";
                    return Ok(authResponse);
                }
                else
                {
                    authResponse.Message = "Password and Password Confirm not matched.";
                    return BadRequest(authResponse);
                }
            }
        }

        [HttpPost("login")]
        public ActionResult<AuthResponse> Login(LoginDto loginDto)
        {
            var user = userService.GetUserByUsername(loginDto.Username);
            if (user == null)
            {
                authResponse.Message = "Username not found.";
                return NotFound(authResponse);
            }
            else
            {
                var userValidate = userService.Login(loginDto.Username, loginDto.Password);
                if (userValidate == false)
                {
                    authResponse.Message = "Wrong password";
                    return BadRequest(authResponse);
                }
                else
                {
                    var token = GeneratAccessToken(loginDto.Username);
                    authResponse.AccessToken = token;
                    var refreshToken = GenerateRefreshToken(loginDto.Username);
                    authResponse.RefreshToken = refreshToken;
                    authResponse.Message = "Login successful";
                    return Ok(authResponse);
                }
            }
        }
        [NonAction]
        private string GeneratAccessToken(string username)
        {
            var user = userService.GetUserByUsername(username);
            var role = roleService.GetRoleById(user.RoleId);
            List<Claim> claims = new List<Claim>
            {
                new Claim("role", role.RoleName),
                new(JwtRegisteredClaimNames.Name, username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSetting:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        [HttpPost("refreshToken")]
        public ActionResult<AuthResponse> RefreshToken(string refreshToken)
        {
            var tokenReader = GetTokenInfor(refreshToken);
            if (userService.GetUserByUsername(tokenReader.Username) != null && tokenReader.ExpireDate > DateTime.Now)
            {

                authResponse.AccessToken = GeneratAccessToken(tokenReader.Username);
                authResponse.RefreshToken = GenerateRefreshToken(tokenReader.Username);
                authResponse.Message = "Token is recreated.";
            }
            else
            {
                authResponse.Message = "Invalid Refresh token";
                return Unauthorized(authResponse);
            }
            return Ok(authResponse);
        }

        [NonAction]
        private string GenerateRefreshToken(string username)
        {
            var user = userService.GetUserByUsername(username);
            var role = roleService.GetRoleById(user.RoleId);
            List<Claim> claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSetting:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        [NonAction]
        public TokenReaderResponse GetTokenInfor(string token)
        {
            TokenReaderResponse reader = new TokenReaderResponse();
            var jwtToken = new JwtSecurityToken(token);
            var nameClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "name");
            //var expiredClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "exp");
            var expirationDate = jwtToken.ValidTo;
            //var roleClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role");
            if (nameClaim != null)
            {
                reader.Username = nameClaim.Value;
                reader.ExpireDate = expirationDate.AddHours(7);
                var userInfor = userService.GetUserByUsername(reader.Username);
                var userRole = roleService.GetRoleById(userInfor.RoleId);
                reader.Role = userRole.RoleName;
            }
            return reader;
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers() => userService.GetUsers();

        [HttpGet("id")]
        public ActionResult<User> GetUserById(int id)
        {
            return userService.GetUserById(id);
        }

    }
}
