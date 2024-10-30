using System.Text.Json.Serialization;

namespace DSCC.API.Data.Models;

public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public int? AdoptionId { get; set; }
    [JsonIgnore]
    public Adoption? Adoption { get; set; }
}
