using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TwitterClone.Models;

namespace TwitterClone.DataContext
{
    public class TwitterCloneDBContext : DbContext
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<Following> Following { get; set; }
        public DbSet<Tweet> Tweet { get; set; }
        public TwitterCloneDBContext(DbContextOptions<TwitterCloneDBContext> options) : base(options)
        {

        }
    }
}
