using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pasticceria.Models.Entities;

namespace Pasticceria.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Dolce> Dolci { get; set; }
        public DbSet<Ingrediente> Ingredienti { get; set; }
        public DbSet<DolceIngrediente> DolceIngrediente { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }
    }
}
