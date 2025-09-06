using GB.Application.Interfaces.Repositories;
using GB.Application.Interfaces.Services;
using GB.Application.Services;
using GB.Domain.Errors;
using GB.Domain.Models;
using Moq;

namespace GB.Application.Tests.Brasseries;

public class BrasserieServiceTests
{
    private IBrasserieService _brasserieService;
    private readonly Mock<IBrasserieRepository> _brasserieRepositoryMock = new();
    
    [Fact]
    public async Task AddAsync_Should_ThrowEntityAlreadyExistsException_When_ABrasserieAlreadyExists()
    {
        // Arrange
        _brasserieRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<BrasserieDTO>()))
            .Returns(true);
        _brasserieService = new BrasserieService(_brasserieRepositoryMock.Object);
        // Act & Assert
        await Assert.ThrowsAsync<BusinessValidationException>(async () => await _brasserieService.AddAsync(new BrasserieDTO()));
    }
    
    [Fact]
    public async Task AddAsync_Should_ReturnBrasserieCreated()
    {
        // Arrange
        _brasserieRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<BrasserieDTO>()))
            .Returns(false);
        var expectedBrasserie = new BrasserieDTO
        {
            Id = 1,
            Nom = "Abbaye de leffe"
        };
        _brasserieRepositoryMock.Setup(br => br.AddAsync(It.IsAny<BrasserieDTO>(), CancellationToken.None))
            .ReturnsAsync(expectedBrasserie);
        _brasserieService = new BrasserieService(_brasserieRepositoryMock.Object);
        
        // Act
        var brasserieToAssert = await _brasserieService.AddAsync(new BrasserieDTO());
        
        // Assert
        Assert.NotNull(brasserieToAssert);
        Assert.Equal(expectedBrasserie.Id, brasserieToAssert.Id);
        Assert.Equal(expectedBrasserie.Nom, brasserieToAssert.Nom);
    }
}