using ECOMERE_BE.Models;
using ECOMERE_BE.Configs;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECOMERE_BE.Providers
{
    public class SubProductProvider : baseProvider
    {
        public async Task<List<SubProduct>> GetAllAsync()
        {
            try
            {
                var data = await db.SubProduct.OrderBy(c => c.CreatedAt).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                return new List<SubProduct>();
            }
        }

        public async Task<SubProduct> GetSubProductByIDAsync(string ID)
        {
            return await db.SubProduct.Where(c => c.Id == ID).FirstOrDefaultAsync();
        }

        public async Task<SubProduct> AddNewSubProductAsync(SubProduct newSubProduct)
        {
            try
            {
                newSubProduct.Id = Guid.NewGuid().ToString();
                newSubProduct.CreatedAt = DateTime.Now;
                db.SubProduct.Add(newSubProduct);
                await db.SaveChangesAsync();
                return newSubProduct;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SubProduct> UpdateSubProductAsync(string id, SubProduct modifiedSubProduct)
        {
            try
            {
                SubProduct existingSubProduct = await db.SubProduct.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingSubProduct != null)
                {
                    existingSubProduct.Name = modifiedSubProduct.Name;
                    existingSubProduct.ProductId = modifiedSubProduct.ProductId;

                    existingSubProduct.ModifiedAt = DateTime.Now;
                    await db.SaveChangesAsync();
                    return existingSubProduct;
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
                SubProduct existingSubSubProduct = await db.SubProduct.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingSubSubProduct != null)
                {
                    db.SubProduct.Remove(existingSubSubProduct);
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
