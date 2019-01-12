namespace ConfHall.Domain.Services
{
    using ConfHall.Domain.Entities;
    using Microsoft.AspNetCore.Identity;
    using System;

    public class HashingService : IPasswordHasher<User>
    {
        #region Methods

        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.BCryptHelper.CheckPassword(password, correctHash);
        }

        public string HashPassword(User user, string password)
        {
            return BCrypt.BCryptHelper.HashPassword(password, GetRandomSalt());
        }

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
