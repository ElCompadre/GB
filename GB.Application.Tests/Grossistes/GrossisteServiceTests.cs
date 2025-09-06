using GB.Application.Interfaces.Repositories;
using GB.Application.Interfaces.Services;
using GB.Application.Services;
using GB.Domain.Errors;
using GB.Domain.Models;
using Moq;

namespace GB.Application.Tests.Grossistes;

public class GrossisteServiceTests
{
    private IGrossisteService _grossisteService;
    private readonly Mock<IGrossisteRepository> _grossisteRepositoryMock = new();
    private readonly Mock<IGrossisteBiereRepository> _grossisteBiereRepositoryMock = new();
    
    [Fact]
    public async Task AddAsync_Should_ThrowEntityAlreadyExistsException_When_AGrossisteAlreadyExists()
    {
        // Arrange
        _grossisteRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<GrossisteDTO>()))
            .Returns(true);
        _grossisteService = new GrossisteService(_grossisteRepositoryMock.Object, _grossisteBiereRepositoryMock.Object);
        
        // Act & Assert
        await Assert.ThrowsAsync<BusinessValidationException>(async () => await _grossisteService.AddAsync(new GrossisteDTO(), CancellationToken.None));
    } 
    
    [Fact]
    public void RemoveBiereFromCatalog_Should_ThrowEntityAlreadyExistsException_When_BiereIsNotInGrossisteCatalog()
    {
        // Arrange
        _grossisteBiereRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(false);
        _grossisteService = new GrossisteService(_grossisteRepositoryMock.Object, _grossisteBiereRepositoryMock.Object);
        
        // Act & Assert
        Assert.Throws<BusinessValidationException>(() => _grossisteService.RemoveBiereFromCatalog(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None));
    }
    
    [Fact]
    public async Task UpdateQuantityBiereAsync_Should_ThrowEntityAlreadyExistsException_When_BiereIsNotInGrossisteCatalog()
    {
        // Arrange
        _grossisteBiereRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(false);
        _grossisteService = new GrossisteService(_grossisteRepositoryMock.Object, _grossisteBiereRepositoryMock.Object);
        
        // Act & Assert
        await Assert.ThrowsAsync<BusinessValidationException>(async () => await _grossisteService.UpdateQuantityBiereAsync(new GrossisteBiereDTO
        {
            GrossisteId = 1,
            BiereId = 1
        }, CancellationToken.None));
    }
    
    [Fact]
    public async Task QuoteRequestAsync_Should_ThrowEntityAlreadyExistsException_When_GrossisteDoesNotExist()
    {
        // Arrange
        _grossisteRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<int>()))
            .Returns(false);
        _grossisteService = new GrossisteService(_grossisteRepositoryMock.Object, _grossisteBiereRepositoryMock.Object);
        
        // Act & Assert
        await Assert.ThrowsAsync<BusinessValidationException>(async () => await _grossisteService.QuoteRequestAsync(new QuoteRequestDTO
        {
            GrossisteId = 1,
            Items = new List<QuoteRequestItemDTO>()
        }, CancellationToken.None));
    }
    
    [Fact]
    public async Task QuoteRequestAsync_Should_ThrowEntityAlreadyExistsException_When_ThereIsADuplicateBiereId()
    {
        // Arrange
        _grossisteRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<int>()))
            .Returns(true);
        _grossisteService = new GrossisteService(_grossisteRepositoryMock.Object, _grossisteBiereRepositoryMock.Object);
        
        // Act & Assert
        await Assert.ThrowsAsync<BusinessValidationException>(async () => await _grossisteService.QuoteRequestAsync(new QuoteRequestDTO
        {
            GrossisteId = 1,
            Items = new List<QuoteRequestItemDTO>
            {
                new()
                {
                    BiereId = 1,
                    Quantite = 8
                },
                new()
                {
                    BiereId = 1,
                    Quantite = 10
                }
            }
        }, CancellationToken.None));
    }
    
    [Fact]
    public async Task QuoteRequestAsync_Should_ThrowEntityAlreadyExistsException_When_TheBiereIsNotInGrossisteCatalog()
    {
        // Arrange
        _grossisteRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<int>()))
            .Returns(true);
        var expectedGrossiste = new GrossisteDTO
        {
            Id = 1,
            Nom = "HLS"
        };
        _grossisteRepositoryMock.Setup(br => br.GetByIdAsync(It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(expectedGrossiste);
        _grossisteBiereRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(false);
        _grossisteService = new GrossisteService(_grossisteRepositoryMock.Object, _grossisteBiereRepositoryMock.Object);
        
        // Act & Assert
        await Assert.ThrowsAsync<BusinessValidationException>(async () => await _grossisteService.QuoteRequestAsync(new QuoteRequestDTO
        {
            GrossisteId = 1,
            Items = new List<QuoteRequestItemDTO>
            {
                new()
                {
                    BiereId = 1,
                    Quantite = 8
                },
                new()
                {
                    BiereId = 2,
                    Quantite = 10
                }
            }
        }, CancellationToken.None));
    }
    
    [Fact]
    public async Task QuoteRequestAsync_Should_ThrowEntityAlreadyExistsException_When_TheQuantityAskIsGreaterThenTheStock()
    {
        // Arrange
        _grossisteRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<int>()))
            .Returns(true);
        var expectedGrossiste = new GrossisteDTO
        {
            Id = 1,
            Nom = "HLS"
        };
        _grossisteRepositoryMock.Setup(br => br.GetByIdAsync(It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(expectedGrossiste);
        _grossisteBiereRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(true);
        var expectedGrossisteBiere = new GrossisteBiereDTO
        {
            Stock = 1
        };
        _grossisteBiereRepositoryMock.Setup(br => br.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(expectedGrossisteBiere);
        _grossisteService = new GrossisteService(_grossisteRepositoryMock.Object, _grossisteBiereRepositoryMock.Object);
        
        // Act & Assert
        await Assert.ThrowsAsync<BusinessValidationException>(async () => await _grossisteService.QuoteRequestAsync(new QuoteRequestDTO
        {
            GrossisteId = 1,
            Items = new List<QuoteRequestItemDTO>
            {
                new()
                {
                    BiereId = 1,
                    Quantite = 8
                },
                new()
                {
                    BiereId = 2,
                    Quantite = 10
                }
            }
        }, CancellationToken.None));
    }
    
    [Fact]
    public async Task QuoteRequestAsync_Should_ReturnQuoteResponseWithoutDiscount()
    {
        // Arrange
        _grossisteRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<int>()))
            .Returns(true);
        var expectedGrossiste = new GrossisteDTO
        {
            Id = 1,
            Nom = "HLS"
        };
        _grossisteRepositoryMock.Setup(br => br.GetByIdAsync(It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(expectedGrossiste);
        _grossisteBiereRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(true);
        var expectedGrossisteBiere = new GrossisteBiereDTO
        {
            Stock = 12,
            Biere = new BiereDTO
            {
                Nom = "Leffe blonde",
                Prix = 2.20m
            }
        };
        _grossisteBiereRepositoryMock.Setup(br => br.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(expectedGrossisteBiere);
        _grossisteService = new GrossisteService(_grossisteRepositoryMock.Object, _grossisteBiereRepositoryMock.Object);
        var quoteRequest = new QuoteRequestDTO
        {
            GrossisteId = 1,
            Items = new List<QuoteRequestItemDTO>
            {
                new()
                {
                    BiereId = 1,
                    Quantite = 8
                },
                new()
                {
                    BiereId = 2,
                    Quantite = 10
                }
            }
        };
        
        // Act
        var quoteResponse = await _grossisteService.QuoteRequestAsync(quoteRequest, CancellationToken.None);
        // Assert
        Assert.NotNull(quoteResponse);
        Assert.Equal(quoteRequest.Items.Count, quoteResponse.Items.Count);
    }
    
    [Fact]
    public async Task AddAsync_Should_ReturnGrossisteCreated()
    {
        // Arrange
        _grossisteRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<GrossisteDTO>()))
            .Returns(false);
        var expectedGrossiste = new GrossisteDTO
        {
            Id = 1,
            Nom = "HLS",
        };
        _grossisteRepositoryMock.Setup(br => br.AddAsync(It.IsAny<GrossisteDTO>(), CancellationToken.None))
            .ReturnsAsync(expectedGrossiste);
        _grossisteService = new GrossisteService(_grossisteRepositoryMock.Object, _grossisteBiereRepositoryMock.Object);
        
        // Act
        var grossisteToAssert = await _grossisteService.AddAsync(new GrossisteDTO());
        
        // Assert
        Assert.NotNull(grossisteToAssert);
        Assert.Equal(expectedGrossiste.Id, grossisteToAssert.Id);
        Assert.Equal(expectedGrossiste.Nom, grossisteToAssert.Nom);
    }
}