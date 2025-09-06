using GB.Domain.Models;

namespace GB.Application.Interfaces.Repositories;

public interface IGrossisteRepository
{
    public Task<GrossisteDTO> AddAsync(GrossisteDTO grossisteDto, CancellationToken cancellation = default);
    public bool CheckIfExists(GrossisteDTO grossisteDto);
    public bool CheckIfExists(int grossisteId);
    
    public Task<GrossisteDTO> GetByIdAsync(int id, CancellationToken cancellation = default);
}