using Entities;
using Entities.DTO;
using Riok.Mapperly.Abstractions;

namespace Common.Mappers;

[Mapper]
public partial class UserMapper
{
    public partial User Map(UserDto userDto);
    public partial UserDto ReverseMap(User user);
}
