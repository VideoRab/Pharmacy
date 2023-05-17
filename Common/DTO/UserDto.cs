﻿namespace Entities.DTO;

public class UserDto
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public IList<OrderDto>? Orders { get; set; }
}
