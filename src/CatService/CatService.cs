using CatCafe.Contracts.Pagination;
using CatService.Repositories;
using Grpc.Core;
using Shared.Pagination;
using Shared.Filtering;
using System.Text.RegularExpressions;
using Google.Protobuf.WellKnownTypes;

namespace CatService;

public class CatGrpcService(ICatRepository repository) : Cats.CatsBase
{
    
    public override Task<CreateCatResponse> CreateCat(
        CreateCatRequest request, 
        ServerCallContext context)

    {
        var response = new CreateCatResponse();
        
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "Name is required."));
        }
        
        if (request.Name.Length > 100)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "Name must be no more than 100 characters."));
        }
        
        if (!Regex.IsMatch(request.Name, @"^[A-Za-zА-Яа-яЁё]+$"))
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "Name must contain only Latin or Cyrillic letters."));
        }
        
        if (request.Age < 1 || request.Age > 20)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "Age must be between 1 and 20."));
        }
        
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

        var cat = repository.CreateCat(request.Name, request.Age, request.Breed);

        response.Cat = cat;
        return Task.FromResult(response);
    }
    
    public override Task<ListCatsResponse> ListCats(
        ListCatsRequest request,
        ServerCallContext context)
    {
        var response = new ListCatsResponse();

        var cats = repository.GetAllCats();

        if (request.HasStatus)
        {
            cats = Filtering.Filter(cats, cat => cat.Status == request.Status);
        }
        
        if (request.HasActivity)
        {
            cats = Filtering.Filter(cats, cat => cat.Activity == request.Activity);
        }
        
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