using ECOMERE_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace ECOMERE_BE.Providers
{
    public class baseProvider
    {
        public ecommereContext db = null;

        public async Task<bool> SaveDataAsync()
        {
            await db.SaveChangesAsync();
            return true;

        }

        public baseProvider()
        {
            DbContextOptionsBuilder<ecommereContext> optionsBuilder = new DbContextOptionsBuilder<ecommereContext>();
            optionsBuilder.UseSqlServer("Server=NguyenDucHieu;Database=ecommere;Trusted_Connection=True;");
            db = new ecommereContext(optionsBuilder.Options);
        }
    }
}
