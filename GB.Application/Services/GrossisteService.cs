using GB.Application.Interfaces.Repositories;
using GB.Application.Interfaces.Services;
using GB.Domain.Errors;
using GB.Domain.Models;

namespace GB.Application.Services;

public class GrossisteService(IGrossisteRepository grossisteRepository, IGrossisteBiereRepository grossisteBiereRepository) : IGrossisteService
{
    public Task<GrossisteDTO> AddAsync(GrossisteDTO grossisteDto, CancellationToken cancellation = default)
    {
        if (grossisteRepository.CheckIfExists(grossisteDto))
        {
            throw new BusinessValidationException("Un grossiste avec cet id ou ce nom existe déjà");
        }
        return grossisteRepository.AddAsync(grossisteDto, cancellation);
    }

    public void RemoveBiereFromCatalog(int grossisteId, int biereId, CancellationToken cancellation = default)
    {
        if (!grossisteBiereRepository.CheckIfExists(grossisteId, biereId))
        {
            throw new BusinessValidationException("Cette bière n'est pas dans le catalogue du grossiste");
        }
        
        grossisteBiereRepository.RemoveBiereFromCatalog(grossisteId, biereId);
    }

    public async Task UpdateQuantityBiereAsync(GrossisteBiereDTO grossisteBiereDto, CancellationToken cancellation = default)
    {
        if (!grossisteBiereRepository.CheckIfExists(grossisteBiereDto.GrossisteId, grossisteBiereDto.BiereId))
        {
            throw new BusinessValidationException("Cette bière n'est pas dans le catalogue du grossiste");
        }

        await grossisteBiereRepository.UpdateQuantityBiereAsync(grossisteBiereDto, cancellation);
    }

    public async Task<QuoteResponseDTO> QuoteRequestAsync(QuoteRequestDTO quoteRequestDto, CancellationToken cancellation = default)
    {
        if (!grossisteRepository.CheckIfExists(quoteRequestDto.GrossisteId))
        {
            throw new BusinessValidationException("Le grossiste doit exister");
        }

        if (quoteRequestDto.Items.Any(qr => quoteRequestDto.Items.Count(q => q.BiereId == qr.BiereId) > 1))
        {
            throw new BusinessValidationException("Il ne peut y avoir de doublon dans la commande");
        }
        
        var grossiste = await grossisteRepository.GetByIdAsync(quoteRequestDto.GrossisteId, cancellation);
        var quoteResponse = new QuoteResponseDTO
        {
            Nom = grossiste.Nom
        };

        foreach (var quoteRequestItemDto in quoteRequestDto.Items)
        {
            if (!grossisteBiereRepository.CheckIfExists(quoteRequestDto.GrossisteId, quoteRequestItemDto.BiereId))
            {
                throw new BusinessValidationException("La bière doit être vendue par le grossiste");
            }
            var grossisteBiere = await grossisteBiereRepository.GetByIdAsync(quoteRequestDto.GrossisteId, quoteRequestItemDto.BiereId, cancellation);
            if (grossisteBiere.Stock < quoteRequestItemDto.Quantite)
            {
                throw new BusinessValidationException("Le nombre de bières commandées ne doit pas être supérieur au stock du grossiste");
            }

            // Si réduction à la ligne
            // quoteResponse.Items.Add(new QuoteItemResponseDTO
            // {
            //     Nom = grossisteBiere.Biere?.Nom ?? string.Empty,
            //     Prix = CalculePrix(quoteRequestItemDto.Quantite, grossisteBiere.Biere?.Prix ?? 0m),
            //     Quantite = quoteRequestItemDto.Quantite
            // });
            
            // Si réduction prix total 
            quoteResponse.Items.Add(new QuoteItemResponseDTO
            {
                Nom = grossisteBiere.Biere?.Nom ?? string.Empty,
                Prix = quoteRequestItemDto.Quantite * grossisteBiere.Biere?.Prix ?? 0m,
                Quantite = quoteRequestItemDto.Quantite
            });
        }
        
        // Si réduction à la ligne
        // quoteResponse.PrixTotal = quoteResponse.Items.Sum(q => q.Prix);
            
        // Si réduction prix total 
        quoteResponse.PrixTotal = CalculePrix(quoteResponse.Items.Sum(q => q.Quantite), quoteResponse.Items.Sum(q => q.Prix));

        return quoteResponse;
    }

    // Pas très claire la fonctionnalité demandée, la réduction doit se faire sur la ligne ou sur le total ?
    // J'ai pris parti pour la réduction à la ligne
    private decimal CalculePrix(int quantite, decimal prix)
    {
        var prixFinal = quantite * (quantite > 10 ? quantite > 20 ? (prix * 0.2m) : (prix * 0.1m) : prix);
        return prixFinal;
    }

    public async Task AddBiereToCatalogAsync(int grossisteId, int biereId, CancellationToken cancellation = default)
    {
        if (grossisteBiereRepository.CheckIfExists(grossisteId, biereId))
        {
            throw new BusinessValidationException("Cette bière est déjà au catalogue");
        }

        var grossisteBiereDto = new GrossisteBiereDTO
        {
            GrossisteId = grossisteId,
            BiereId = biereId
        };
        
        await grossisteBiereRepository.AddAsync(grossisteBiereDto, cancellation);
    }
}