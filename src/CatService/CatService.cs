using CatCafe.Contracts.Pagination;
using CatService.Repositories;
using CatService.Validators;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Shared.Pagination;
using Shared.Filtering;


namespace CatService;

public class CatGrpcService(ICatRepository repository) : Cats.CatsBase
{
    
    public override Task<CreateCatResponse> CreateCat(
        CreateCatRequest request, 
        ServerCallContext context)

    {
        CreateCatValidator.Validate(request);
        
        if (repository.GetAllCats()
            .Any(cat => string.Equals(
                cat.Name,
                request.Name,
                StringComparison.OrdinalIgnoreCase)))
        {
            throw new RpcException(
                new Status(
                    StatusCode.AlreadyExists,
                    $"Cat with name '{request.Name}' already exists."));
        }

        var cat = repository.CreateCat(request.Name, request.Age, request.Gender, request.Breed);

        return Task.FromResult(new CreateCatResponse
        {
            Cat = cat 
        });
    }
    
    public override Task<ListCatsResponse> ListCats(
        ListCatsRequest request,
        ServerCallContext context)
    {
        var response = new ListCatsResponse();

        var cats = repository.GetAllCats();
        
        if (request.HasActivity)
        {
            cats = Filtering.Filter(
                cats, cat => cat.CatActivityState.Activity == request.Activity);
        }
        
        cats = cats
            .OrderBy(cat => cat.Id)
            .ToList();
        
        var pageCats = Paginator.Paginate(
            cats,
            request.Pagination.Page,
            request.Pagination.PageSize);
        
        response.Cats.AddRange(pageCats.Items);
        
        var pagination = new PaginationResponse
        {
            Page =  pageCats.PageNumber,
            PageSize = pageCats.PageSize,
            TotalItems = pageCats.TotalCount,
            TotalPages = pageCats.TotalPages,
        };
        
        response.Pagination = pagination;
        return Task.FromResult(response);
    }

    public override Task<GetCatResponse> GetCat(
        GetCatRequest request,
        ServerCallContext context)
    {
        var response = new GetCatResponse();
        var cat = repository.GetCatById(request.Id);
        if (cat is null)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound, $"Cat id: {request.Id} not found"));
        } 
        response.Cat = cat;
        return Task.FromResult(response);
    }

    public override Task<Empty> StartExternalActivity(
        StartExternalActivityRequest request, 
        ServerCallContext context)
    {
        var res = repository.SetCatActivity(
            request.CatId,
            request.Activity,
            request.ExternalActivityId,
            request.StartTime.ToDateTimeOffset(),
            request.EndTime.ToDateTimeOffset()
            );
        
        if (res == false)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound, $"Cat id: {request.CatId} not found"));
        }
        
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> DeleteCat(
        DeleteCatRequest request,
        ServerCallContext context)
    {
        var res = repository.DeleteCat(request.Id);
        if (!res)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound, $"Cat id: {request.Id} not found"));
        }
        return Task.FromResult(new Empty());
    }
}