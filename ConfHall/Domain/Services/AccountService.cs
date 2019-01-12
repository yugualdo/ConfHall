namespace ConfHall.Domain.Services
{
    using ConfHall.Domain.Entities;
    using ConfHall.Model;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using ConfHall.Models;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Class AccountService all login ModelBuinisess.
    /// </summary>
    public class AccountService : IAccountService
    {
        #region Constructor
        private readonly IUserService userService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IConfiguration _config;

        public AccountService(IConfiguration _config, IUserService userService, SignInManager<User> signInManager, UserManager<User> _userManager, IOptions<JwtIssuerOptions> jwtOptions) : base()
        {
            this.userService = userService;
            this._signInManager = signInManager;
            this._userManager = _userManager;
            this._jwtOptions = jwtOptions.Value;
            this._config = _config;
        }

        #endregion

        #region Interface

        /// <summary>
        /// Validate login credencials
        /// </summary>
        /// <param name="model"></param>
        /// <returns>if the user exist return true. If not exist return false</returns>
        public bool LoginWithoutIdentity(UserModel model)
        {
            List<UserModel> users = this.userService.Get().ToList();
            UserModel user = users.Find(c => c.UserName.ToLower().Trim().Equals(model.UserName.ToLower().Trim()));

            if (user != null)
            {
                return HashingService.ValidatePassword(model.PasswordHash, user.PasswordHash); ;
            }
            return false;
        }

        /// <summary>
        /// Validate login credencials
        /// </summary>
        /// <param name="model"></param>
        /// <returns>if the user exist return true. If not exist return false</returns>
        public async Task<AccountModel> PasswordSignInAsync(AccountModel model, string remoteIpAddreess)
        {
            User user = await this._userManager.FindByNameAsync(model.UserName);
            if (user != null && !this.IsSignedIn(user))
            {
                var passwordSignInResult = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);
                if (passwordSignInResult.Succeeded)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var now = DateTime.UtcNow;
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
                        new Claim(JwtRegisteredClaimNames.Aud,"documents"),
                        new Claim(JwtRegisteredClaimNames.Aud,"image"),
                        new Claim(JwtRegisteredClaimNames.Aud,"post"),
                        new Claim(JwtRegisteredClaimNames.Aud,"stats"),
                        new Claim(JwtRegisteredClaimNames.Aud,"client")
                    };

                    var tokeOptions = new JwtSecurityToken(
                        issuer: _config["Tokens:Issuer"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(100),
                        signingCredentials: signinCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                    var userModel = Mapper.Map<User, UserModel>(user);
                    userModel.PasswordHash = String.Empty;
                    AccountModel accountModel = new AccountModel
                    {
                        UserName = userModel.UserName,
                        Email = userModel.Email,
                        Token = tokenString,
                        Expiration = DateTime.Now.AddMinutes(100).ToString()
                    };
                    return accountModel;
                }
            }
            return null;
        }

        /// <summary>
        /// Validate login credencials
        /// </summary>
        /// <param name="model"></param>
        /// <returns>if the user exist return true. If not exist return false</returns>
        public async Task<bool> SignInAsync(AccountModel model)
        {
            List<UserModel> users = this.userService.Get().ToList();
            UserModel user = users.Find(c => c.UserName.ToLower().Trim().Equals(model.Email.ToLower().Trim()));

            if (user != null)
            {
                if (HashingService.ValidatePassword(model.Password, user.PasswordHash))
                {
                    var userEntity = await _userManager.FindByEmailAsync(model.Email);
                    if (!this.IsSignedIn(userEntity))
                    {
                        await this._signInManager.SignInAsync(userEntity, model.RememberMe);
                        return true;
                    }
                }
            }
            return false;
        }

        public void Logout()
        {
            UserModel CurrentUser = this.userService.GetCurrentUser();
            User user = Mapper.Map<UserModel, User>(CurrentUser);
            this.RemoveCurrentUserClaim(user);
            this._signInManager.SignOutAsync();
        }

        /// <summary>
        /// Add user to a role if the user exists, otherwise, create the user and adds him to the role.
        /// </summary>
        /// <param name="User">User entity</param>
        /// <param name="roleName">Role Name</param>
        public async Task<bool> AddUserToRoleAsync(User User, string roleName)
        {
            IdentityResult newUserRole = await this._userManager.AddToRoleAsync(User, roleName);
            return newUserRole.Succeeded;
        }

        /// <summary>
        /// Add user to a role if the user exists, otherwise, create the user and adds him to the role.
        /// </summary>
        /// <param name="User">User entity</param>
        /// <param name="roleName">Role Name</param>
        public async Task<bool> RemoveUserFromRoleAsync(User User, string roleName)
        {
            IdentityResult newUserRole = await this._userManager.RemoveFromRoleAsync(User, roleName);
            return newUserRole.Succeeded;
        }


        #endregion

        #region Functions

        private AccountModel GetSession(User user, string remoteIpAddreess)
        {
            JwtSecurityToken securityToken = this.GetSecurityToken(user, remoteIpAddreess);
            var userModel = Mapper.Map<User, UserModel>(user);
            userModel.PasswordHash = String.Empty;

            AccountModel accountModel = new AccountModel
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = securityToken.ValidTo.Date.ToString()
            };
            return accountModel;
        }

        private async Task SetCurrentUserClaim(User User)
        {
            await this._userManager.AddClaimAsync(User, new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()));
        }

        private void RemoveCurrentUserClaim(User User)
        {
            try
            {
                var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, User.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
                        new Claim(JwtRegisteredClaimNames.Aud,"documents"),
                        new Claim(JwtRegisteredClaimNames.Aud,"image"),
                        new Claim(JwtRegisteredClaimNames.Aud,"post"),
                        new Claim(JwtRegisteredClaimNames.Aud,"stats"),
                        new Claim(JwtRegisteredClaimNames.Aud,"client")
                    };
                foreach(var claim in claims)
                {
                    this._userManager.RemoveClaimAsync(User, claim);
                }
            }
            catch (Exception) { }
        }

        private JwtSecurityToken GetSecurityToken(User user, string remoteIpAddreess)
        {
            var claims = this.GetUserClaimsAsync(user);

            // Create the JWT security token and encode it.
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: this._jwtOptions.Issuer,
                //audience: remoteIpAddreess,
                audience: this._jwtOptions.Audience,
                claims: claims,
                notBefore: this._jwtOptions.NotBefore,
                expires: this._jwtOptions.Expiration,
                signingCredentials: this._jwtOptions.SigningCredentials);

            return jwt;
        }

        private IEnumerable<Claim> GetUserClaimsAsync(User user)
        {
            var userClaims = this._userManager.GetClaimsAsync(user);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, this._jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            }.Union(userClaims.Result);
            return claims;
        }

        private bool IsSignedIn(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.UserData, user.Id.ToString())
            };
            var userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            return this._signInManager.IsSignedIn(principal);
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        #endregion
    }
}
