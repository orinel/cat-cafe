using Google.Protobuf.WellKnownTypes;

namespace CatService.Repositories;

public class InMemoryCatRepository : ICatRepository
{
    private List<CatItem> CatsList { get; }  = 
    [
        CreateSeedCat(1, "Барсик", 3, Gender.Male, Breed.DomesticCat, CatActivity.Eating),
        CreateSeedCat(2, "Мурка", 5, Gender.Female, Breed.DomesticCat, CatActivity.Grooming),
        CreateSeedCat(3, "Васька", 2, Gender.Male, Breed.DomesticCat, CatActivity.Resting),
    ];

    private static CatItem CreateSeedCat(int id, string name, int age, Gender gender, Breed breed, CatActivity activity)
    {
        return new CatItem
        {
            Id = id,
            Name = name,
            Age = age,
            Gender = gender,
            Breed = breed,
            Activity = activity,
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

    public bool DeleteCat(int id)
    {
        return CatsList.RemoveAll(cat => cat.Id == id) > 0;
    }
}