using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using CatsService.Repositories;

namespace CatsGrpcService;

public class CatsService : Cats.CatsBase
{
    private readonly ICatRepository repository;

    public CatsService(ICatRepository repository)
    {
        this.repository = repository;
    }
    
    public override Task<ListCatsReply> ListCats(
        Empty request,
        ServerCallContext context)
    {
        var reply = new ListCatsReply();
        reply.Cats.AddRange(repository.GetAllCats());
        return Task.FromResult(reply);
    }
}