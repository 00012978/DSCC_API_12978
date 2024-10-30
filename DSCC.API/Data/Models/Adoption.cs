namespace DSCC.API.Data.Models;

public class Adoption
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public Pet Pet { get; set; }
}
