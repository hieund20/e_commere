using ECOMERE_BE.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECOMERE_BE.Providers
{
    public class CategoryProvider : baseProvider
    {
        public async Task<List<Category>> GetAllAsync()
        {
            try
            {
                var data = await db.Category.OrderBy(c => c.CreatedAt).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                return new List<Category>();
            }
        }

        public async Task<Category> GetCategoryByIDAsync(string ID)
        {
            return await db.Category.Where(c => c.Id == ID).FirstOrDefaultAsync();
        }

        public async Task<Category> AddNewCategoryAsync(Category newCtegory)
        {
            try
            {
                newCtegory.Id = Guid.NewGuid().ToString();
                newCtegory.CreatedAt = DateTime.Now;  
                db.Category.Add(newCtegory);
                await db.SaveChangesAsync();
                return newCtegory;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Category> UpdateCategoryAsync(string id, Category modifiedCategory)
        {
            try
            {
                Category existingCategory = await db.Category.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingCategory != null)
                {
                    existingCategory.Name = modifiedCategory.Name;
                    existingCategory.ModifiedAt = DateTime.Now;
                    await db.SaveChangesAsync();
                    return existingCategory;
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

        public async Task<bool> DeleteCategoryAsync(string id)
        {
            try
            {
                Category existingCategory = await db.Category.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingCategory != null)
                {
                    db.Category.Remove(existingCategory);
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
