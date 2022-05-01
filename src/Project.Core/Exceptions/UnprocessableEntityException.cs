using System.Net;

namespace Project.Core.Exceptions
{
    /// <summary>
    /// An exception that is thrown when the requested entity cannot be processed.
    /// </summary>
    public class UnprocessableEntityException : CustumeException
    {
        public UnprocessableEntityException(string message) : base(HttpStatusCode.UnprocessableEntity, message) { }
    }
}
