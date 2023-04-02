using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Areas.Identity.Data;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class WebApplication1Context : IdentityDbContext<WebApplication1User>
{
    public WebApplication1Context(DbContextOptions<WebApplication1Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new AppUserEntityConfiguration());
    }
    public DbSet<Faktura> Faktura { get; set; }
    public DbSet<Stavka> Stavka { get; set; }
    public DbSet<Prodano> Prodano { get; set; }
}

internal class AppUserEntityConfiguration : IEntityTypeConfiguration<WebApplication1User>
{
    public void Configure(EntityTypeBuilder<WebApplication1User> builder)
    {

    }

}

