using CatCafe.Contracts.Pagination;
using CatService.Repositories;
using Grpc.Core;
using Shared.Pagination;
using Shared.Filtering;

namespace CatService;

public class CatGrpcService(ICatRepository repository) : Cats.CatsBase
{
    
    public override Task<ListCatsResponse> ListCats(
        ListCatsRequest request,
        ServerCallContext context)
    {
        var reply = new ListCatsResponse();

        var cats = repository.GetAllCats();

        if (request.HasStatus)
        {
            cats = Filtering.Filter(cats, cat => cat.Status == request.Status);
        }
        
        var pageCats = Paginator.Paginate(
            cats,
            request.Pagination.Page,
            request.Pagination.PageSize);
        
        reply.Cats.AddRange(pageCats.Items);
        
        var pagination = new PaginationResponse
        {
            Page =  pageCats.PageNumber,
            PageSize = pageCats.PageSize,
            TotalItems = pageCats.TotalCount,
            TotalPages = pageCats.TotalPages,
        };
        
        reply.Pagination = pagination;
        return Task.FromResult(reply);
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
}