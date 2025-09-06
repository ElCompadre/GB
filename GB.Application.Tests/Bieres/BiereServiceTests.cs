using GB.Application.Interfaces.Repositories;
using GB.Application.Interfaces.Services;
using GB.Application.Services;
using GB.Domain.Errors;
using GB.Domain.Models;
using Moq;

namespace GB.Application.Tests;

public class BiereServiceTests
{
    private IBiereService _biereService;
    private readonly Mock<IBiereRepository> _biereRepositoryMock = new();
    private readonly Mock<IGrossisteBiereRepository> _grossisteBiereRepositoryMock = new();
    
    [Fact]
    public async Task AddAsync_Should_ThrowEntityAlreadyExistsException_When_ABiereAlreadyExists()
    {
        // Arrange
        _biereRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<BiereDTO>()))
            .Returns(true);
        _biereService = new BiereService(_biereRepositoryMock.Object, _grossisteBiereRepositoryMock.Object);
        
        // Act & Assert
        await Assert.ThrowsAsync<BusinessValidationException>(async () => await _biereService.AddAsync(new BiereDTO()));
    }
    
    [Fact]
    public async Task AddAsync_Should_ReturnBiereCreated()
    {
        // Arrange
        _biereRepositoryMock.Setup(br => br.CheckIfExists(It.IsAny<BiereDTO>()))
            .Returns(false);
        var expectedBiere = new BiereDTO
        {
            Id = 1,
            Nom = "Leffe blonde",
            DegresAlcool = 6.6m,
            Prix = 2.20m,
            BrasserieId = 1
        };
        _biereRepositoryMock.Setup(br => br.AddAsync(It.IsAny<BiereDTO>(), CancellationToken.None))
            .ReturnsAsync(expectedBiere);
        _biereService = new BiereService(_biereRepositoryMock.Object, _grossisteBiereRepositoryMock.Object);
        
        // Act
        var biereToAssert = await _biereService.AddAsync(new BiereDTO());
        
        // Assert
        Assert.NotNull(biereToAssert);
        Assert.Equal(expectedBiere.Id, biereToAssert.Id);
        Assert.Equal(expectedBiere.Nom, biereToAssert.Nom);
        Assert.Equal(expectedBiere.DegresAlcool, biereToAssert.DegresAlcool);
        Assert.Equal(expectedBiere.Prix, biereToAssert.Prix);
        Assert.Equal(expectedBiere.BrasserieId, biereToAssert.BrasserieId);
    }
}