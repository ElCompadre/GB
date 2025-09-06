namespace GB.Application.Interfaces.Repositories;

public interface IGrossisteBiereRepository
{
    public Task<GrossisteBiereDTO> AddAsync(GrossisteBiereDTO grossisteBiereDto, CancellationToken cancellationToken = default);
    public void RemoveBiereFromCatalog(int grossisteId, int biereId);
    public void RemoveBiereFromAllCatalogs(int biereId);

    public bool CheckIfExists(int grossisteId, int biereId);
    public Task UpdateQuantityBiereAsync(GrossisteBiereDTO grossisteBiereDto, CancellationToken cancellation = default);
    
    public Task<GrossisteBiereDTO> GetByIdAsync(int grossisteId, int biereId, CancellationToken cancellationToken = default);
}