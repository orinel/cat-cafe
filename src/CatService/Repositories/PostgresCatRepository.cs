using CatService.Data.Entities;

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
        return new CatItem
        {
            Id = cat.Id,
            Name = cat.Name,
            Age = cat.Age,
            Breed = cat.Breed,
            Status = cat.Status,
            Activity = cat.Activity,
            CreatedAt = Timestamp.FromDateTimeOffset(cat.CreatedAt)
        };
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
                Activity =  cat.Activity,
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
            Activity =  cat.Activity,
            CreatedAt = Timestamp.FromDateTimeOffset(cat.CreatedAt)
        };
    }
}