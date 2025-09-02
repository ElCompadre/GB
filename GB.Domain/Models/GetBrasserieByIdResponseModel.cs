namespace GB.Domain.Models;

public class GetBrasserieByIdResponseModel
{
    public int Id { get; init; }

    public string Nom { get; init; } = string.Empty;
    
    public ICollection<GetBrasserieByIdBiereResponseModel> Bieres { get; init; } = new List<GetBrasserieByIdBiereResponseModel>();
}

public class GetBrasserieByIdBiereResponseModel
{
    public int Id { get; init; }

    public string Nom { get; init; } = string.Empty;

    public decimal DegresAlcool { get; init; }

    public decimal Prix { get; init; }
    
    public ICollection<GetBrasserieByIdGrossisteResponseModel>  Grossistes { get; init; } = new List<GetBrasserieByIdGrossisteResponseModel>();
}

public class GetBrasserieByIdGrossisteResponseModel
{
    public int Id { get; init; }

    public string Nom { get; init; } = string.Empty;
}