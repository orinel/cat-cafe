namespace CatService.Repositories;

public interface ICatRepository
{
    CatItem CreateCat(string name, int age, Gender gender, Breed breed);
    List<CatItem> GetAllCats();
    CatItem? GetCatById(int id);
    bool SetCatActivity(
        int catId,
        Activity activity,
        int? externalActivityId,
        DateTimeOffset? activityStartsAt,
        DateTimeOffset? activityEndsAt);
    bool DeleteCat(int id);
}