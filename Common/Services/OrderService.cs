using Common.Mappers;
using Common.Services.Interfaces;
using DAL.Abstraction;
using Entities;
using Entities.DTO;

namespace Common.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly OrderMapper _mapper;

    public OrderService(IOrderRepository repository, OrderMapper mapper)
    {
        _orderRepository = repository;
        _mapper = mapper;
    }

    public async Task CreateAsync(OrderDto orderDto, CancellationToken cancellationToken)
    {
        var order = _mapper.Map(orderDto);

        await _orderRepository.CreateAsync(order, cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await GetByIdAsync(id, cancellationToken);
        if (order is null)
        {
            throw new NullReferenceException($"Unable to delete non-existent {nameof(order)}");
        }

        await _orderRepository.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<OrderDto>?> GetAllAsync(CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(cancellationToken);
        if (orders is null)
        {
            throw new NullReferenceException($"{nameof(orders)} is null");
        }

        var ordersDto = _mapper.ReverseMapList(orders);

        return ordersDto;
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
        if (order is null)
        {
            throw new NullReferenceException($"{nameof(order)} is null");
        }

        var orderDto = _mapper.ReverseMap(order);

        return orderDto;
    }

    public async Task UpdateAsync(OrderDto orderDto, CancellationToken cancellationToken)
    {
        var order = _mapper.Map(orderDto);

        await _orderRepository.UpdateAsync(order, cancellationToken);
    }
}
