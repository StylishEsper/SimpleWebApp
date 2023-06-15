using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using NETD3202_Lab5_V3.Models;

namespace NETD3202_Lab5_V3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<VideoGame> VideoGames { get; set; }
        public DbSet<MoreDetail> MoreDetails { get; set; }
    }
}
