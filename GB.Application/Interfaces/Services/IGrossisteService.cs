using GB.Domain.Models;

namespace GB.Application.Interfaces.Services;

public interface IGrossisteService
{
    public Task<GrossisteDTO> AddAsync(GrossisteDTO grossisteDto, CancellationToken cancellation = default);
}