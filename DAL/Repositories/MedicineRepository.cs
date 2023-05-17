using DAL.Abstraction;
using DAL.Data;
using Entities;
using IdentityModel;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class MedicineRepository : IMedicineRepository
{
    private readonly ApplicationContext _applicationContext;
    private readonly DbSet<Medicine> _dbSet;

    public MedicineRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
        _dbSet = _applicationContext.Set<Medicine>();
    }
    public async Task CreateAsync(Medicine medicine, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(medicine, cancellationToken);

        await _applicationContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var medicine = await _dbSet.FindAsync(id, cancellationToken);

        _dbSet.Remove(medicine!);

        await _applicationContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Medicine>?> GetAllAsync(CancellationToken cancellationToken)
    {
        var medicines = await _dbSet.AsNoTracking()
            .Include(x => x.Order)
            .ToListAsync(cancellationToken);

        return medicines;
    }

    public async Task<Medicine?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var medicine = await _dbSet.AsNoTracking()
            .Include(x => x.Order)
            .FirstOrDefaultAsync(x => x.Id == id);

        return medicine;
    }

    public async Task UpdateAsync(Medicine medicine, CancellationToken cancellationToken)
    {
        _applicationContext.Entry(medicine).State = EntityState.Modified;

        await _applicationContext.SaveChangesAsync(cancellationToken);
    }
}