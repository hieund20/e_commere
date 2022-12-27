using ECOMERE_BE.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECOMERE_BE.Providers
{
    public class ProductProvider : baseProvider
    {
        public async Task<List<Product>> GetAllAsync()
        {
            try
            {
                var data = await db.Product.OrderBy(c => c.CreatedAt).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                return new List<Product>();
            }
        }

        public async Task<Product> GetProductByIDAsync(string ID)
        {
            return await db.Product.Include(c => c.Comment).Where(c => c.Id == ID).FirstOrDefaultAsync();
        }

        public async Task<Product> AddNewProductAsync(Product newProduct)
        {
            try
            {
                newProduct.Id = Guid.NewGuid().ToString();
                newProduct.CreatedAt = DateTime.Now;
                db.Product.Add(newProduct);
                await db.SaveChangesAsync();
                return newProduct;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Product> UpdateProductAsync(string id, Product modifiedProduct)
        {
            try
            {
                Product existingProduct = await db.Product.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingProduct != null)
                {
                    existingProduct.Name = modifiedProduct.Name;
                    existingProduct.UnitPrice = modifiedProduct.UnitPrice;
                    existingProduct.Quantity = modifiedProduct.Quantity;
                    existingProduct.IsHidden = modifiedProduct.IsHidden;
                    existingProduct.ModifiedAt = DateTime.Now;
                    await db.SaveChangesAsync();
                    return existingProduct;
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

        public async Task<bool> DeleteProductAsync(string id)
        {
            try
            {
                Product existingProduct = await db.Product.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
                if (existingProduct != null)
                {
                    db.Product.Remove(existingProduct);
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
