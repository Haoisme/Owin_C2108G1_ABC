using Microsoft.EntityFrameworkCore;
using Owin_C2108G1_ABC.Model;
using System.Collections.Generic;
using System.Security.Claims;

namespace Owin_C2108G1_ABC.Data
{
    public class DbConnection: DbContext 
    {
        public DbConnection(DbContextOptions options) : base(options) { }
        public DbSet<Account> Account { get; set; }
        
    }
}
