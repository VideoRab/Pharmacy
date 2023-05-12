using DAL.Abstraction;

namespace DAL.Data;

public class DbInitializer : IDbInitializer
{
    private readonly ApplicationContext _context;
    public DbInitializer(ApplicationContext context)
    {
        _context = context;
    }

    public void DbInitialize()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _context.Roles?.AddRange(FakeData.Roles);
        _context.SaveChanges();

        _context.Users?.AddRange(FakeData.Users);
        _context.SaveChanges();

        _context.UserRoles?.AddRange(FakeData.UserRoles);
        _context.SaveChanges();

        _context.Medicines?.AddRange(FakeData.Medicines);
        _context.SaveChanges();

        _context.Orders?.AddRange(FakeData.Orders);
        _context.SaveChanges();
    }
}
