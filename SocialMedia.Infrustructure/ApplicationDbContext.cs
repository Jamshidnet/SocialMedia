using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Abstraction;
using SocialMedia.Domein.Entities;
using SocialMedia.Infrustructure.Interceptor;
using System.Reflection;

namespace SocialMedia.Infrustructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        private readonly InterceptorClass _interceptor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            InterceptorClass interceptor)
            : base(options)
        {
            _interceptor = interceptor;
        }


        public DbSet<User> Users { get  ; set  ; }
        public DbSet<Comment> Comments { get  ; set  ; }
        public DbSet<Post> Posts { get  ; set  ; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entity.Name).Property(typeof(DateTimeOffset), "CreatedDate")
                    .HasColumnType("timestamptz");

                modelBuilder.Entity(entity.Name).Property(typeof(DateTimeOffset), "UpdatedDate")
                    .HasColumnType("timestamptz");
            }

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_interceptor);
        }
    }
}
