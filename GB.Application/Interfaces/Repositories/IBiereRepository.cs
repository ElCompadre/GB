using GB.Domain.Models;

namespace GB.Application.Interfaces.Repositories;

public interface IBiereRepository
{
    public ICollection<BiereDTO> GetAllByBrasserie(int brasserieId);
    public BiereDTO Insert(BiereDTO biereDto);
    public void Delete(int biereId);
}