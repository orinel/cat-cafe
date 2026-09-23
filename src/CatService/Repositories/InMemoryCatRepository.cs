using Google.Protobuf.WellKnownTypes;

namespace CatService.Repositories;

public class InMemoryCatRepository : ICatRepository
{
    private List<CatItem> CatsList { get; }  = 
    [
        CreateSeedCat(1, "Барсик", 3, Breed.DomesticCat, CatStatus.Available, CatActivity.Eating),
        CreateSeedCat(2, "Мурзик", 5, Breed.DomesticCat, CatStatus.Available, CatActivity.Grooming),
        CreateSeedCat(3, "Васька", 2, Breed.DomesticCat, CatStatus.Booked, CatActivity.Resting),
    ];

    private static CatItem CreateSeedCat(int id, string name, int age, Breed breed, CatStatus status, CatActivity activity)
    {
        return new CatItem
        {
            Id = id,
            Name = name,
            Age = age,
            Breed = breed,
            Status = status,
            Activity = activity,
            CreatedAt = Timestamp.FromDateTimeOffset(DateTimeOffset.UtcNow)
        };
    }

    public CatItem CreateCat(string name, int age, Breed breed)
    {
        var id = CatsList.Count == 0
            ? 1
            : CatsList.Max(cat => cat.Id) + 1;
        
        var cat = new CatItem
        {
            Id = id,
            Name = name,
            Age = age,
            Breed = breed,
            Status = CatStatus.Available,
            Activity = CatActivity.Idle,
            CreatedAt = Timestamp.FromDateTimeOffset(DateTimeOffset.UtcNow)
        };
        
        CatsList.Add(cat);

        return cat;
    }
    
    public List<CatItem> GetAllCats()
    {
        return CatsList;
    }

    public CatItem? GetCatById(int id)
    {
        return CatsList.Find(cat => cat.Id == id);
    }
}