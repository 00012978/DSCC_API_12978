namespace DSCC.API.Data;

public record PetResponse(int Id, string Name, string Species, int? AdoptionId);
public record PetRequest(string Name, string Species, int? AdoptionId);
public record AdoptionResponse(int Id, DateTime Date, int PetId);
public record AdoptionRequest(DateTime Date, int PetId);