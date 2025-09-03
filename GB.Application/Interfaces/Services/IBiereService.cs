using GB.Domain.Models;

namespace GB.Application.Interfaces.Services;

public interface IBiereService
{
    public Task<BiereDTO> AddAsync(BiereDTO biereDto, CancellationToken cancellationToken = default);
    public void Delete(int biereId);
}