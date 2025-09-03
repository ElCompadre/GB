using GB.Domain.Models;

namespace GB.Application.Interfaces.Repositories;

public interface IBiereRepository
{
    public Task<BiereDTO> AddAsync(BiereDTO biereDto, CancellationToken cancellation = default);
    public void Delete(int biereId);
}