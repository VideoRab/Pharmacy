using Common.Mappers;
using Common.Services.Interfaces;
using DAL.Abstraction;
using Entities.DTO;

namespace Common.Services;

public class MedicineService : IMedicineService
{
    private readonly IMedicineRepository _medicineRepository;
    private readonly MedicineMapper _mapper;

    public MedicineService(IMedicineRepository repository, MedicineMapper mapper)
    {
        _medicineRepository = repository;
        _mapper = mapper;
    }

    public async Task CreateAsync(MedicineDto medicineDto, CancellationToken cancellationToken)
    {
        var medicine = _mapper.Map(medicineDto);

        await _medicineRepository.CreateAsync(medicine, cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var medicine = await GetByIdAsync(id, cancellationToken);
        if (medicine is null)
        {
            throw new NullReferenceException($"Unable to delete non-existent {nameof(medicine)}");
        }

        await _medicineRepository.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<MedicineDto>?> GetAllAsync(CancellationToken cancellationToken)
    {
        var medicines = await _medicineRepository.GetAllAsync(cancellationToken);
        if (medicines is null)
        {
            throw new NullReferenceException($"{nameof(medicines)} is null");
        }

        var medicinesDto = _mapper.ReverseMapList(medicines);

        return medicinesDto;
    }

    public async Task<MedicineDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var medicine = await _medicineRepository.GetByIdAsync(id, cancellationToken);
        if (medicine is null)
        {
            throw new NullReferenceException($"{nameof(medicine)} is null");
        }

        var medicineDto = _mapper.ReverseMap(medicine);

        return medicineDto;
    }

    public async Task UpdateAsync(MedicineDto medicineDto, CancellationToken cancellationToken)
    {
        var medicine = _mapper.Map(medicineDto);

        await _medicineRepository.UpdateAsync(medicine, cancellationToken);
    }
}
