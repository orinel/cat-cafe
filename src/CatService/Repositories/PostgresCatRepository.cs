namespace CatService.Repositories;

using Data;
using Google.Protobuf.WellKnownTypes;

public class PostgresCatRepository : ICatRepository
{
    private readonly CatCafeDbContext context;
    
    public PostgresCatRepository(CatCafeDbContext context)
    {
        this.context = context;
    }

    public List<CatItem> GetAllCats()
    {
        var cats = context.Cats.ToList();
        return cats
            .Select(cat => new CatItem
            {
                Id = cat.Id,
                Name = cat.Name,
                Age = cat.Age,
                Breed = cat.Breed,
                Status = cat.Status,
                CreatedAt = Timestamp.FromDateTimeOffset(cat.CreatedAt)
            }).ToList();
    }

    public CatItem? GetCatById(int id)
    {
        var cat = context.Cats.Find(id);
        if (cat == null)
        {
            return null;
        }
        return new CatItem
        {
            Id = cat.Id,
            Name = cat.Name,
            Age = cat.Age,
            Breed = cat.Breed,
            Status = cat.Status,
            CreatedAt = Timestamp.FromDateTimeOffset(cat.CreatedAt)
        };
    }
}