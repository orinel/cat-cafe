namespace CatService.Repositories;

using CatService;

public interface ICatRepository
{
    List<CatItem> GetAllCats();
    CatItem? GetCatById(int id);
}