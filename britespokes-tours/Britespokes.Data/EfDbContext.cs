using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using Britespokes.Core;
using Britespokes.Core.Domain;

namespace Britespokes.Data
{
  public class EfDbContext : DbContext
  {
    public EfDbContext()
    {
    }

    public EfDbContext(string databaseName)
      : base(databaseName)
    {
    }

    public DbSet<Organization> Organizations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<TourType> TourTypes { get; set; }
    public DbSet<Departure> Departures { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatus { get; set; }
    public DbSet<AvailabilityStatus> AvailabilityStatus { get; set; }
    public DbSet<Adjustment> Adjustments { get; set; }
    public DbSet<DiscountAdjustment> DiscountAdjustments { get; set; }
    public DbSet<DiscountCode> DiscountCodes { get; set; }
    public DbSet<TourMeta> TourMeta { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<SEOToolStaticPage> SEOToolStaticPages { get; set; }
    public DbSet<GiftCard> GiftCard { get; set; }
    public DbSet<GiftOrder> GiftOrders { get; set; }
    public DbSet<GiftOrderDetail> GiftOrderDetails { get; set; }
    public DbSet<PerspectivePost> PerspectivePosts { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
        .Where(type => !String.IsNullOrEmpty(type.Namespace))
        .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

      foreach (var type in typesToRegister)
      {
        dynamic configurationInstance = Activator.CreateInstance(type);
        modelBuilder.Configurations.Add(configurationInstance);
      }

      base.OnModelCreating(modelBuilder);
    }

    public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
    {
      return base.Set<TEntity>();
    }

    public int Save()
    {
      return base.SaveChanges();
    }
  }
}
