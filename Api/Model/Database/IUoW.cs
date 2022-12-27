using Microsoft.EntityFrameworkCore;
using Model.Model;

namespace Model.Database
{
    public interface IUoW
    {
        public DbSet<User> Users { get; set; }

        public Task<int> SaveChangesAsync();
    }
}
