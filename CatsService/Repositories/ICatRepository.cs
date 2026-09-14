namespace CatsService.Repositories;

using CatsGrpcService;

public interface ICatRepository
{
    List<CatItem> GetAllCats();
    CatItem? GetCatById(int id);
}