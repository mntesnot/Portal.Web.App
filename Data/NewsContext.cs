using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portal.Web.App.Models;

namespace Portal.Web.App.Data
{
    public class NewsContext : DbContext
    {
        public NewsContext (DbContextOptions<NewsContext> options)
            : base(options)
        {
        }

        public DbSet<News> News { get; set; } = default!;
    }
}
