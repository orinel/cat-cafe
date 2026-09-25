using CatService.Data.Entities;
using CatService.Data;
using Google.Protobuf.WellKnownTypes;

namespace CatService.Repositories;

public class PostgresCatRepository : ICatRepository
{
    private readonly CatCafeDbContext context;
    
    public PostgresCatRepository(CatCafeDbContext context)
    {
        this.context = context;
    }

    private static CatItem MapToCatItem(CatEntity cat)
    {
        return new CatItem{
            Id = cat.Id,
            Name = cat.Name,
            Age = cat.Age,
            Breed = cat.Breed,
            Status = cat.Status,
            Activity = cat.Activity,
            CreatedAt = Timestamp.FromDateTimeOffset(cat.CreatedAt)
        };
    }

    public CatItem CreateCat(string name, int age, Breed breed)
    {
        var cat = new CatEntity
        {
            Name = name,
            Age = age,
            Breed = breed,
            Status = CatStatus.Available,
            Activity = CatActivity.Idle,
        };
        context.Cats.Add(cat);
        context.SaveChanges();
        return MapToCatItem(cat);
    }

    public List<CatItem> GetAllCats()
    {
        var cats = context.Cats.ToList();
        return cats
            .Select(MapToCatItem).ToList();
    }

    public CatItem? GetCatById(int id)
    {
        var cat = context.Cats.Find(id);
        return cat == null ? null : MapToCatItem(cat);
    }

    public bool DeleteCat(int id)
    {
        var cat = context.Cats.Find(id);
        if (cat == null)
        {
            return false;
        }
        context.Cats.Remove(cat);
        return context.SaveChanges() > 0;
    }
}