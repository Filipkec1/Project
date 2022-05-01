using System;
using System.Net;

namespace Project.Core.Exceptions
{
    /// <summary>
    /// An exception that is thrown when the requested entitiy has not been found.
    /// </summary>
    public class EntityNotFoundException : CustumeException
    {
        public EntityNotFoundException(Type type, object id) : base(HttpStatusCode.NotFound, $"{type} with '{id}' was not found") { }
    }
}
