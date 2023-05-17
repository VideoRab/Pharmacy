using Entities.Enums;

namespace Entities.DTO;

public class MedicineDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public MedicineType Type { get; set; }
    public decimal Price { get; set; }
    public bool ByRecipe { get; set; }
    public Guid? OrderId { get; set; }
}
