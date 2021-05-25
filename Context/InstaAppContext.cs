using InstaTicket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Context
{
    public class InstaAppContext : IdentityDbContext<IdentityUser>
    {
        public InstaAppContext(DbContextOptions<InstaAppContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Concert> Events { get; set; }
        public DbSet<Festival> Festivals { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

    }
}
