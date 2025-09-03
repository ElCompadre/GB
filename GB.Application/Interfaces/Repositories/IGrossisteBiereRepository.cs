namespace GB.Application.Interfaces.Repositories;

public interface IGrossisteBiereRepository
{
    public Task<GrossisteBiereDTO> AddAsync(GrossisteBiereDTO grossisteBiereDto, CancellationToken cancellationToken = default);
    public void DeleteLinkBiereToGrossiste(int biereId);

    public bool CheckIfExists(int biereId, int grossisteId);
}