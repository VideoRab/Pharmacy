using Microsoft.AspNetCore.Identity;

namespace Entities;

public class User : IdentityUser<Guid>
{
    public virtual IList<Order>? Orders { get; set; }
}
