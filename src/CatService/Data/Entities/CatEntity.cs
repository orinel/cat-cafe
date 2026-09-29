namespace CatService.Data.Entities;

public class CatEntity
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public int Age { get; set; }
    public Gender Gender { get; init; }
    public Breed Breed { get; init; }
    public CatActivity Activity { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
}