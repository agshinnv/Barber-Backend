using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<About> Abouts { get; set; }
        public DbSet<AboutImage> AboutImages { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<BarberPricing> BarberPricings { get; set; }
        public DbSet<BarberService> BarberServices { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<Colleague> Colleagues { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ComplaintSuggest> ComplaintSuggests { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PricingCategory> PricingCategories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceImage> ServiceImages { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<SubService> SubServices { get; set; }
        public DbSet<WorkTime> WorkTimes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
