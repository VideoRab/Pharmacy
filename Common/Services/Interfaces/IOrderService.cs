using Entities;
using Entities.DTO;

namespace Common.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>?> GetAllAsync(CancellationToken cancellationToken);
    Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(OrderDto orderDto, CancellationToken cancellationToken);
    Task UpdateAsync(OrderDto orderDto, CancellationToken cancellationToken);
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}
