namespace CatService.Repositories;

public interface ICatRepository
{
    CatItem CreateCat(string name, int age, Breed breed);
    List<CatItem> GetAllCats();
    CatItem? GetCatById(int id);
    bool DeleteCat(int id);
}