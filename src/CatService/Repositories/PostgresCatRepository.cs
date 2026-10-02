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

    private static CatActivityState MapToCatActivityState(CatEntity cat)
    {
        var state = new CatActivityState
        {
            Activity = cat.Activity
        };
        
        if (cat.ExternalActivityId.HasValue)
            state.ExternalActivityId = cat.ExternalActivityId.Value;
        
        if (cat.ActivityStartsAt.HasValue)
            state.StartTime = Timestamp.FromDateTimeOffset(cat.ActivityStartsAt.Value);
        
        if (cat.ActivityEndsAt.HasValue)
            state.EndTime =  Timestamp.FromDateTimeOffset(cat.ActivityEndsAt.Value);
        
        return state;
    }
    
    private static CatItem MapToCatItem(CatEntity cat)
    {
        return new CatItem{
            Id = cat.Id,
            Name = cat.Name,
            Age = cat.Age,
            Gender = cat.Gender,
            Breed = cat.Breed,
            CatActivityState =  MapToCatActivityState(cat),
            CreatedAt = Timestamp.FromDateTimeOffset(cat.CreatedAt)
        };
    }
    
    public CatItem CreateCat(string name, int age, Gender gender, Breed breed)
    {
        var cat = new CatEntity
        {
            Name = name,
            Age = age,
            Gender = gender,
            Breed = breed,
            Activity = Activity.Idle,
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
    
    public bool SetCatActivity(
        int id,
        Activity activity,
        int? externalActivityId,
        DateTimeOffset? activityStartsAt,
        DateTimeOffset? activityEndsAt)
    {
        var cat = context.Cats.Find(id);
        if (cat == null) return false;
        
        cat.Activity = activity;
        cat.ExternalActivityId = externalActivityId;
        cat.ActivityStartsAt = activityStartsAt;
        cat.ActivityEndsAt = activityEndsAt;
        
        context.SaveChanges();
        return true;
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