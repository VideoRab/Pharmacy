using Entities;

namespace DAL.Abstraction;

public interface IMedicineRepository
{
    Task<IEnumerable<Medicine>?> GetAllAsync(CancellationToken cancellationToken);
    Task<Medicine?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(Medicine medicine, CancellationToken cancellationToken);
    Task UpdateAsync(Medicine medicine, CancellationToken cancellationToken);
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}
