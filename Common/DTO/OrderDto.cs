namespace Entities.DTO;

public class OrderDto
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public string? DealCode { get; set; }
    public IList<MedicineDto>? Medicines { get; set; }
}
