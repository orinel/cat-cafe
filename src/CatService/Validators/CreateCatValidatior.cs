using Grpc.Core;
using System.Text.RegularExpressions;

namespace CatService.Validators;

public static class CreateCatValidator
{
    public static void Validate(CreateCatRequest request)
    {
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
    }
}