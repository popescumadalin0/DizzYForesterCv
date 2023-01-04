using Microsoft.EntityFrameworkCore;
using Model.Model;

namespace Model.Database
{
    public interface IUoW
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Experience> Experiences { get; set; }

        public Task<int> SaveChangesAsync();
    }
}
