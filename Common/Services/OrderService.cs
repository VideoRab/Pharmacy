using Common.Mappers;
using Common.Services.Interfaces;
using DAL.Abstraction;
using Entities;
using Entities.DTO;
using FluentValidation;

namespace Common.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IValidator<OrderDto> _validator;
    private readonly OrderMapper _mapper;

    public OrderService(IOrderRepository repository,
        IValidator<OrderDto> validator,
        OrderMapper mapper)
    {
        _orderRepository = repository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task CreateAsync(OrderDto orderDto, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(orderDto);

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
        _validator.ValidateAndThrow(orderDto);

        var order = _mapper.Map(orderDto);

        await _orderRepository.UpdateAsync(order, cancellationToken);
    }
}
