using ECOMERE_BE.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECOMERE_BE.Providers
{
    public class UserProvider : baseProvider
    {
        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                var data = await db.User.OrderBy(c => c.CreatedAt).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }

        public async Task<User> GetUserByIDAsync(string ID)
        {
            return await db.User.Where(c => c.Id == ID).FirstOrDefaultAsync();
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8)
             );
            return hashed;
        }

        public async Task<User> AddNewUserAsync(User newUser)
        {
            try
            {
                newUser.Id = Guid.NewGuid().ToString();
                newUser.CreatedAt = DateTime.Now;
                newUser.Password = HashPassword(newUser.Password);
                db.User.Add(newUser);
                await db.SaveChangesAsync();
                return newUser;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> UpdateUserAsync(string id, User modifiedUser)
        {
            try
            {
                User existingUser = await db.User.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingUser != null)
                {
                    existingUser.FirstName = existingUser.FirstName;
                    existingUser.LastName = existingUser.LastName;
                    existingUser.Email = existingUser.Email;
                    existingUser.Phone = existingUser.Phone;
                    existingUser.ModifiedAt = DateTime.Now;
                    await db.SaveChangesAsync();
                    return existingUser;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            try
            {
                User existingUser = await db.User.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingUser != null)
                {
                    db.User.Remove(existingUser);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
