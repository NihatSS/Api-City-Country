﻿using ApiPractice.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiPractice.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<City> Cities{ get; set; }
        public DbSet<Country> Countries { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
    }
}
