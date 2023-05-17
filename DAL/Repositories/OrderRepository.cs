using DAL.Abstraction;
using DAL.Data;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class OrderRepository :IOrderRepository
{
    private readonly ApplicationContext _applicationContext;
    private readonly DbSet<Order> _dbSet;

    public OrderRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
        _dbSet = _applicationContext.Set<Order>();
    }

    public async Task CreateAsync(Order order, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(order, cancellationToken);

        await _applicationContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await _dbSet.FindAsync(id, cancellationToken);

        _dbSet.Remove(order!);

        await _applicationContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Order>?> GetAllAsync(CancellationToken cancellationToken)
    {
        var orders = await _dbSet.AsNoTracking()
            .Include(x => x.Medicines)
            .Include(x => x.User)
            .ToListAsync(cancellationToken);

        return orders;
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await _dbSet.AsNoTracking()
            .Include(x => x.Medicines)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        return order;
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        _applicationContext.Entry(order).State = EntityState.Modified;
        
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }
}
