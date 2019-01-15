namespace ConfHall.Domain.Services
{
    using ConfHall.Domain.Entities;
    using Microsoft.AspNetCore.Identity;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class HashingService : IPasswordHasher<User>
    {
        #region Methods

        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.BCryptHelper.CheckPassword(password, correctHash);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(User user, string password)
        {
            return BCrypt.BCryptHelper.HashPassword(password, GetRandomSalt());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="hashedPassword"></param>
        /// <param name="providedPassword"></param>
        /// <returns></returns>
        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            try
            {
                if (BCrypt.BCryptHelper.CheckPassword(providedPassword, user.PasswordHash))
                {
                    return PasswordVerificationResult.Success;
                }
            }
            catch (Exception) { }

            return PasswordVerificationResult.Failed;
        }

        private static string GetRandomSalt()
        {
            return BCrypt.BCryptHelper.GenerateSalt(12);
        }

        #endregion Methods
    }
}
