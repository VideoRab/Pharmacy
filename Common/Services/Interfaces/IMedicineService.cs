using Entities.DTO;

namespace Common.Services.Interfaces;

public interface IMedicineService
{
    Task<IEnumerable<MedicineDto>?> GetAllAsync(CancellationToken cancellationToken);
    Task<MedicineDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(MedicineDto medicineDto, CancellationToken cancellationToken);
    Task UpdateAsync(MedicineDto medicineDto, CancellationToken cancellationToken);
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}
