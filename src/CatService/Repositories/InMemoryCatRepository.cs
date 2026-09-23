namespace CatService.Repositories;

public class InMemoryCatRepository : ICatRepository
{
    private List<CatItem> CatsList { get; }  = 
    [
        CreateCat(1, "Барсик", 3, Breed.DomesticCat, CatStatus.Available),
        CreateCat(2, "Мурзик", 5, Breed.DomesticCat, CatStatus.Available),
        CreateCat(3, "Васька", 2, Breed.DomesticCat, CatStatus.Resting),
    ];

    private static CatItem CreateCat(int id, string name, int age, Breed breed, CatStatus status)
    {
        return new CatItem
        {
            Id = id,
            Name = name,
            Age = age,
            Breed = breed,
            Status = status
        };
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