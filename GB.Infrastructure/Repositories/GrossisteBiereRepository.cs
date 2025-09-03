using GB.Application.Interfaces;
using GB.Application.Interfaces.Repositories;

namespace GB.Infrastructure.Repositories;

public class GrossisteBiereRepository(IGBContext context) : IGrossisteBiereRepository
{
    public void DeleteLinkBiereToGrossiste(int biereId)
    {
        var grossisteBieres = context.GrossisteBieres.Where(gb => gb.BiereId == biereId);
        context.GrossisteBieres.RemoveRange(grossisteBieres);
        context.SaveChanges();
    }
}