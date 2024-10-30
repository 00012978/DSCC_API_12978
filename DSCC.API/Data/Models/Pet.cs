namespace DSCC.API.Data.Models;

public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public Adoption? Adoption { get; set; }
}
