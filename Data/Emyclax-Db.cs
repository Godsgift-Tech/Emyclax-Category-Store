using Emyclax_Category_Store.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Emyclax_Category_Store.Data
{
    public class Emyclax_Db: DbContext
    {
        public Emyclax_Db(DbContextOptions<Emyclax_Db> options) : base(options)
        {
        }
        public DbSet<EmyCatlog> EmyCatlogs { get; set; }    
    }
}
