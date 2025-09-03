using GB.Application.Interfaces.Repositories;
using GB.Application.Interfaces.Services;
using GB.Domain.Errors;
using GB.Domain.Models;

namespace GB.Application.Services;

public class GrossisteService(IGrossisteRepository grossisteRepository) : IGrossisteService
{
    public Task<GrossisteDTO> AddAsync(GrossisteDTO grossisteDto, CancellationToken cancellation = default)
    {
        if (grossisteRepository.CheckIfExists(grossisteDto))
        {
            throw new EntityAlreadyExistsException("Un grossiste avec cet id ou ce nom existe déjà");
        }
        return grossisteRepository.AddAsync(grossisteDto, cancellation);
    }
}