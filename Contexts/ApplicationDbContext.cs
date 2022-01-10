using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewspaperAdvertisementManagementSystem.Models;

namespace NewspaperAdvertisementManagementSystem.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<Client>
    {


        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Advertisement> Advertisements { get; set; }

        public virtual DbSet<Admin> Admins { get; set; }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }

        public virtual DbSet<Subscriber> Subscribers { get; set; }

    }
}