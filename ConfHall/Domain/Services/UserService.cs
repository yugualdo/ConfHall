namespace ConfHall.Domain.Services
{
    using ConfHall.Domain.Entities;
    using ConfHall.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using ConfHall.Domain.Repositories;

    /// <summary>
    /// UserService class.
    /// </summary>
    public class UserService : IUserService
    {
        #region Fields

        IUserRepository UserRepository;
        IHttpContextAccessor httpContextAccessor;
        IPasswordHasher<User> hashingService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="UserRepository">The User Repository.</param>
        /// <param name="hashingService"></param>
        /// <param name="httpContextAccessor"></param>
        /// 
        public UserService(

            IUserRepository UserRepository,
            IHttpContextAccessor httpContextAccessor,
            IPasswordHasher<User> hashingService
            )
        {
            this.UserRepository = UserRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.hashingService = hashingService;


        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Sets a Campaign by UserModel.
        /// </summary>
        /// <param name="UserModel">The UserModel.</param>
        /// <returns>The id of the User</returns>
        public Guid Add(UserModel UserModel)
        {
            var user = Mapper.Map<User>(UserModel);

            this.FillIdentityParameters(ref user);

            string errors = ValidateUser(user);

            if (errors != null)
                throw new ValidationException(errors);

            user.PasswordHash = this.hashingService.HashPassword(user, user.PasswordHash);
            this.UserRepository.Insert(user);

            return user.Id;
        }

        /// <summary>
        /// Gets all countries.
        /// </summary>
        /// <param></param>
        /// <returns>IEnumerable User</returns>
        public IEnumerable<UserModel> Get()
        {
            IEnumerable<User> User = this.UserRepository.GetAll();
            return User.Select(c => Mapper.Map<UserModel>(c));
        }

        /// <summary>
        /// Gets a User by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>User model</returns>
        public UserModel Get(Guid id)
        {
            var User = this.UserRepository.Get(id);
            return Mapper.Map<UserModel>(User);
        }



        /// <summary>
        /// Update a User by UserModel.
        /// </summary>
        /// <param name="UserModel">The UserModel.</param>
        /// <returns></returns>
        public void Update(UserModel UserModel)
        {
            var user = Mapper.Map<User>(UserModel);

            this.FillIdentityParameters(ref user);

            string errors = ValidateUser(user);

            if (errors != null)
                throw new ValidationException(errors);
            if (!VerifyPasswordHash(user, UserModel))
            {
                user.PasswordHash = this.hashingService.HashPassword(user, UserModel.PasswordHash);
            }
            this.UserRepository.Update(user);
        }

        public UserModel GetCurrentUser()
        {
            UserModel CurrentUSer = null;
            IEnumerable<Claim> CurrentUserClaims = this.httpContextAccessor.HttpContext.User.Claims;
            IEnumerable<Claim> CurrentUserNameClaims = CurrentUserClaims.Where(c => c.Type == ClaimTypes.NameIdentifier);
            foreach (Claim claim in CurrentUserNameClaims)
            {
                if (CurrentUSer == null)
                {
                    try
                    {
                        Guid CurrentUserId = Guid.Parse(claim.Value);
                        CurrentUSer = this.Get(CurrentUserId);
                    }
                    catch (Exception) { }
                }
            }
            return CurrentUSer;
        }

        private bool VerifyPasswordHash(User user, UserModel userModel)
        {
            var passwordhash = this.hashingService.HashPassword(user, userModel.PasswordHash);

            if (passwordhash.Equals(user.PasswordHash))
                return true;
            return false;
        }
        private string ValidateUser(User User)
        {
            List<string> errors = new List<string>();

            if (User == null)
            {
                errors.Add("The User does not exist.");
            }
            if (errors.Any())
                return errors.Aggregate((c, n) => c + "*" + n);

            return null;
        }
        #endregion Methods

        #region Private Methods

        private void FillIdentityParameters(ref User user)
        {
            user.NormalizedEmail = user.Email.ToUpper();
            user.EmailConfirmed = true;
            user.NormalizedUserName = user.UserName.ToUpper();
            user.PhoneNumberConfirmed = true;
            user.ConcurrencyStamp = Guid.NewGuid().ToString("D");
            user.SecurityStamp = Guid.NewGuid().ToString("D");
        }

        #endregion
    }
}
