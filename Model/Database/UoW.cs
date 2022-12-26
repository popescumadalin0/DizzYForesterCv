using Microsoft.EntityFrameworkCore;
using Model.Model;

namespace Model.Database
{
    public class UoW : DbContext, IUoW
    {
        public UoW(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
