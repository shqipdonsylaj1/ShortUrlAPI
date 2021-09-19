using Microsoft.EntityFrameworkCore;
using ShortUrlAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrlAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        #region Properties
        public DbSet<LongUrl> LongUrls { get; set; }
        #endregion

        #region Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        #endregion
    }
}
