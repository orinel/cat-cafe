namespace CatService.Data.Entities;

public class CatEntity
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public int Age { get; set; }
    public Gender Gender { get; init; }
    public Breed Breed { get; init; }
    public Activity Activity { get; set; }
    public int? ExternalActivityId { get; set; }
    public DateTimeOffset? ActivityStartsAt { get; set; }
    public DateTimeOffset? ActivityEndsAt   { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
}