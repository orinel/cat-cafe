using Google.Protobuf.WellKnownTypes;

namespace CatService.Repositories;

public class InMemoryCatRepository : ICatRepository
{
    private List<CatItem> CatsList { get; }  = 
    [
        CreateSeedCat(1, "Барсик", 3, Gender.Male, Breed.DomesticCat, Activity.Eating),
        CreateSeedCat(2, "Мурка", 5, Gender.Female, Breed.DomesticCat, Activity.Grooming),
        CreateSeedCat(3, "Васька", 2, Gender.Male, Breed.DomesticCat, Activity.Resting),
    ];
    
    private static CatItem CreateSeedCat(
        int id,
        string name,
        int age,
        Gender gender,
        Breed breed,
        Activity activity)
    {
        return new CatItem
        {
            Id = id,
            Name = name,
            Age = age,
            Gender = gender,
            Breed = breed,
            CatActivityState = new CatActivityState
            {
                Activity = activity,
            },
            CreatedAt = Timestamp.FromDateTimeOffset(DateTimeOffset.UtcNow)
        };
    }
    
    public CatItem CreateCat(string name, int age, Gender gender, Breed breed)
    {
        var id = CatsList.Count == 0
            ? 1
            : CatsList.Max(cat => cat.Id) + 1;
        
        var cat = new CatItem
        {
            Id = id,
            Name = name,
            Age = age,
            Gender = gender,
            Breed = breed,
            CatActivityState = new CatActivityState
            {
                Activity = Activity.Idle,
            },
            CreatedAt = Timestamp.FromDateTimeOffset(DateTimeOffset.UtcNow)
        };
        
        CatsList.Add(cat);

        return cat;
    }
    
    public List<CatItem> GetAllCats()
    {
        return CatsList.ToList();
    }
    
    public CatItem? GetCatById(int id)
    {
        return CatsList.Find(cat => cat.Id == id);
    }
    
    public bool SetCatActivity(
        int catId,
        Activity activity,
        int? externalActivityId,
        DateTimeOffset? activityStartsAt,
        DateTimeOffset? activityEndsAt)
    {
        var cat = CatsList.Find(cat => cat.Id == catId);
        if (cat == null) return false;
        
        var state = cat.CatActivityState;
        state.Activity = activity;
        
        if (externalActivityId.HasValue)
        {
            state.ExternalActivityId = externalActivityId.Value;
        }
        else
        {
            state.ClearExternalActivityId();
        }
        
        if (activityStartsAt.HasValue)
        {
            state.StartTime = Timestamp.FromDateTimeOffset(activityStartsAt.Value);
        }

        if (activityEndsAt.HasValue)
        {
            state.EndTime = Timestamp.FromDateTimeOffset(activityEndsAt.Value);
        }
        
        return true;
    }
    
    public bool DeleteCat(int id)
    {
        return CatsList.RemoveAll(cat => cat.Id == id) > 0;
    }
}