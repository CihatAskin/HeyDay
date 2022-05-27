using System.Net;

namespace Heyday.Application.Common.Exceptions;

public class EntityNotFoundException : CustomException
{
    public EntityNotFoundException(string message) : base(message, null, HttpStatusCode.NotFound)
    {
    }
}