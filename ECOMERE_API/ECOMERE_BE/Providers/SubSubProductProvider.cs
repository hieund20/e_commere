using ECOMERE_BE.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECOMERE_BE.Providers
{
    public class SubSubProductProvider : baseProvider
    {
        public async Task<List<SubSubProduct>> GetAllAsync()
        {
            try
            {
                var data = await db.SubSubProduct.OrderBy(c => c.CreatedAt).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                return new List<SubSubProduct>();
            }
        }

        public async Task<SubSubProduct> GetSubSubProductByIDAsync(string ID)
        {
            return await db.SubSubProduct.Where(c => c.Id == ID).FirstOrDefaultAsync();
        }

        public async Task<SubSubProduct> AddNewSubSubProductAsync(SubSubProduct newSubSubProduct)
        {
            try
            {
                newSubSubProduct.Id = Guid.NewGuid().ToString();
                newSubSubProduct.CreatedAt = DateTime.Now;
                db.SubSubProduct.Add(newSubSubProduct);
                await db.SaveChangesAsync();
                return newSubSubProduct;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SubSubProduct> UpdateSubSubProductAsync(string id, SubSubProduct modifiedSubSubProduct)
        {
            try
            {
                SubSubProduct existingSubSubProduct = await db.SubSubProduct.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingSubSubProduct != null)
                {
                    existingSubSubProduct.Name = modifiedSubSubProduct.Name;
                    existingSubSubProduct.UnitPrice = modifiedSubSubProduct.UnitPrice;
                    existingSubSubProduct.Quantity = modifiedSubSubProduct.Quantity;

                    existingSubSubProduct.ModifiedAt = DateTime.Now;
                    await db.SaveChangesAsync();
                    return existingSubSubProduct;
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

        public async Task<SubSubProduct> UpdateImageSubSubProductAsync(string id, IFormFile file)
        {
            try
            {
                SubSubProduct existingSubSubProduct = await db.SubSubProduct.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingSubSubProduct != null)
                {
                    existingSubSubProduct.ModifiedAt = DateTime.Now;
                    await db.SaveChangesAsync();
                    return existingSubSubProduct;
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

        public async Task<bool> DeleteSubSubProductAsync(string id)
        {
            try
            {
                SubSubProduct existingSubSubProduct = await db.SubSubProduct.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingSubSubProduct != null)
                {
                    db.SubSubProduct.Remove(existingSubSubProduct);
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
