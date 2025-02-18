namespace eCommerce.Core.DTO;

public record AuthenticationResponse(
  Guid UserID,
  string? Email,
  string? PersonName,
  string? Gender,
  string? Token,
  bool Success
  )
{
    //Parameterless constructor  para poder ser usado por el AutoMapper, pero para cumplir
    //con características de los records se mandan todas las propiedades como default de cada datatype
    //Es decir usando el primary constructor o invocándolo
    public AuthenticationResponse() : this(default, default, default, default, default, default)
    {
    }
}