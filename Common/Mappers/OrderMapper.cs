using Entities;
using Entities.DTO;
using Riok.Mapperly.Abstractions;

namespace Common.Mappers;

[Mapper]
public partial class OrderMapper
{
    public partial Order Map(OrderDto orderDto);
    public partial OrderDto ReverseMap(Order order);
    public partial IEnumerable<Order> MapList(IEnumerable<OrderDto> orderDtoList);
    public partial IEnumerable<OrderDto> ReverseMapList(IEnumerable<Order> orderList);
}
