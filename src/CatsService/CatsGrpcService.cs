using Grpc.Core;
using CatsService.Repositories;
using CatCafe.Contracts.Pagination;
using Pagination;

namespace CatsGrpcService;

public class CatsService(ICatRepository repository) : Cats.CatsBase
{
    
    public override Task<ListCatsResponse> ListCats(
        PaginationRequest request,
        ServerCallContext context)
    {
        var reply = new ListCatsResponse();
        var pageCats = Paginator.Paginate(
            repository.GetAllCats(),
            request.Page,
            request.PageSize);
        
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