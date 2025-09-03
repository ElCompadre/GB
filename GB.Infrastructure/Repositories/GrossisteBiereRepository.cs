using GB.Application.Interfaces.Repositories;
using GB.Infrastructure.Context;

namespace GB.Infrastructure.Repositories;

public class GrossisteBiereRepository(GBContext context) : IGrossisteBiereRepository
{
    public void DeleteLinkBiereToGrossiste(int biereId)
    {
        var grossisteBieres = context.GrossisteBieres.Where(gb => gb.BiereId == biereId);
        context.GrossisteBieres.RemoveRange(grossisteBieres);
        context.SaveChanges();
    }
}