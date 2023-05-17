using Entities;
using Entities.DTO;
using Riok.Mapperly.Abstractions;

namespace Common.Mappers;

[Mapper]
public partial class MedicineMapper
{
    public partial Medicine Map(MedicineDto medicineDto);
    public partial MedicineDto ReverseMap(Medicine medicine);
    public partial IEnumerable<Medicine> MapList(IEnumerable<MedicineDto> medicineDtoList);
    public partial IEnumerable<MedicineDto> ReverseMapList(IEnumerable<Medicine> medicineList);
}
