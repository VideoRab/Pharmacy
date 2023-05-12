using Entities.Enums;

namespace Entities;

public class Medicine
{
    public Guid Id { get; set; }
#pragma warning disable CS8618
    public string Name { get; set; }
#pragma warning restore CS8618
    public MedicineType Type { get; set; }
    public decimal Price { get; set; }
    public bool ByRecipe { get; set; }
    public Guid? OrderId { get; set; }
    public virtual Order? Order { get; set; }
}
