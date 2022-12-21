using ECOMERE_BE.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECOMERE_BE.Providers
{
    public class UserRoleProvider : baseProvider
    {
        public async Task<List<UserRole>> GetAllAsync()
        {
            try
            {
                var data = await db.UserRole.OrderBy(c => c.CreatedAt).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                return new List<UserRole>();
            }
        }

        public async Task<UserRole> GetUserRoleByIDAsync(string ID)
        {
            return await db.UserRole.Where(c => c.Id == ID).FirstOrDefaultAsync();
        }


        public async Task<UserRole> AddNewUserRoleAsync(UserRole newUserRole)
        {
            try
            {
                newUserRole.Id = Guid.NewGuid().ToString();
                newUserRole.CreatedAt = DateTime.Now;
                db.UserRole.Add(newUserRole);
                await db.SaveChangesAsync();
                return newUserRole;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UserRole> UpdateUserRoleAsync(string id, UserRole modifiedUserRole)
        {
            try
            {
                UserRole existingUserRole = await db.UserRole.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingUserRole != null)
                {
                    existingUserRole.UserRole1 = modifiedUserRole.UserRole1;
                    await db.SaveChangesAsync();
                    return existingUserRole;
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

        public async Task<bool> DeleteUserRoleAsync(string id)
        {
            try
            {
                UserRole existingUserRole = await db.UserRole.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingUserRole != null)
                {
                    db.UserRole.Remove(existingUserRole);
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
