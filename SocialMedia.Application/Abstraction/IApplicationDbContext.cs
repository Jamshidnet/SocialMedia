using Microsoft.EntityFrameworkCore;
using SocialMedia.Domein.Entities;

namespace SocialMedia.Application.Abstraction
{
    public  interface IApplicationDbContext
    {
        DbSet<User>  Users { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Post> Posts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
