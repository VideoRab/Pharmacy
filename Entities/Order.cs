namespace Entities;

public class Order
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
#pragma warning disable CS8618
    public string DealCode { get; set; }
#pragma warning restore CS8618
    public virtual IList<Medicine>? Medicines { get; set; }
    public Guid? UserId { get; set; }
    public virtual User? User { get; set; }
}
